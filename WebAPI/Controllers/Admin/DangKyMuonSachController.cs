using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Service_Admin;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class DangKyMuonSachController : ControllerBase
    {
        private readonly DangKyMuonSachService _dangKyMuonSachService;

        public DangKyMuonSachController(DangKyMuonSachService dangKyMuonSachService)
        {
            _dangKyMuonSachService = dangKyMuonSachService;
        }

        [HttpGet]
        public IActionResult GetAllDangKyMuonSach()
        {
            try
            {
                List<DTO_DangKyMuonSach_GroupSDT> listDangKyMuonSach = _dangKyMuonSachService.GetAllDangKyMuonSach();

                if (listDangKyMuonSach.Count > 0)
                {
                    return Ok(new APIResponse<List<DTO_DangKyMuonSach_GroupSDT>>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = listDangKyMuonSach
                    });
                }
                else
                {
                    return Ok(new APIResponse<List<DTO_DangKyMuonSach_GroupSDT>>()
                    {
                        Success = false,
                        Message = "Không có dữ liệu trong database",
                        Data = listDangKyMuonSach
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{maDK}/{tinhTrang}")]
        public IActionResult UpdateTinhTrang(int maDK, int tinhTrang)
        {
            try
            {
                if (_dangKyMuonSachService.UpdateTinhTrang(maDK, tinhTrang))
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = true,
                        Message = "Cập nhật thành công",
                        Data = null
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Cập nhật không thành công thành công! Đã xảy ra lỗi",
                        Data = null
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{maDK}")]
        public IActionResult Get_CTDK_ByMaDK(int maDK)
        {
            try
            {
                List<DTO_Sach_Muon> listSachDK = _dangKyMuonSachService.Get_CTDK_ByMaDK(maDK);

                if (listSachDK.Count > 0) 
                { 
                    return Ok(new APIResponse<List<DTO_Sach_Muon>>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = listSachDK
                    }); 
                }
                else 
                { 
                    return Ok(new APIResponse<List<DTO_Sach_Muon>>()
                    {
                        Success = false,
                        Message = "Không có dữ liệu",
                        Data = listSachDK
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{sdt}")]
        public IActionResult GetMaTheBySDT(string sdt)
        {
            try
            {
                int maThe = _dangKyMuonSachService.GetMaTheBySDT(sdt);

                if (maThe > 0)
                {
                    return Ok(new APIResponse<int>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = maThe
                    });
                }
                else
                {
                    return Ok(new APIResponse<int>()
                    {
                        Success = false,
                        Message = "Không có dữ liệu",
                        Data = maThe
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{maThe}")]
        public IActionResult CheckHanTheDocGia(int maThe)
        {
            try
            {
                if (_dangKyMuonSachService.CheckHanTheDocGia(maThe))
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = true,
                        Message = "Tìm thấy dữ liệu",
                        Data = null
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Không tìm thấy dữ liệu",
                        Data = null
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public IActionResult Insert([FromBody] DTO_Tao_Phieu_Muon x)
        {
            try
            {
                if ( _dangKyMuonSachService.Insert(x))
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = true,
                        Message = "Tạo phiếu mượn thành công",
                        Data = null
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Tạo phiếu mượn không thành công thành công! Đã xảy ra lỗi",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{maDK}")]
        public IActionResult Get_DangKySachMuon_ByMaDK(int maDK)
        {
            try
            {
                DTO_DangKyMuonSach data = _dangKyMuonSachService.Get_DangKySachMuon_ByMaDK(maDK);

                if (data != null)
                {
                    return Ok(new APIResponse<DTO_DangKyMuonSach>()
                    { 
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = data
                    });
                }
                else
                {
                    return Ok(new APIResponse<DTO_DangKyMuonSach>()
                    {
                        Success = false,
                        Message = "Lấy dữ liệu không thành công",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{SDT}")]
        public IActionResult CheckDocGia(string SDT)
        {
            try
            {
                if (_dangKyMuonSachService.CheckDocGia(SDT))
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = true,
                        Message = "SDT đã đăng ký thẻ",
                        Data = null
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "SDT chưa đăng ký thẻ",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}