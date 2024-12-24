using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.Areas.Admin.Data;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/quanlyphieutra")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class QuanLyPhieuTraController : Controller
    { // Constructor của lớp DangKyMuonSachController (có thể để trống vì chưa cần API)

        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;
        public QuanLyPhieuTraController()
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
                return View();
            }
        }


        [HttpPost]
        [Route("GetListPhieuTraPaging_APP")]
        public async Task<IActionResult> GetListPhieuTraPaging_APP([FromBody] GetListPhieuTraPaging req)
        {
            try
            {
                PagingResult<PhieuTra_GroupMaPM_DTO> phieuTraList = new PagingResult<PhieuTra_GroupMaPM_DTO>();

                var reqjson = JsonConvert.SerializeObject(req);
                var httpContent = new StringContent(reqjson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/QuanLyPhieuTra/GetListPhieuTraPaging_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    phieuTraList = JsonConvert.DeserializeObject<PagingResult<PhieuTra_GroupMaPM_DTO>>(data);
                    //return Ok(responseObject);
                    return Json(new { success = true, phieuTraList = phieuTraList });
                }
                else
                {
                    // Handle unsuccessful status code
                    return Json(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Get_ChiTietPT_ByMaPM_APP/{maPM}")]
        public IActionResult Get_ChiTietPT_ByMaPM_APP(int maPM)
        {

            List<DTO_Sach_Tra> bookList = new List<DTO_Sach_Tra>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/QuanLyPhieuTra/Get_ChiTietPT_ByMaPM_API/{maPM}").Result;

            if (response.IsSuccessStatusCode)
            {

                string data = response.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<dynamic>(data);
                bookList = responseObject.ToObject<List<DTO_Sach_Tra>>();

            }
            return Ok(bookList);
            //return Json(new { success = true, sachList = bookList });
        }


        [HttpPost]
        [Route("Get_ChiTietPT_ByMaPT_APP/{maPT}")]
        public IActionResult Get_ChiTietPT_ByMaPT_APP(int maPT)
        {

            List<DTO_Sach_Tra> bookList = new List<DTO_Sach_Tra>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/QuanLyPhieuTra/Get_ChiTietPT_ByMaPT_API/{maPT}").Result;

            if (response.IsSuccessStatusCode)
            {

                string data = response.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<dynamic>(data);
                bookList = responseObject.ToObject<List<DTO_Sach_Tra>>();

            }
            return Ok(bookList);
            //return Json(new { success = true, sachList = bookList });
        }


        [HttpPost]
        [Route("TaoHoaDon_APP/{maPT}/{maThe}")]
        public async Task<IActionResult> TaoHoaDon_APP(int maPT, int maThe)
        {
            try
            {
                // Gửi yêu cầu tới API
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/QuanLyPhieuTra/TaoHoaDon_API/{maPT}/{maThe}").Result;

                // Kiểm tra phản hồi
                if (response.IsSuccessStatusCode)
                {
                    var pdfData = await response.Content.ReadAsByteArrayAsync();

                    if (pdfData?.Length > 0)
                    {
                        // Mã hóa PDF thành Base64
                        string base64Pdf = Convert.ToBase64String(pdfData);

                        // Trả về kết quả
                        return Json(new { success = true, pdfBase64 = base64Pdf });
                    }

                    return Json(new { success = false, message = "API không trả về dữ liệu PDF." });
                }

                return Json(new
                {
                    success = false,
                    message = $"Yêu cầu API thất bại. Mã lỗi: {(int)response.StatusCode} ({response.StatusCode})"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Lỗi khi gọi API: " + ex.Message
                });
            }
        }


    }
}
