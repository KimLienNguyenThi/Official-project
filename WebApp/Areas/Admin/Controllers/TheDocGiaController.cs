using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using WebApp.Admin.Data;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Helper;
using WebApp.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/thedocgia")]

    //[Authorize(AuthenticationSchemes = "AdminCookie")]

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

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/TheDocGia/GetAllTheDocGia").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_DocGia_TheDocGia>>>(dataJson);

                    if (apiResponse != null && apiResponse.Success)
                    {
                        var data = apiResponse.Data;
                        ViewData["TheDocGia"] = data;
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
        }


        [HttpPost]
        [Route("ThongTinTheDocGia")]
        public async Task<ActionResult> ThongTinTheDocGia(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/TheDocGia/ThongTinTheDocGia/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<DTO_DocGia_TheDocGia>>(dataJson);

                    if (apiResponse != null && apiResponse.Success)
                    {
                        // Trả về dữ liệu JSON nếu thành công
                        return Json(new { success = true, data = apiResponse.Data });
                    }
                    else
                    {
                        // Trả về thông báo lỗi nếu không tìm thấy sách
                        return Json(new { success = false, message = apiResponse?.Message });
                    }
                }
                else
                {
                    // Xử lý trường hợp phản hồi lỗi từ API
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Lỗi từ API: " + errorMessage });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine($"Error in ThongTinTheDocGia: {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi hệ thống" });
            }
        }



        [HttpPost]
        [Route("GiaHanTheDocGia")]
        public async Task<ActionResult> GiaHanTheDocGia(int maThe, DateOnly thoiGianGiaHan, int tienGiaHan)
        {
            try
            {
                DTO_DocGia_TheDocGia tdg = new DTO_DocGia_TheDocGia();

                tdg.MaThe = maThe;
                tdg.NgayHetHan = thoiGianGiaHan;
                tdg.TienThe = tienGiaHan;
                tdg.DiaChi = "null";
                tdg.GioiTinh = "null";
                tdg.HoTenDG = "null";
                tdg.NgayDangKy = thoiGianGiaHan;
                tdg.NgaySinh = thoiGianGiaHan;
                tdg.SDT = "null";

                // call API
                HttpResponseMessage response = await _client.PostAsJsonAsync(_client.BaseAddress + "/TheDocGia/Update", tdg);

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
                // Xử lý lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi" });
            }
        }


        [HttpGet]
        [Route("GenerateTheDocGiaPDF")]
        public async Task<IActionResult> GenerateTheDocGiaPDF(int maNV, int maThe, DateTime ngayDK, string tenDocGia, string soDienThoai, string gioiTinh, DateOnly ngaySinh, string diaChi, int hanThe, int tienDK)
        {
            try
            {
                DTO_DocGia_TheDocGia tdg = new DTO_DocGia_TheDocGia
                {
                    MaThe = maThe,
                    MaNhanVien = maNV,
                    NgayDangKy = DateOnly.FromDateTime(DateTime.Now),
                    HoTenDG = tenDocGia,
                    SDT = soDienThoai,
                    GioiTinh = gioiTinh,
                    NgaySinh = ngaySinh,
                    DiaChi = diaChi,
                    TienThe = tienDK,
                    NgayHetHan = DateOnly.FromDateTime(DateTime.Now).AddMonths(hanThe)
                };

                // Gọi API tạo file PDF
                HttpResponseMessage generatePDF = await _client.PostAsJsonAsync(_client.BaseAddress + "/GeneratePDF/GenerateTheDocGiaPDF", tdg);

                if (generatePDF.IsSuccessStatusCode)
                {
                    // Trả file về client (browser)
                    var fileContent = await generatePDF.Content.ReadAsByteArrayAsync();
                    var fileName = "HoaDonTaoThe.pdf";
                    return File(fileContent, "application/pdf", fileName);
                }
                else
                {
                    // Trả lỗi nếu không thành công
                    string errorDetails = await generatePDF.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Tạo file PDF thất bại: " + errorDetails });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Tạo file PDF thất bại: " + ex.Message });
            }
        }



        [HttpPost]
        [Route("DangKyTheDocGia")]
        public async Task<ActionResult> DangKyTheDocGia(int maNV, DateTime ngayDK, string tenDocGia, string soDienThoai, string gioiTinh, DateOnly ngaySinh, string diaChi, int hanThe, int tienDK)
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
                HttpResponseMessage response = await _client.PostAsJsonAsync(_client.BaseAddress + "/TheDocGia/DangKyTheDocGia", tdg);

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<DTO_DocGia_TheDocGia>>(dataJson);

                    if (apiResponse != null && apiResponse.Success) 
                    {
                        return Json(new { success = true, data = apiResponse.Data });
                    }
                    else
                    {
                        return Json(new { success = false, message = apiResponse.Message });
                    }
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
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/TheDocGia/GetAllTheDocGia").Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_DocGia_TheDocGia>>>(dataJson);

                if (apiResponse != null && apiResponse.Success)
                {
                    return Json(new { success = true, data = apiResponse.Data });
                }
                else
                {
                    return Json(new { success = false, Message = apiResponse.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = response.Content.ReadAsStringAsync() });
            }
        }
    }
}
