using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;
using WebApp.DTOs;
using WebApp.Models.Responses;
using X.PagedList;

namespace WebApp.Controllers
{
    public class BorrowBookController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7028/api/Client");
        private readonly HttpClient _client;

        public BorrowBookController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        public async Task<IActionResult> Index(int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                List<dynamic> bookList = new List<dynamic>();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Book/GetAllBooks").Result;

                // Ghi lại thông tin về trạng thái phản hồi
                Debug.WriteLine($"Response Status Code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Response Data: {data}"); // Ghi lại nội dung trả về

                    bookList = JsonConvert.DeserializeObject<List<dynamic>>(data);
                    Debug.WriteLine($"Number of books retrieved: {bookList.Count}");
                }
                else
                {
                    Debug.WriteLine("Failed to retrieve books.");
                }

                // Cài đặt phân trang
                int pageSize = 9;
                int pageNumber = (page ?? 1);
                var pagedBooks = bookList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.CurrentPage = pageNumber;
                ViewBag.TotalPages = (int)Math.Ceiling((double)bookList.Count / pageSize);

                return View(pagedBooks);
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetBookByName(string tenSach)
        {
            List<GetBookByNameResDto> bookList = new List<GetBookByNameResDto>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Book/GetBookByName/{Uri.EscapeDataString(tenSach)}").Result;

            Debug.WriteLine($"Response Status Code: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Response Data: {data}"); // Ghi lại nội dung trả về

                bookList = JsonConvert.DeserializeObject<List<GetBookByNameResDto>>(data) ?? bookList;
                Debug.WriteLine($"Number of books retrieved: {bookList.Count}");
            }
            else
            {
                Debug.WriteLine("Failed to retrieve books.");
            }

            Debug.WriteLine(JsonConvert.SerializeObject(bookList));

            return Ok(new { success = true, sachList = bookList });
        }


        [HttpPost]
        public async Task<IActionResult> GetBookByCategory(string ngonNgu, string theLoai, string namXB)
        {

            List<GetBookByNameResDto> bookList = new List<GetBookByNameResDto>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Book/GetBookByCategory/{ngonNgu}/{theLoai}/{namXB}").Result;

            Debug.WriteLine($"Response Status Code: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Response Data: {data}"); // Ghi lại nội dung trả về

                bookList = JsonConvert.DeserializeObject<List<GetBookByNameResDto>>(data) ?? bookList;
                Debug.WriteLine($"Number of books retrieved: {bookList.Count}");
            }
            else
            {
                Debug.WriteLine("Failed to retrieve books.");
            }

            Debug.WriteLine(JsonConvert.SerializeObject(bookList));

            return Ok(new { success = true, sachList = bookList });


        }

        [HttpPost]
        public IActionResult Borrow(int maSach, int soLuongMuon, int soLuongSachHienTai)
        {
            try
            {
                if (ListSachMuon.listSachMuon.ContainsKey(maSach))
                {
                    var value = ListSachMuon.listSachMuon[maSach];

                    if ((value + soLuongMuon) > (soLuongSachHienTai - 5))
                    {
                        return Json(new { success = false, message = "Số lượng sách vượt quá số lượng sách hiện có!" });
                    }
                    if ((value + soLuongMuon) > 2)
                    {
                        return Json(new { success = false, message = "Số lượng sách mượn vượt quá 2 quyển cùng loại!" });
                    }
                    ListSachMuon.listSachMuon[maSach] = value + soLuongMuon;
                }
                else
                {
                    if (soLuongMuon > 2)
                    {
                        return Json(new { success = false, message = "Số lượng sách mượn vượt quá 2 quyển cùng loại!" });
                    }
                    ListSachMuon.listSachMuon.Add(maSach, soLuongMuon);
                }
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        //public async Task<IActionResult> GioHang()
        //{
        //    List<SachDTO> bookList = new List<SachDTO>();

        //    try
        //    {
        //        // lấy ra danh sách mã sách từ listSachMuon để gọi API
        //        var listMaSach = ListSachMuon.listSachMuon.Keys;
        //        int[] maSach = listMaSach.ToArray();
        //        // Xây dựng query string từ mảng mã sách
        //        string queryString = string.Join("&", maSach.Select(id => $"maSach={id}"));

        //        HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/BorrowBook/GetBooksForBorrow?{queryString}");


        //        if (response.IsSuccessStatusCode)
        //        {
        //            string data = response.Content.ReadAsStringAsync().Result;
        //            bookList = JsonConvert.DeserializeObject<List<SachDTO>>(data);
        //        }
        //        else
        //        {
        //            return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        return StatusCode(500, new { success = false, message = ex.Message });
        //    }

        //    return View(bookList);
        //}


        //public IActionResult ConfirmBorrow(int[] maSach, int[] soLuongSach)
        //{
        //    try
        //    {
        //        BorrowingData data = new BorrowingData
        //        {
        //            MaSach = maSach,
        //            SoLuongSach = soLuongSach,
        //            SdtUser = User.FindFirstValue("PhoneNumber")
        //    };

        //        // Gửi yêu cầu POST và truyền dữ liệu từ đối tượng BorrowingData dưới dạng body
        //        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/BorrowBook/BorrowBook", data).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Xóa danh sách sách mượn
        //            ListSachMuon.listSachMuon.Clear();
        //            return Ok(new { success = true });
        //        }
        //        else
        //        {
        //            return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý ngoại lệ
        //        return StatusCode(500, new { success = false, message = ex.Message });
        //    }
        //}


        //[HttpPost]
        //public ActionResult XoaSachMuon(int maSach)
        //{
        //    try
        //    {
        //        if (ListSachMuon.listSachMuon.ContainsKey(maSach))  // Kiểm tra mã sách đưa vào có tồn tại hay không
        //        {
        //            ListSachMuon.listSachMuon.Remove(maSach);
        //            return Json(new { success = true, message = "Cập nhật số lượng thành công" });
        //        }
        //        else
        //        {
        //            return Json(new { success = false, message = "Không tìm thấy sách." });
        //        }
        //    }
        //    catch
        //    {
        //        return Json(new { success = false, message = "Đã xảy ra lỗi khi xoá sách." });
        //    }
        //}
    }
}
