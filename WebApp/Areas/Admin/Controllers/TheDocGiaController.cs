using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Helper;
using WebApp.DTOs;
using WebApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/thedocgia")]

    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class TheDocGiaController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public TheDocGiaController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [Route("")]
        public IActionResult Index()
        {
            if (User.IsInRole("QuanLyKho"))
            {
                return RedirectToAction("LoiPhanQuyen", "phanquyen");
            }
            else
            {
                List<DTO_DocGia_TheDocGia> data = new List<DTO_DocGia_TheDocGia>();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/TheDocGia/GetAllTheDocGia").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<List<DTO_DocGia_TheDocGia>>(dataJson);

                    ViewData["TheDocGia"] = data;
                    return View();
                }
                else
                {
                    return View();
                }
            }


        }


        [HttpPost]
        [Route("ThongTinTheDocGia")]
        public ActionResult ThongTinTheDocGia(int id)
        {
            try
            {
                DTO_DocGia_TheDocGia theDocGia = new DTO_DocGia_TheDocGia();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/TheDocGia/ThongTinTheDocGia/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    theDocGia = JsonConvert.DeserializeObject<DTO_DocGia_TheDocGia>(dataJson);

                    if (theDocGia != null)
                    {
                        // Trả về dữ liệu JSON nếu thành công
                        return Json(new { success = true, data = theDocGia });
                    }
                    else
                    {
                        // Trả về thông báo lỗi nếu không tìm thấy sách
                        return Json(new { success = false, message = "Không tìm thấy độc  giả" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Gọi API không thành công" });
                }


            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine($"Error in ChiTietDocGia: {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi" });
            }
        }


        [HttpPost]
        [Route("GiaHanTheDocGia")]
        public ActionResult GiaHanTheDocGia(int maThe, DateOnly thoiGianGiaHan, int tienGiaHan)
        {
            try
            {
                DTO_DocGia_TheDocGia tdg = new DTO_DocGia_TheDocGia();

                tdg.MaThe = maThe;
                tdg.NgayHetHan = thoiGianGiaHan;
                tdg.TienThe = tienGiaHan;
                tdg.DiaChi = "a";
                tdg.GioiTinh = "a";
                tdg.HoTenDG = "a";
                tdg.NgayDangKy = thoiGianGiaHan;
                tdg.NgaySinh = thoiGianGiaHan;
                tdg.SDT = "0";

                // call API
                HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/TheDocGia/Update", tdg).Result;

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, data = tdg });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi" });
            }
        }


        [HttpPost]
        [Route("DangKyTheDocGia")]
        public ActionResult DangKyTheDocGia(int maNV, DateTime ngayDK, string tenDocGia, string soDienThoai, string gioiTinh, DateOnly ngaySinh, string diaChi, int hanThe, int tienDK)
        {
            try
            {
                DTO_DocGia_TheDocGia tdg = new DTO_DocGia_TheDocGia();

                tdg.MaNhanVien = maNV;
                tdg.NgayDangKy = DateOnly.FromDateTime(DateTime.Now);
                tdg.HoTenDG = tenDocGia;
                tdg.SDT = soDienThoai;
                tdg.GioiTinh = gioiTinh;
                tdg.NgaySinh = ngaySinh;
                tdg.DiaChi = diaChi;
                tdg.TienThe = tienDK;
                tdg.NgayHetHan = DateOnly.FromDateTime(DateTime.Now).AddMonths(hanThe);

                // call API
                HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/TheDocGia/DangKyTheDocGia", tdg).Result;

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, data = tdg });
                }
                else
                {
                    return Json(new { success = false, message = response.Content.ReadAsStringAsync() });
                }

            }
            catch (Exception ex)
            {
                // Kiểm tra xem lỗi có phải do tồn tại số điện thoại không
                if (ex.Message.Contains("Số điện thoại đã tồn tại."))
                    return Json(new { success = false, message = "Số điện thoại đã tồn tại." });

                // Nếu không phải là lỗi tồn tại số điện thoại, xử lý lỗi khác
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("DangKyTheDocGia")]
        public ActionResult GetAllTheDocGia()
        {
            List<DTO_DocGia_TheDocGia> data = new List<DTO_DocGia_TheDocGia>();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/TheDocGia/GetAllTheDocGia").Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<DTO_DocGia_TheDocGia>>(dataJson);

                return Json(new { success = true, data = data });
            }
            else
            {
                return Json(new { success = false, message = "Lỗi gọi API" });
            }
        }
    }
}
