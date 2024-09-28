using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Areas.Admin.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/phanquyen")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class PhanQuyenController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public PhanQuyenController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [Route("")]
        public IActionResult Index()
        {
            if (!(User.IsInRole("Admin")))
            {
                return RedirectToAction("loiphanquyen", "phanquyen");
            }
            else
            {
                List<DTO_NhanVien_LoginNV> data = new List<DTO_NhanVien_LoginNV>();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Account/GetAllNhanVien").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<List<DTO_NhanVien_LoginNV>>(dataJson);

                    ViewData["ThongTinNhanVien"] = data;
                    return View();
                }
                else
                {
                    return View();
                }

            }
        }


        [Route("LoiPhanQuyen")]
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }


        [Route("ThongTinNhanVien")]
        [HttpPost]
        public ActionResult ThongTinNhanVien(int id)
        {
            try
            {
                DTO_NhanVien_LoginNV data = new DTO_NhanVien_LoginNV();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Account/GetById/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<DTO_NhanVien_LoginNV>(dataJson);


                        // Trả về dữ liệu JSON nếu thành công
                        return Json(new { success = true, data = data });
                }
                else
                {
                    // Trả về thông báo lỗi nếu không tìm thấy sách
                    return Json(new { success = false, message = "Không tìm thấy nhân viên" });
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
        [Route("ThemNhanVien")]
        public ActionResult ThemNhanVien(string hoTen, string soDienThoai, string gioiTinh, DateOnly ngaySinh, string diaChi, string chucVu, string username, string password)
        {
            try
            {
                DTO_NhanVien_LoginNV nv = new DTO_NhanVien_LoginNV();

                nv.HoTenNV = hoTen;
                nv.SDT = soDienThoai;
                nv.GioiTinh = gioiTinh;
                nv.NgaySinh = ngaySinh;
                nv.DiaChi = diaChi;
                nv.ChucVu = chucVu;
                nv.Username = username;
                nv.Password = password;

                // call API
                HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/Account/ThemNhanVien", nv).Result;

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, data = nv });
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


        [HttpPost]
        [Route("CapNhatThongTin")]
        public ActionResult CapNhatThongTin(int maNV, DateOnly ngaySinh, string diaChi, string gioiTinh, string soDienThoai, string hoTen, string chucVu, string username, string password)
        {
            try
            {
                DTO_NhanVien_LoginNV nv = new DTO_NhanVien_LoginNV();

                nv.MaNV = maNV;
                nv.NgaySinh = ngaySinh;
                nv.DiaChi = diaChi;
                nv.GioiTinh = gioiTinh;
                nv.SDT = soDienThoai;
                nv.HoTenNV = hoTen;
                nv.ChucVu = chucVu;
                nv.Username = username;
                nv.Password = password;

                // call API
                HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/Account/UpdateThongTinNhanVien", nv).Result;

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, data = nv });
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

                // Kiểm tra xem lỗi có phải do tồn tại số username không
                if (ex.Message.Contains("Username đã tồn tại."))
                    return Json(new { success = false, message = "Username đã tồn tại." });

                // Xử lý lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi" });
            }
        }


        [HttpGet]
        [Route("GetAllNhanVien")]
        public ActionResult GetAllNhanVien()
        {
            try
            {
                List<DTO_NhanVien_LoginNV> data = new List<DTO_NhanVien_LoginNV>();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Account/GetAllNhanVien").Result;

                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<List<DTO_NhanVien_LoginNV>>(dataJson);

                    // Trả về dữ liệu JSON nếu thành công
                    return Json(new { data = data });

            }
            catch (Exception ex)
            {
                // Trong trường hợp có lỗi, có thể log lỗi hoặc xử lý theo nhu cầu của bạn
                return Json(new { Error = "Không thể lấy dữ liệu nhan vien." });
            }
        }


    }
}
