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


        //[HttpGet]
        //[Route("GetAllThongTinDangKy")]

        //public ActionResult GetAllThongTinDangKy()
        //{
        //    try
        //    {
        //        List<DKiMuonSachDTO_PM> data = new List<DKiMuonSachDTO_PM>();
        //        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/PhieuMuon/GetAllThongTinDangKy").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string dataJson = response.Content.ReadAsStringAsync().Result;
        //            data = JsonConvert.DeserializeObject<List<DKiMuonSachDTO_PM>>(dataJson);

        //            return Json(new { success = true, result = data });
        //        }
        //        else
        //        {
        //            return Json(new { success = false });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json( new { success = false, message = ex.Message });
        //    }
        //}


        //[HttpPost]
        //[Route("ThemSachMuon")]
        //public ActionResult ThemSachMuon(int MaSach, string TenSach, int SoLuong, int MaDK)
        //{
        //    List<DTO_Sach_Muon> listSachMuon;

        //    if (MaDK > 0)
        //    {
        //        List<DTO_Sach_Muon> data = new List<DTO_Sach_Muon>();

        //        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/PhieuMuon/Get_CTDK_ByMaDK/{MaDK}").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string dataJson = response.Content.ReadAsStringAsync().Result;
        //            data = JsonConvert.DeserializeObject<List<DTO_Sach_Muon>>(dataJson);
        //        }
        //        else
        //        {
        //            return Json(new { success = false });
        //        }

        //        Console.WriteLine("LoaiClick: " + HttpContext.Session.GetObjectFromJson<int>("LoaiClick"));

        //        if (HttpContext.Session.GetObjectFromJson<int>("LoaiClick") == 2)
        //        {
        //            HttpContext.Session.SetObjectAsJson("ListSachMuon", null);
        //        }

        //        if (data == null)
        //        {
        //            listSachMuon = new List<DTO_Sach_Muon>();
        //        }
        //        else
        //        {
        //            listSachMuon = data;
        //        }

        //        HttpContext.Session.SetObjectAsJson("ListSachMuon", listSachMuon);
        //        HttpContext.Session.SetObjectAsJson("LoaiClick", 1);

        //        Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
        //        Console.WriteLine("LoaiClick: " + HttpContext.Session.GetObjectFromJson<int>("LoaiClick"));


        //        // Trả về một JsonResult chứa danh sách sách đã cập nhật
        //        return Json(new { success = true, data = listSachMuon });

        //    }
        //    else
        //    {
        //        Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
        //        Console.WriteLine("LoaiClick: " + HttpContext.Session.GetObjectFromJson<int>("LoaiClick"));

        //        if (HttpContext.Session.GetObjectFromJson<int>("LoaiClick") == 1)
        //        {
        //            HttpContext.Session.SetObjectAsJson("ListSachMuon", null);
        //            Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
        //        }

        //        // Lấy danh sách sách đã mượn từ Session hoặc tạo danh sách mới nếu chưa tồn tại
        //        if ( HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon") == null)
        //        {
        //            listSachMuon = new List<DTO_Sach_Muon>();
        //        }
        //        else
        //        {
        //            listSachMuon = HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon");

        //            Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
        //        }

        //        // Tìm xem sách có MaSach trong danh sách chưa
        //        var existingSach = listSachMuon.FirstOrDefault(s => s.MaSach == MaSach);

        //        if (existingSach != null)
        //        {
        //            if ((existingSach.SoLuong + SoLuong) > 2)
        //            {
        //                return Json(new { success = false, message = "Số lượng sách vượt quá quy định" });
        //            }
        //            else
        //            {
        //                // Nếu đã tồn tại, tăng số lượng
        //                existingSach.SoLuong += SoLuong;
        //            }
        //        }
        //        else
        //        {
        //            // Nếu chưa tồn tại, thêm sách mới vào danh sách
        //            var sachMoi = new DTO_Sach_Muon
        //            {
        //                MaSach = MaSach,
        //                TenSach = TenSach,
        //                SoLuong = SoLuong
        //            };

        //            listSachMuon.Add(sachMoi);
        //        }

        //        // Lưu danh sách đã cập nhật vào Session
        //        HttpContext.Session.SetObjectAsJson("ListSachMuon", listSachMuon);
        //        HttpContext.Session.SetObjectAsJson("LoaiClick", 2);

        //        Console.WriteLine("ListSachMuon: " + HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon"));
        //        Console.WriteLine("LoaiClick: " + HttpContext.Session.GetObjectFromJson<int>("LoaiClick"));


        //        // Trả về một JsonResult chứa danh sách sách đã cập nhật
        //        return Json(new { success = true, data = listSachMuon });
        //    }

        //}


        //[HttpPost]
        //[Route("LamMoiDanhSachSachMuon")]
        //public ActionResult LamMoiDanhSachSachMuon()
        //{
        //    HttpContext.Session.SetObjectAsJson("ListSachMuon", new List<DTO_Sach_Muon>());
        //    return Json(new { success = true });
        //}


        //[HttpPost]
        //[Route("XoaSachMuon")]
        //public ActionResult XoaSachMuon(int MaSach)
        //{
        //    // Lấy danh sách sách đã mượn từ Session hoặc tạo danh sách mới nếu chưa tồn tại
        //    List<DTO_Sach_Muon> listSachMuon = listSachMuon = HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon") ?? new List<DTO_Sach_Muon>();

        //    // Tìm và xóa sách khỏi danh sách dựa trên mã sách
        //    var sachXoa = listSachMuon.FirstOrDefault(s => s.MaSach == MaSach);
        //    if (sachXoa != null)
        //    {
        //        listSachMuon.Remove(sachXoa);
        //        HttpContext.Session.SetObjectAsJson("ListSachMuon", listSachMuon);
        //        return Json(new { success = true });
        //    }

        //    return Json(new { success = false });
        //}


        //[HttpPost]
        //[Route("TaoPhieuMuon")]
        //public ActionResult TaoPhieuMuon(int MaNhanVien, int MaThe, DateOnly NgayMuon, DateOnly NgayTra, int MaDK)
        //{
        //    DTO_Tao_Phieu_Muon tpm = new DTO_Tao_Phieu_Muon();

        //    tpm.MaNhanVien = MaNhanVien;
        //    tpm.MaTheDocGia = MaThe;
        //    tpm.NgayMuon = NgayMuon;
        //    tpm.NgayTra = NgayTra;
        //    tpm.MaDK = MaDK;
        //    tpm.listSachMuon = HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon");

        //    if (HttpContext.Session.GetObjectFromJson<List<DTO_Sach_Muon>>("ListSachMuon") == null)
        //        return Json(new { success = false });
        //    else
        //    {
        //        // Gửi yêu cầu POST và truyền dữ liệu từ đối tượng tpm dưới dạng body
        //        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/PhieuMuon/Insert", tpm).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            if (tpm.MaDK != 0)
        //            {
        //                HttpResponseMessage res = _client.GetAsync(_client.BaseAddress + $"/PhieuMuon/UpdateTinhTrang/{tpm.MaDK}/{2}").Result;

        //                if (res.IsSuccessStatusCode)
        //                {
        //                    return Json(new { success = true });

        //                }
        //                else
        //                {
        //                    return Json(new { success = false, message = "Failed to retrieve data from API." });
        //                }
        //            }

        //            return Json(new { success = true });
        //        }
        //        else
        //        {
        //            return Json(new { success = false, message = "Failed to retrieve data from API." });
        //        }
        //    }
        //}


    }
}
