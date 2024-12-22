using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using WebApp.Admin.Data;
using WebApp.Areas.Admin.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/dangkymuonsach")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class DangKyMuonSachController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public DangKyMuonSachController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("QuanLyKho"))
            {
                return RedirectToAction("LoiPhanQuyen", "phanquyen");
            }
            else
            {
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/DangKyMuonSach/GetAllDangKyMuonSach");

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_DangKyMuonSach_GroupSDT>>>(dataJson);

                    if (apiResponse != null && apiResponse.Success)
                    {
                        ViewData["dkiMuonSach"] = apiResponse.Data;
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
        [Route("HandleBtnHuyAndDuyet")]
        public ActionResult HandleBtnHuyAndDuyet(int maDK, int tinhTrang, string token)
        {
            try
            {
                // đính kèm token khi gọi API
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/UpdateTinhTrang/{maDK}/{tinhTrang}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_DangKyMuonSach_GroupSDT>>>(dataJson);

                    if (apiResponse != null && apiResponse.Success)
                    {
                        return Json(new { success = true, message = apiResponse.Message, data = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = apiResponse.Message, data = false });
                    }
                }
                else
                {
                    return Json(new { success = false, message = response.Content.ReadAsStringAsync(), data = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "error HandleBtnHuyDon: ", ex });
            }
        }


        [HttpPost]
        [Route("SubmitTaoPhieuMuon")]
        public ActionResult SubmitTaoPhieuMuon(int maNV, int maDK, DateOnly ngayTra, DateOnly ngayMuon, string sdt, string token)
        {
            try
            {
                List<DTO_Sach_Muon> danhSachDK = new List<DTO_Sach_Muon>();
                int maThe;
                bool checkHanThe;


                // call API
                HttpResponseMessage response_Get_CTDK_ByMaDK = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/Get_CTDK_ByMaDK/{maDK}").Result;

                if (response_Get_CTDK_ByMaDK.IsSuccessStatusCode)
                {
                    string dataJson = response_Get_CTDK_ByMaDK.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_Sach_Muon>>>(dataJson);
                    danhSachDK = apiResponse.Data;
                }


                // call API
                HttpResponseMessage response_GetMaTheBySDT = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/GetMaTheBySDT/{sdt}").Result;

                if (response_GetMaTheBySDT.IsSuccessStatusCode)
                {
                    string dataJson = response_GetMaTheBySDT.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<int>>(dataJson);
                    maThe = apiResponse.Data;
                }
                else
                {
                    maThe = 0;
                }


                // call API
                HttpResponseMessage response_CheckHanTheDocGia = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/CheckHanTheDocGia/{maThe}").Result;

                if (response_CheckHanTheDocGia.IsSuccessStatusCode)
                {
                    checkHanThe = true;
                }
                else
                {
                    checkHanThe = false;
                }



                if (checkHanThe)
                {
                    DTO_Tao_Phieu_Muon tpm = new DTO_Tao_Phieu_Muon();
                    tpm.NgayMuon = ngayMuon;
                    tpm.NgayTra = ngayTra;
                    tpm.MaNhanVien = maNV;
                    tpm.MaTheDocGia = maThe;
                    tpm.MaDK = maDK;
                    tpm.listSachMuon = danhSachDK;

                    // đính kèm token khi gọi API
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    // call API
                    HttpResponseMessage response_Insert = _client.PostAsJsonAsync(_client.BaseAddress + "/DangKyMuonSach/Insert", tpm).Result;

                    if (response_Insert.IsSuccessStatusCode)
                    {
                        string dataJson = response_Insert.Content.ReadAsStringAsync().Result;
                        var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_DangKyMuonSach_GroupSDT>>>(dataJson);

                        if (apiResponse != null && apiResponse.Success)
                        {
                            return Json(new { success = true, data = apiResponse.Data, message = apiResponse.Message });
                        }
                        else
                        {
                            return Json(new { success = false, data = apiResponse.Data, message = apiResponse.Message });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = response_Insert.RequestMessage });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Thẻ độc giả đã hết hạn", data = "checkthedocgia" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "error HandleBtnHuyDon: ", ex });
            }
        }


        [HttpPost]
        [Route("GetDKSachMuonByMaDK")]
        public ActionResult GetDKSachMuonByMaDK(int maDK)
        {
            // call API
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/Get_DangKySachMuon_ByMaDK/{maDK}").Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse<DTO_DangKyMuonSach>>(dataJson);

                if (apiResponse != null && apiResponse.Success)
                {
                    return Json(new { success = true, data = apiResponse.Data });
                }
                else
                {
                    return Json(new { success = false, data = apiResponse.Data });
                }
            }
            else
            {
                return Json(new { success = false, message = response.Content.ReadAsStringAsync() });
            }
        }


        [HttpPost]
        [Route("GetChiTietDKByMaDK")]
        public ActionResult GetChiTietDKByMaDK(int maDK)
        {
            List<DTO_Sach_Muon> danhSachDK = new List<DTO_Sach_Muon>();

            // call API
            HttpResponseMessage response_Get_CTDK_ByMaDK = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/Get_CTDK_ByMaDK/{maDK}").Result;

            if (response_Get_CTDK_ByMaDK.IsSuccessStatusCode)
            {
                string dataJson = response_Get_CTDK_ByMaDK.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_Sach_Muon>>>(dataJson);

                if (apiResponse != null && apiResponse.Success)
                {
                    return Json(new { success = true, data = apiResponse.Data });
                }
                else
                {
                    return Json(new { success = false, data = danhSachDK, message = apiResponse.Message });
                }

            }
            else
            {
                return Json(new { success = false, message = response_Get_CTDK_ByMaDK.Content.ReadAsStringAsync() });

            }
        }


        [HttpPost]
        [Route("CheckDocGia")]
        public ActionResult CheckDocGia(string SDT)
        {
            try
            {
                // call API
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/CheckDocGia/{SDT}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_Sach_Muon>>>(dataJson);

                    if (apiResponse != null && apiResponse.Success)
                    {
                        return Json(new { success = true, message = apiResponse.Message, data = true });
                    }
                    else
                    {
                        return Json(new { success = true, message = apiResponse.Message, data = true });
                    }
                }
                else
                {
                    return Json(new { success = true, message = response.Content.ReadAsStringAsync(), data = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while checking DocGia: ", ex.Message });
            }
        }


        [HttpGet]
        [Route("GetAllDkiMuonSach")]
        public ActionResult GetAllDkiMuonSach()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/DangKyMuonSach/GetAllDangKyMuonSach").Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse<List<DTO_DangKyMuonSach_GroupSDT>>>(dataJson);

                if (apiResponse != null && apiResponse.Success)
                {
                    return Json(new { success = true, data = apiResponse.Data });
                }
                else
                {
                    return Json(new { success = false, data = apiResponse.Data, message = apiResponse.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = response.Content.ReadAsStringAsync() });
            }
        }

    }
}