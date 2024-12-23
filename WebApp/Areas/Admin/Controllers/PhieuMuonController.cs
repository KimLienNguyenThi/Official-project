using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.DTOs.Admin;
using WebApp.Areas.Admin.Data;
using WebApp.DTOs.Admin;
using System.Collections.Generic;
using WebApp.Areas.Admin.Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApp.DTOs;
using Microsoft.AspNetCore.Authorization;
using WebApp.Admin.Data;
using System.Net.Http;
using Humanizer;
using System.Text;
using WebApp.Responses;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/phieumuon")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class PhieuMuonController : Controller
    {
        // Constructor của lớp DangKyMuonSachController (có thể để trống vì chưa cần API)
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public PhieuMuonController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

       /* Action để hiển thị trang đăng ký mượn sách*/
       [Route("")]
        public async Task <IActionResult> Index()
        {
            if (User.IsInRole("QuanLyKho"))
            {
                return  RedirectToAction("LoiPhanQuyen", "phanquyen");
            }
            else
            {
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/TheDocGia/GetAllTheDocGia");

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_DocGia_TheDocGia>>>(dataJson);
                   
                    if (apiResponse != null && apiResponse.Success)
                    {
                        var data = apiResponse.Data;
                        ViewData["ThongTinDocGia"] = data;
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
        public async Task<ActionResult> ValidatePhieuMuon(int maThe)
        {
            try
            {
                // Xây dựng URL với tham số maThe
                var requestUri = new Uri($"{_client.BaseAddress}/PhieuMuon/ValidatePhieuMuon/{maThe}");

                // Gọi API từ WebApp controller
                HttpResponseMessage response = await _client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    // Đọc nội dung phản hồi
                    var dataJson = await response.Content.ReadAsStringAsync();

                    // Giải mã JSON thành đối tượng
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<object>>(dataJson);

                    // Kiểm tra dữ liệu trả về
                    if (apiResponse?.Success == true)
                    {
                        return Json(new { success = true, message = apiResponse.Message });
                    }
                    else
                    {
                        return Json(new { success = false, message = apiResponse?.Message ?? "Lỗi không xác định từ API." });
                    }
                }
                else
                {
                    // Xử lý lỗi HTTP từ API
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"API lỗi: {errorContent}" });
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Lỗi liên quan đến yêu cầu HTTP
                return Json(new { success = false, message = $"Lỗi HTTP: {httpEx.Message}" });
            }
            catch (JsonException jsonEx)
            {
                // Lỗi khi phân tích JSON
                return Json(new { success = false, message = $"Lỗi phân tích JSON: {jsonEx.Message}" });
            }
            catch (Exception ex)
            {
                // Lỗi chung
                return Json(new { success = false, message = $"Lỗi không xác định: {ex.Message}" });
            }
        }




        [Route("GetAllThongTinDocGia")]
        public ActionResult GetAllThongTinDocGia()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/PhieuMuon/GetThongTinTheDocGia").Result;

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


        [HttpGet]
        [Route("GetByMaCuonSach/{maCuonSach}")]
        public JsonResult GetByMaCuonSach(string maCuonSach)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/PhieuMuon/GetByMaCuonSach/{maCuonSach}").Result;
             
                if (response.IsSuccessStatusCode)
                {
                    // Đọc nội dung phản hồi từ Web API
                    var dataJson = response.Content.ReadAsStringAsync().Result;

                    // Chuyển đổi JSON phản hồi thành APIResponse
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<BookDetailsDTO>>(dataJson);

                    if (apiResponse != null && apiResponse.Success)
                    {
                        // Trả về kết quả thành công
                        return Json(new
                        {
                            success = true,
                            data = apiResponse.Data,
                            message = apiResponse.Message
                        });
                    }
                    else
                    {
                        // Trả về lỗi nếu không tìm thấy sách hoặc API trả về thất bại
                        return Json(new
                        {
                            success = false,
                            data = (object)null,
                            message = apiResponse?.Message ?? "Không có thông tin"
                        });
                    }
                }
                else
                {
                    // Xử lý lỗi khi gọi API thất bại
                    return Json(new
                    {
                        success = false,
                        data = (object)null,
                        message = $"Không thể kết nối đến API: {response.ReasonPhrase}"
                    });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi bất ngờ
                return Json(new
                {
                    success = false,
                    data = (object)null,
                    message = $"Lỗi hệ thống: {ex.Message}"
                });
            }
        }


        [HttpPost]
        [Route("TaoPhieuMuon")]
        public async Task<IActionResult> TaoPhieuMuon([FromBody] DTO_Tao_Phieu_Muon tpm)
        {

            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (tpm == null)
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
                }

                // Chuyển đổi DTO thành chuỗi JSON
                var jsonContent = JsonConvert.SerializeObject(tpm);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Gửi yêu cầu POST tới API TaoPhieuTra_API
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/PhieuMuon/Insert", httpContent);

                // Kiểm tra phản hồi từ API
                if (response.IsSuccessStatusCode)
                {
                    // Đọc nội dung PDF từ phản hồi
                    var pdfData = await response.Content.ReadAsByteArrayAsync();

                    if (pdfData != null && pdfData.Length > 0)
                    {
                        // Chuyển file PDF sang dạng Base64 để gửi về JavaScript
                        string base64Pdf = Convert.ToBase64String(pdfData);
                        return Json(new { success = true, pdfBase64 = base64Pdf });
                    }

                    return Json(new { success = false, message = "Không nhận được file PDF từ API." });
                }
                string checkSL = await response.Content.ReadAsStringAsync();
                // Xử lý nếu API trả về lỗi
                return Json(new { success = false, message = checkSL });
            }
            catch (Exception ex)
            {
                // Bắt lỗi và trả về thông báo lỗi
                return Json(new { success = false, message = "Lỗi khi gọi API: " + ex.Message });
            }
        }

    }
}
