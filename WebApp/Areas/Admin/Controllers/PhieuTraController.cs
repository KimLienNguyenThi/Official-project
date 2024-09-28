using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Text;
using System.Threading;
using WebApp.Areas.Admin.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/phieutra")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class PhieuTraController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public PhieuTraController()
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
        [Route("GetListPhieuMuonPaging_APP")]
        public async Task<IActionResult> GetListPhieuMuonPaging_APP([FromBody] GetListPhieuMuonPaging req)
        {
            try
            {
                List<PhieuMuonDTO> phieuMuonList = new List<PhieuMuonDTO>();

                var reqjson = JsonConvert.SerializeObject(req);
                var httpContent = new StringContent(reqjson, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/PhieuTra/GetListPhieuMuonPaging_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string data =  response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<PagingResult<PhieuMuonDTO>>(data);
                    //return Ok(responseObject);
                    return Ok(new { success = true, phieuMuonList = responseObject });
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
        [Route("GetSachMuon_APP/{maPM}")]
        public IActionResult GetSachMuon_APP(int maPM)
        {


            List<SachMuonDTO> bookList = new List<SachMuonDTO>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/PhieuTra/GetSachMuon_API/{maPM}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<dynamic>(data);
               bookList = responseObject.ToObject<List<SachMuonDTO>>();
               // bookList = responseObject.sachList.ToObject<List<SachMuonDTO>>();

            }
            return Ok(bookList);
         //return Json(new { success = true, sachList = bookList });
        }


        [HttpPost]
        [Route("TaoPhieuTra_APP")]
        public async Task<IActionResult> TaoPhieuTra_APP([FromBody] DTO_Tao_Phieu_Tra dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Invalid data.");
                }

                var jsonContent = JsonConvert.SerializeObject(dto);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/PhieuTra/TaoPhieuTra_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            } 
            catch (Exception ex)
            {
                return BadRequest($"API call failed    {  ex.Message}" );

            }
        }
    }
}
