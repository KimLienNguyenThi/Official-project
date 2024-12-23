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

        

        [HttpPost]
        public IActionResult Insert([FromBody] DTO_Tao_Phieu_Muon data)
        {
            try
            {
                var CheckSL = _phieuMuonService.CheckSoLuongSach(data);
                if(CheckSL != null && CheckSL.Length > 0)
                {
                    return BadRequest( CheckSL ); 
                }

                else {
                    // Gọi service để tạo phiếu trả và nhận về file PDF dưới dạng byte[]
                    var pdfData = _phieuMuonService.InsertAndGeneratePDF(data);

                    if (pdfData != null && pdfData.Length > 0) // Kiểm tra dữ liệu PDF
                    {
                        // Tạo tên file PDF
                        string fileName = $"PhieuMuon_{Guid.NewGuid()}.pdf";

                        // Thiết lập response để trả về file PDF
                        return File(pdfData, "application/pdf", fileName);
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Tạo file PDF không thành công!" });
                    }
                }
               
                

                
            }
            catch (Exception ex)
            {
                // Bắt lỗi và trả về thông báo lỗi
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi tạo phiếu mượn.", error = ex.Message });
            }
        }

    }
}
