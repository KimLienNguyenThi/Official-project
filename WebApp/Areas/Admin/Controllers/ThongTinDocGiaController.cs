using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Areas.Admin.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/thongtindocgia")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class ThongTinDocGiaController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public ThongTinDocGiaController()
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

                    ViewData["ThongTinDocGia"] = data;
                    return View();
                }
                else
                {
                    return View();
                }
            }


        }


        [HttpGet]
        [Route("GetAllThongTinDocGia")]
        public ActionResult GetAllThongTinDocGia()
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
        [Route("CapNhatThongTin")]
        public ActionResult CapNhatThongTin(int maDocGia, DateOnly ngaySinh, string diaChi, string gioiTinh, string soDienThoai, string tenDocGia)
        {
            try
            {
                DocGium dg = new DocGium();

                dg.MaDg = maDocGia;
                dg.HoTenDg = tenDocGia;
                dg.NgaySinh =  ngaySinh;
                dg.DiaChi = diaChi;
                dg.GioiTinh = gioiTinh;
                dg.Sdt = soDienThoai;

                // call API
                HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/ThongTinDocGia/UpdateThongTinDocGia", dg).Result;

                if (response.IsSuccessStatusCode)
                {
                return Json(new { success = true, data = dg });
                }
                else
                {
                    return Json(new { success = false, message = response.RequestMessage });
                }

            }
            catch (Exception ex)
            {
                // Kiểm tra xem lỗi có phải do tồn tại số điện thoại không
                if (ex.Message.Contains("Số điện thoại đã tồn tại."))
                    return Json(new { success = false, message = "Số điện thoại đã tồn tại." });

                // Xử lý lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi" });
            }
        }


    }
}
