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

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/phieumuon")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class PhieuMuonController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public PhieuMuonController()
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
                try
                {

                    // Lấy tất cả sách trong db
                    // Lấy tất cả dữ liệu ở bảng thẻ độc giả
                    // Lấy tất cả dữ liệu ở bảng DkiMuonSach khi DkiMuonSach.Tinhtrang == 1 && TheDocGia.NgayHH >= DateTime.Now

                    DataStartPhieuMuonDTO data = new DataStartPhieuMuonDTO();
                    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/PhieuMuon/GetAllDataToStart").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string dataJson = response.Content.ReadAsStringAsync().Result;
                        data = JsonConvert.DeserializeObject<DataStartPhieuMuonDTO>(dataJson);

                        ViewBag.DocGia = data.docGia;
                        ViewBag.Sach = data.sach;
                        ViewBag.DangKyMuonSach = data.dKiMuonSach;

                        HttpContext.Session.SetObjectAsJson("LoaiClick", 2);

                        Console.WriteLine("LoaiClick: " + HttpContext.Session.GetObjectFromJson<int>("LoaiClick"));

                        return View();

                    }
                    else
                    {
                        return View();
                    }

                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = ex.Message });
                }
            }


        }

        [HttpGet]
        [Route("GetAllThongTinDangKy")]

        public ActionResult GetAllThongTinDangKy()
        {
            try
            {
                List<DKiMuonSachDTO_PM> data = new List<DKiMuonSachDTO_PM>();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/PhieuMuon/GetAllThongTinDangKy").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<List<DKiMuonSachDTO_PM>>(dataJson);

                    return Json(new { success = true, result = data });
                }
                else
                {
                    return Json(new { success = false });
                }

            }
            catch (Exception ex)
            {
                return Json( new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        [Route("ThemSachMuon")]
        public ActionResult ThemSachMuon(int MaSach, string TenSach, int SoLuong, int MaDK)
        {
            List<DTO_Sach_Muon> listSachMuon;

            if (MaDK > 0)
            {
                List<DTO_Sach_Muon> data = new List<DTO_Sach_Muon>();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/PhieuMuon/Get_CTDK_ByMaDK/{MaDK}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<List<DTO_Sach_Muon>>(dataJson);
                }
                else
                {
                    return Json(new { success = false });
                }

                Console.WriteLine("LoaiClick: " + HttpContext.Session.GetObjectFromJson<int>("LoaiClick"));

                if (HttpContext.Session.GetObjectFromJson<int>("LoaiClick") == 2)
                {
                    HttpContext.Session.SetObjectAsJson("ListSachMuon", null);
                }

                if (data == null)
                {
                    listSachMuon = new List<DTO_Sach_Muon>();
                }
                else
                {
                    listSachMuon = data;
                }

                HttpContext.Session.SetObjectAsJson("ListSachMuon", listSachMuon);
                HttpContext.Session.SetObjectAsJson("LoaiClick", 1);

                Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
                Console.WriteLine("LoaiClick: " + HttpContext.Session.GetObjectFromJson<int>("LoaiClick"));


                // Trả về một JsonResult chứa danh sách sách đã cập nhật
                return Json(new { success = true, data = listSachMuon });

            }
            else
            {
                Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
                Console.WriteLine("LoaiClick: " + HttpContext.Session.GetObjectFromJson<int>("LoaiClick"));

                if (HttpContext.Session.GetObjectFromJson<int>("LoaiClick") == 1)
                {
                    HttpContext.Session.SetObjectAsJson("ListSachMuon", null);
                    Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
                }

                // Lấy danh sách sách đã mượn từ Session hoặc tạo danh sách mới nếu chưa tồn tại
                if ( HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon") == null)
                {
                    listSachMuon = new List<DTO_Sach_Muon>();
                }
                else
                {
                    listSachMuon = HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon");

                    Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
                }

                // Tìm xem sách có MaSach trong danh sách chưa
                var existingSach = listSachMuon.FirstOrDefault(s => s.MaSach == MaSach);

                if (existingSach != null)
                {
                    if ((existingSach.SoLuong + SoLuong) > 2)
                    {
                        return Json(new { success = false, message = "Số lượng sách vượt quá quy định" });
                    }
                    else
                    {
                        // Nếu đã tồn tại, tăng số lượng
                        existingSach.SoLuong += SoLuong;
                    }
                }
                else
                {
                    // Nếu chưa tồn tại, thêm sách mới vào danh sách
                    var sachMoi = new DTO_Sach_Muon
                    {
                        MaSach = MaSach,
                        TenSach = TenSach,
                        SoLuong = SoLuong
                    };

                    listSachMuon.Add(sachMoi);
                }

                // Lưu danh sách đã cập nhật vào Session
                HttpContext.Session.SetObjectAsJson("ListSachMuon", listSachMuon);
                HttpContext.Session.SetObjectAsJson("LoaiClick", 2);

                Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
                Console.WriteLine("LoaiClick: " + HttpContext.Session.GetObjectFromJson<int>("LoaiClick"));


                // Trả về một JsonResult chứa danh sách sách đã cập nhật
                return Json(new { success = true, data = listSachMuon });
            }

        }


        [HttpPost]
        [Route("LamMoiDanhSachSachMuon")]
        public ActionResult LamMoiDanhSachSachMuon()
        {
            HttpContext.Session.SetObjectAsJson("ListSachMuon", new List<DTO_Sach_Muon>());
            return Json(new { success = true });
        }


        [HttpPost]
        [Route("XoaSachMuon")]
        public ActionResult XoaSachMuon(int MaSach)
        {
            // Lấy danh sách sách đã mượn từ Session hoặc tạo danh sách mới nếu chưa tồn tại
            List<DTO_Sach_Muon> listSachMuon = listSachMuon = HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon") ?? new List<DTO_Sach_Muon>();

            // Tìm và xóa sách khỏi danh sách dựa trên mã sách
            var sachXoa = listSachMuon.FirstOrDefault(s => s.MaSach == MaSach);
            if (sachXoa != null)
            {
                listSachMuon.Remove(sachXoa);
                HttpContext.Session.SetObjectAsJson("ListSachMuon", listSachMuon);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }


        [HttpPost]
        [Route("TaoPhieuMuon")]
        public ActionResult TaoPhieuMuon(int MaNhanVien, int MaThe, DateOnly NgayMuon, DateOnly NgayTra, int MaDK)
        {
            DTO_Tao_Phieu_Muon tpm = new DTO_Tao_Phieu_Muon();

            tpm.MaNhanVien = MaNhanVien;
            tpm.MaTheDocGia = MaThe;
            tpm.NgayMuon = NgayMuon;
            tpm.NgayTra = NgayTra;
            tpm.MaDK = MaDK;
            tpm.listSachMuon = HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon");

            if (HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon") == null)
                return Json(new { success = false });
            else
            {
                // Gửi yêu cầu POST và truyền dữ liệu từ đối tượng tpm dưới dạng body
                HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/PhieuMuon/Insert", tpm).Result;

                if (response.IsSuccessStatusCode)
                {
                    if (tpm.MaDK != 0)
                    {
                        HttpResponseMessage res = _client.GetAsync(_client.BaseAddress + $"/PhieuMuon/UpdateTinhTrang/{tpm.MaDK}/{2}").Result;

                        if (res.IsSuccessStatusCode)
                        {
                            return Json(new { success = true });

                        }
                        else
                        {
                            return Json(new { success = false, message = "Failed to retrieve data from API." });
                        }
                    }

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
        }


    }
}
