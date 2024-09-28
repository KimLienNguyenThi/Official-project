using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using WebApp.Areas.Admin.Data;
using WebApp.DTOs;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/thanhlysach")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class ThanhLySachController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public ThanhLySachController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }
        [Route("")]
        public IActionResult Index()
        {
            if (User.IsInRole("ThuThu"))
            {
                return RedirectToAction("LoiPhanQuyen", "phanquyen");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [Route("GetListDonViTLPaging_APP")]
        public async Task<IActionResult> GetListDonViTLPaging_APP([FromBody] GetListPhieuMuonPaging req)
        {
            try
            {
                List<DTO_DonViTL> donviList = new List<DTO_DonViTL>();

                var reqjson = JsonConvert.SerializeObject(req);
                var httpContent = new StringContent(reqjson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/ThanhLySach/GetListDonViTLPaging_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<WebApp.DTOs.PagingResult<DTO_DonViTL>>(data);
                    //return Ok(responseObject);
                    return Ok(new { success = true, donviList = responseObject });
                }
                else
                {
                    // Handle unsuccessful status code
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("GetListKhoTLPaging_APP")]
        public async Task<IActionResult> GetListKhoTLPaging_APP([FromBody] GetListPhieuMuonPaging req)
        {
            try
            {
                List<KhoSachThanhLyDTO> sachList = new List<KhoSachThanhLyDTO>();

                var reqjson = JsonConvert.SerializeObject(req);
                var httpContent = new StringContent(reqjson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/ThanhLySach/GetListKhoTLPaging_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<WebApp.DTOs.PagingResult<KhoSachThanhLyDTO>>(data);
                    //return Ok(responseObject);
                    return Ok(new { success = true, sachList = responseObject });
                }
                else
                {
                    // Handle unsuccessful status code
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("GetListSachNhapPaging_APP")]
        public async Task<IActionResult> GetListSachNhapPaging_APP([FromBody] GetListPhieuMuonPaging req)
        {
            try
            {
                List<DTO_Sach_Nhap_Kho> sachList = new List<DTO_Sach_Nhap_Kho>();

                var reqjson = JsonConvert.SerializeObject(req);
                var httpContent = new StringContent(reqjson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/ThanhLySach/GetListSachNhapPaging_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<WebApp.DTOs.PagingResult<DTO_Sach_Nhap_Kho>>(data);
                    //return Ok(responseObject);
                    return Ok(new { success = true, sachList = responseObject });
                }
                else
                {
                    // Handle unsuccessful status code
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("ThemDonViThanhLy_APP")]
        public async Task<IActionResult> ThemDonViThanhLy_APP([FromBody] DonViTl dto)
        {
            try
            {
                if (dto == null || string.IsNullOrEmpty(dto.TenDv) || string.IsNullOrEmpty(dto.Sdtdv) || string.IsNullOrEmpty(dto.DiaChiDv))
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
                }

                var jsonContent = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/ThanhLySach/ThemDonViThanhLy_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<InsertRes>(result);
                    return Ok(res);
                }
                else
                {
                    return BadRequest(new { success = false, message = "Thêm đơn vị thất bại." });
                }
            }
            catch (Exception ex)
            {
                //// Kiểm tra xem lỗi có phải do tồn tại số điện thoại không
                //if (ex.Message.Contains("Số điện thoại đã tồn tại."))
                //    return BadRequest(new { success = false, message = "Số điện thoại đã tồn tại." });
                return BadRequest(new { success = false, message = $"API call failed: {ex.Message}" });
            }
        }


        [HttpPost]
        [Route("ThemSachThanhLy_APP")]
        public async Task<IActionResult> ThemSachThanhLy_APP([FromBody] SachNhapKhoDTO dto)
        {
            try
            {
                if (dto == null )
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
                }

                var jsonContent = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/ThanhLySach/ThemSachThanhLy_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Thêm sách thất bại." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"API call failed: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("PhieuThanhLy_APP")]
        public async Task<IActionResult> PhieuThanhLy_APP([FromBody] DTO_Tao_Phieu_TL dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
                }

                var jsonContent = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/ThanhLySach/PhieuThanhLy_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Tạo phiếu thanh lý thất bại." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"API call failed: {ex.Message}" });
            }
        }

    }
}
