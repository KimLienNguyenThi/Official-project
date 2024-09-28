using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public IActionResult Index()
        {
            if (User.IsInRole("QuanLyKho"))
            {
                return RedirectToAction("LoiPhanQuyen", "phanquyen");
            }
            else
            {
                List<DTO_DangKyMuonSach_GroupSDT> data = new List<DTO_DangKyMuonSach_GroupSDT>();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/DangKyMuonSach/GetAllDangKyMuonSach").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<List<DTO_DangKyMuonSach_GroupSDT>>(dataJson);

                    ViewData["dkiMuonSach"] = data;
                    return View();
                }
                else

                {
                    return View();
                }
            }

        }


        [HttpPost]
        [Route("HandleBtnHuyAndDuyet")]
        public ActionResult HandleBtnHuyAndDuyet(int maDK, int tinhTrang)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/UpdateTinhTrang/{maDK}/{tinhTrang}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Update tình trạng thành công", data = true });
                }
                else
                {
                    return Json(new { success = true, message = "Update tình trạng thất bại", data = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "error HandleBtnHuyDon: ", ex });
            }
        }


        [HttpPost]
        [Route("SubmitTaoPhieuMuon")]
        public ActionResult SubmitTaoPhieuMuon(int maNV, int maDK, DateOnly ngayTra, DateOnly ngayMuon, string sdt)
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
                    danhSachDK = JsonConvert.DeserializeObject<List<DTO_Sach_Muon>>(dataJson);
                }


                // call API
                HttpResponseMessage response_GetMaTheBySDT = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/GetMaTheBySDT/{sdt}").Result;

                if (response_GetMaTheBySDT.IsSuccessStatusCode)
                {
                    string dataJson = response_GetMaTheBySDT.Content.ReadAsStringAsync().Result;
                    maThe = JsonConvert.DeserializeObject<int>(dataJson);
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


                    // call API
                    HttpResponseMessage response_Insert = _client.PostAsJsonAsync(_client.BaseAddress + "/DangKyMuonSach/Insert", tpm).Result;

                    if (response_Insert.IsSuccessStatusCode)
                    {
                        return Json(new { success = true, data = tpm });
                    }
                    else
                    {
                        return Json(new { success = false, message = response_Insert.RequestMessage });
                    }
                }
                else
                {
                    return Json(new { success = true, message = "Thẻ độc giả đã hết hạn", data = "checkthedocgia" });
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
            DTO_DangKyMuonSach data = new DTO_DangKyMuonSach();

            // call API
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/DangKyMuonSach/Get_DangKySachMuon_ByMaDK/{maDK}").Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<DTO_DangKyMuonSach>(dataJson);

                return Json(new { success = true, data = data });
            }
            else
            {
                return Json(new { success = false });
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
                danhSachDK = JsonConvert.DeserializeObject<List<DTO_Sach_Muon>>(dataJson);

                return Json(new { success = true, data = danhSachDK });
            }
            else
            {
                return Json(new { success = false });

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
                    return Json(new { success = true, message = "SDT đã đăng ký thẻ", data = true });
                }
                else
                {
                    return Json(new { success = true, message = "SDT chưa đăng ký thẻ", data = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while checking DocGia" });
            }
        }


        [HttpGet]
        [Route("GetAllDkiMuonSach")]
        public ActionResult GetAllDkiMuonSach()
        {
            List<DTO_DangKyMuonSach_GroupSDT> data = new List<DTO_DangKyMuonSach_GroupSDT>();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/DangKyMuonSach/GetAllDangKyMuonSach").Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<DTO_DangKyMuonSach_GroupSDT>>(dataJson);

                return Json(new { success = true, data = data });
            }
            else
            {
                return Json(new { success = false });
            }

        }

    }
}
