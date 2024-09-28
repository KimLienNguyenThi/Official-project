using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WebAPI.DTOs;
using WebApp.DTOs;
using X.PagedList;

namespace WebApp.Controllers
{
    public class BorrowBookController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api");
        private readonly HttpClient _client;

        public BorrowBookController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        public IActionResult Index(int? page)
        {
            if(User.Identity.IsAuthenticated)
            {
                // Lấy dữ liệu từ API 
                List<SachDTO> bookList = new List<SachDTO>();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Book/GetAllBook").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    bookList = JsonConvert.DeserializeObject<List<SachDTO>>(data);
                }

                // Phân trang
                // Số lượng sách trên 1 trang
                int pageSize = 9;

                // Số trang
                int pageNumber = (page ?? 1);

                // Sử dụng PagedList để chia danh sách thành các trang
                IPagedList<SachDTO> pagedListSach = bookList.ToPagedList(pageNumber, pageSize);

                // Truyền thông tin phân trang vào ViewBag để sử dụng trong View
                ViewBag.CurrentPage = pageNumber;
                ViewBag.TotalPages = pagedListSach.PageCount;

                return View(pagedListSach);
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        public IActionResult SearchBook(string tenSach)
        {

            List<SachDTO> bookList = new List<SachDTO>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Book/GetBookByName/{tenSach}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<dynamic>(data);
                bookList = responseObject.sachList.ToObject<List<SachDTO>>();
            }

            return Ok(new { success = true, sachList = bookList });
        }


        [HttpPost]
        public IActionResult GetBookByCategory(string ngonNgu, string theLoai, string namXB)
        {
            List<SachDTO> bookList = new List<SachDTO>();

            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Book/GetBookByCategory/{ngonNgu}/{theLoai}/{namXB}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(data);
                    bookList = responseObject.sachList.ToObject<List<SachDTO>>();
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, new { success = false, message = ex.Message });
            }

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
        public async Task<IActionResult> GioHang()
        {
            List<SachDTO> bookList = new List<SachDTO>();

            try
            {
                // lấy ra danh sách mã sách từ listSachMuon để gọi API
                var listMaSach = ListSachMuon.listSachMuon.Keys;
                int[] maSach = listMaSach.ToArray();
                // Xây dựng query string từ mảng mã sách
                string queryString = string.Join("&", maSach.Select(id => $"maSach={id}"));

                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/BorrowBook/GetBooksForBorrow?{queryString}");


                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    bookList = JsonConvert.DeserializeObject<List<SachDTO>>(data);
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, new { success = false, message = ex.Message });
            }

            return View(bookList);
        }


        public IActionResult ConfirmBorrow(int[] maSach, int[] soLuongSach)
        {
            try
            {
                BorrowingData data = new BorrowingData
                {
                    MaSach = maSach,
                    SoLuongSach = soLuongSach,
                    SdtUser = User.FindFirstValue("PhoneNumber")
            };

                // Gửi yêu cầu POST và truyền dữ liệu từ đối tượng BorrowingData dưới dạng body
                HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/BorrowBook/BorrowBook", data).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Xóa danh sách sách mượn
                    ListSachMuon.listSachMuon.Clear();
                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public ActionResult XoaSachMuon(int maSach)
        {
            try
            {
                if (ListSachMuon.listSachMuon.ContainsKey(maSach))  // Kiểm tra mã sách đưa vào có tồn tại hay không
                {
                    ListSachMuon.listSachMuon.Remove(maSach);
                    return Json(new { success = true, message = "Cập nhật số lượng thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Không tìm thấy sách." });
                }
            }
            catch
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi xoá sách." });
            }
        }
    }
}
