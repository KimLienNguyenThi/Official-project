using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System;
using System.Text;
using System.Threading;
using WebApp.Areas.Admin.Data;
using WebApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Headers;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/nhapsach")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class NhapSachController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public NhapSachController (IWebHostEnvironment webHostEnvironment)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _webHostEnvironment = webHostEnvironment;
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
        [Route("GetListNCCPaging_APP")]
        public async Task<IActionResult> GetListNCCPaging_APP([FromBody] GetListPhieuMuonPaging req)
        {
            try
            {
                List<NhaCungCap> nccList = new List<NhaCungCap>();

                var reqjson = JsonConvert.SerializeObject(req);
                var httpContent = new StringContent(reqjson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/NhapSach/GetListNCCPaging_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<PagingResult<NhaCungCap>>(data);
                    //return Ok(responseObject);
                    return Ok(new { success = true, nccList = responseObject });
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
        [Route("GetListSachPaging_APP")]
        public async Task<IActionResult> GetListSachPaging_APP([FromBody] GetListPhieuMuonPaging req)
        {
            try
            {
                List<SachDTO> sachList = new List<SachDTO>();

                var reqjson = JsonConvert.SerializeObject(req);
                var httpContent = new StringContent(reqjson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Nhapsach/GetListSachPaging_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<PagingResult<SachDTO>>(data);
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
        [Route("ThemNCC_APP")]
        public async Task<IActionResult> ThemNCC_APP([FromBody] NhaCungCap dto)
        {
            try
            {
                if (dto == null || string.IsNullOrEmpty(dto.TenNcc) || string.IsNullOrEmpty(dto.SdtNcc) || string.IsNullOrEmpty(dto.DiaChiNcc))
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
                }

                var jsonContent = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/NhapSach/ThemNCC_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<InsertRes>(result);
                    return Ok(res);
                }
                else
                {
                    return BadRequest(new { success = false, message = "Thêm NCC thất bại." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"API call failed: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("PhieuNhap_APP")]
        public async Task<IActionResult> PhieuNhap_APP([FromForm] DTO_Tao_Phieu_Nhap dto)
        {
            try
            {
                var lstSachNhap = new List<DTO_Sach_Nhap_Json>();

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "img_web");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var item in dto.listSachNhap)
                {
                    string relativeImagePath = "empty";
                    // Lưu ảnh và tạo đường dẫn tương đối
                    if (item.fileImage != null && item.fileImage.Length > 0)
                    {
                        var fileName = Path.GetFileName(item.fileImage.FileName); // Lấy tên file
                        var localImagePath = Path.Combine(path, fileName); // Tạo đường dẫn cục bộ đầy đủ

                        // Lưu ảnh vào thư mục wwwroot/img_web
                        using (var stream = new FileStream(localImagePath, FileMode.Create))
                        {
                            await item.fileImage.CopyToAsync(stream);
                        }

                        // Tạo đường dẫn tương đối để lưu vào SQL
                        relativeImagePath = $"~/img_web/{fileName}";
                    }

                    // Thêm đường dẫn tương đối vào đối tượng DTO_Sach_Nhap_Json
                    lstSachNhap.Add(new DTO_Sach_Nhap_Json()
                    {
                        giaSach = item.giaSach,
                        maSach = item.maSach,
                        moTa = item.moTa,
                        namXB = item.namXB,
                        ngonNgu = item.ngonNgu,
                        nhaXB = item.nhaXB,
                        soLuong = item.soLuong,
                        tacGia = item.tacGia,
                        tenSach = item.tenSach,
                        theLoai = item.theLoai,
                        fileImage = relativeImagePath // Thêm đường dẫn tương đối vào đối tượng DTO_Sach_Nhap_Json
                    });
                }

                var convertData = new DTO_Tao_Phieu_Nhap_Json()
                {
                    MaNhaCungCap = dto.MaNhaCungCap,
                    MaNhanVien = dto.MaNhanVien,
                    NgayNhap = dto.NgayNhap,
                    listSachNhap = lstSachNhap
                };

                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(convertData), Encoding.UTF8, "application/json"), "data");

                var response = await _client.PostAsync($"{_client.BaseAddress}/NhapSach/PhieuNhap_API", multipartContent);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Tạo phiếu nhập thất bại." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"API call failed: {ex.Message}" });
            }
        }

    }

}

