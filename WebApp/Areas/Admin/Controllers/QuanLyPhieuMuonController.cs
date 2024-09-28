using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.Areas.Admin.Data;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/quanlyphieumuon")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class QuanLyPhieuMuonController : Controller
    {
        
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;
        public QuanLyPhieuMuonController()
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
        [Route("GetListDG_PhieuMuonPaging_APP")]
        public async Task<IActionResult> GetListDG_PhieuMuonPaging_APP([FromBody] GetListPhieuTraPaging req)
        {
            try
            {
                PagingResult<PhieuMuon_GroupMaDG_DTO> docGiaList = new PagingResult<PhieuMuon_GroupMaDG_DTO>();

                var reqjson = JsonConvert.SerializeObject(req);
                var httpContent = new StringContent(reqjson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/QuanLyPhieuMuon/GetListDG_PhieuMuonPaging_API", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    docGiaList = JsonConvert.DeserializeObject<PagingResult<PhieuMuon_GroupMaDG_DTO>>(data);
                    //return Ok(responseObject);
                    return Json(new { success = true, docGiaList = docGiaList });
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
        [Route("Get_ChiTietPM_ByMaDG_APP/{maThe}")]
        public IActionResult Get_ChiTietPM_ByMaDG_APP(int maThe)
        {

            List<SachMuon_allPmDTO> bookList = new List<SachMuon_allPmDTO>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/QuanLyPhieuMuon/Get_ChiTietPM_ByMaDG_API/{maThe}").Result;

            if (response.IsSuccessStatusCode)
            {

                string data = response.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<dynamic>(data);
                bookList = responseObject.ToObject<List<SachMuon_allPmDTO>>();
                

            }
            return Ok(bookList);
            //return Json(new { success = true, sachList = bookList });
        }

        [HttpPost]
        [Route("Get_ChiTietPM_ByMaPM_APP/{maPM}")]
        public IActionResult Get_ChiTietPM_ByMaPM_APP(int maPM)
        {

            List<SachMuon_allPmDTO> bookList = new List<SachMuon_allPmDTO>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/QuanLyPhieuMuon/Get_ChiTietPM_ByMaPM_API/{maPM}").Result;

            if (response.IsSuccessStatusCode)
            {

                string data = response.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<dynamic>(data);
                bookList = responseObject.ToObject<List<SachMuon_allPmDTO>>();


            }
            return Ok(bookList);
            //return Json(new { success = true, sachList = bookList });
        }

    }

}
