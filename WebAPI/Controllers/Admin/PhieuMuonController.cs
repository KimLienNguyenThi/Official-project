using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]

    public class PhieuMuonController : Controller
    {
        private readonly QuanLyThuVienContext _context;
        private readonly IMapper _mapper;
        private readonly PhieuMuonService _phieuMuonService;
        private readonly TheDocGiaService _theDocGiaService;
        public PhieuMuonController(IMapper mapper, QuanLyThuVienContext context, PhieuMuonService phieuMuonService, TheDocGiaService theDocGiaService)
        {
            _context = context;
            _mapper = mapper;
            _phieuMuonService = phieuMuonService;
            _theDocGiaService = theDocGiaService;

        }
        [HttpGet("{id}")]
        public IActionResult GetThongTinTheDocGia(int id)
        {
            try
            {
                // Gọi service để lấy thông tin độc giả
                DTO_DocGia_TheDocGia thongTinDocGia = _theDocGiaService.GetById(id);

                if (thongTinDocGia == null)
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Không có dữ liệu của độc giả",
                        Data = null
                    });
                }

                return Ok(new APIResponse<DTO_DocGia_TheDocGia>()
                {
                    Success = true,
                    Message = "Lấy thông tin độc giả thành công",
                    Data = thongTinDocGia
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<object>()
                {
                    Success = false,
                    Message = $"Đã xảy ra lỗi: {ex.Message}",
                    Data = null
                });
            }
        }


        [HttpGet("{maThe}")]
        public IActionResult ValidatePhieuMuon(int maThe)
        {
            try
            {
                // Gọi service để xác thực phiếu mượn
                var validationMessages = _phieuMuonService.ValidatePhieuMuon(maThe);

                // Xử lý phản hồi dựa trên kết quả
                if (!string.IsNullOrEmpty(validationMessages))
                {
                    return Ok(new APIResponse<object>
                    {
                        Success = false,
                        Message = validationMessages,
                        Data = null
                    });
                }

                return Ok(new APIResponse<object>
                {
                    Success = true,
                    Message = "Lập phiếu mượn",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu có hệ thống logging)
                // _logger.LogError(ex, "Error validating PhieuMuon");

                // Trả về lỗi server
                return StatusCode(500, new APIResponse<object>
                {
                    Success = false,
                    Message = $"Lỗi server: {ex.Message}",
                    Data = null
                });
            }
        }


        [HttpGet("{maCuonSach}")]
        public IActionResult GetByMaCuonSach(string maCuonSach)
        {
            try
            {
                var bookDetails = _phieuMuonService.GetByMaCuonSach(maCuonSach);

                if (bookDetails == null)
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Không tìm thấy thông tin sách",
                        Data = null
                    });
                }

                return Ok(new APIResponse<BookDetailsDTO>()
                {
                    Success = true,
                    Message = "Lấy thông tin sách thành công",
                    Data = bookDetails
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<object>()
                {
                    Success = false,
                    Message = $"Đã xảy ra lỗi: {ex.Message}",
                    Data = null
                });
            }
        }

        /*[HttpGet("{maCuonSach}")]
        public ActionResult ThongTinTheDocGia(string maCuonSach)
        {
            try
            {
                BookDetailsDTO thongTinsach = _phieuMuonService.GetByMaCuonSach(maCuonSach);

                if (thongTinsach == null)
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Không có dữ liệu của độc giả",
                        Data = null
                    });
                }

                return Ok(new APIResponse<BookDetailsDTO>()
                {
                    Success = true,
                    Message = "Lấy thông tin độc giả thành công",
                    Data = thongTinsach
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

    }
}
