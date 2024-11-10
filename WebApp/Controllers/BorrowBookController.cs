using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using WebApp.Areas.Admin.Data;
using WebApp.DTOs;
using WebApp.Responses;
using X.PagedList;
using static Azure.Core.HttpHeader;

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

        
        public IActionResult GioHang()
        {

            return View(); // Trả về view rỗng, JavaScript sẽ lo việc lấy dữ liệu
        }


        [HttpPost]
        public async Task<IActionResult> LayThongTinSach([FromBody] List<int> maSachList)
        {
            List<GetBookByNameResDto> bookList = new List<GetBookByNameResDto>();

            try
            {
                if (maSachList == null || !maSachList.Any())
                {
                    return BadRequest(new { success = false, message = "Danh sách mã sách trống hoặc không hợp lệ." });
                }

                string queryString = string.Join("&", maSachList.Select(id => $"masach={id}"));

                HttpResponseMessage response = await _client.GetAsync($"{_client.BaseAddress}/BorrowBook/GetBooksForBorrow?{queryString}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    bookList = JsonConvert.DeserializeObject<List<GetBookByNameResDto>>(data);
                }
                else
                {
                    return BadRequest(new { success = false, message = "Không lấy được dữ liệu từ API." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }

            return Json(new { success = true, data = bookList });
        }
        //[HttpPost]
        //public async Task<IActionResult> LayThongTinSach([FromBody] int[] maSach)
        //{
        //    try
        //    {
        //        // Gọi API backend để lấy thông tin sách
        //        using (var client = new HttpClient())
        //        {
        //            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/BorrowBook/GetBooksForBorrow/{maSach}").Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var data = await response.Content.ReadAsStringAsync();
        //                var books = JsonConvert.DeserializeObject<List<GetBookByNameResDto>>(data);

        //                return Ok(books); // Trả về danh sách sách cho frontend
        //            }
        //            else
        //            {
        //                return BadRequest(new { message = "Không thể lấy thông tin từ backend." });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}


        //[HttpPost]
        //public IActionResult Borrow(int maSach, int soLuongMuon, int soLuongSachHienTai)
        //{
        //    try
        //    {
        //        if (ListSachMuon.listSachMuon.ContainsKey(maSach))
        //        {
        //            var value = ListSachMuon.listSachMuon[maSach];

        //            if ((value + soLuongMuon) > (soLuongSachHienTai - 5))
        //            {
        //                return Json(new { success = false, message = "Số lượng sách vượt quá số lượng sách hiện có!" });
        //            }
        //            if ((value + soLuongMuon) > 2)
        //            {
        //                return Json(new { success = false, message = "Số lượng sách mượn vượt quá 2 quyển cùng loại!" });
        //            }
        //            ListSachMuon.listSachMuon[maSach] = value + soLuongMuon;
        //        }
        //        else
        //        {
        //            if (soLuongMuon > 2)
        //            {
        //                return Json(new { success = false, message = "Số lượng sách mượn vượt quá 2 quyển cùng loại!" });
        //            }
        //            ListSachMuon.listSachMuon.Add(maSach, soLuongMuon);
        //        }
        //        return Ok(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}






    }
}
