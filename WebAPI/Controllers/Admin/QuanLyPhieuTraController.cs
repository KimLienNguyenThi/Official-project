using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Helpers;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class QuanLyPhieuTraController : ControllerBase
    {

        private readonly QuanLyPhieuTraService _qlphieuTraService;
        private readonly QuanLyThuVienContext _context;
        private readonly IMapper _mapper;
        public QuanLyPhieuTraController(IMapper mapper, QuanLyThuVienContext context, QuanLyPhieuTraService qlphieuTraService)
        {
            _context = context;
            _mapper = mapper;
            _qlphieuTraService = qlphieuTraService;

        }


        [HttpPost]
        public async Task<ActionResult<PagingResult<PhieuTra_GroupMaPM_DTO>>> GetListPhieuTraPaging_API([FromBody] GetListPhieuTraPaging req)
        {
            var phieuTra = await _qlphieuTraService.GetAllPhieuTraPaging(req);

            return Ok(phieuTra);
        }


        [HttpGet("{maPM}")]
        public async Task<ActionResult<DTO_Sach_Tra>> Get_ChiTietPT_ByMaPM_API(int maPM)
        {
            try
            {
                if (maPM != 0)
                {
                    var listCTPT = _qlphieuTraService.Get_ChiTietPT_ByMaPM(maPM);
                    List<DTO_Sach_Tra> sachDtos = _mapper.Map<List<DTO_Sach_Tra>>(listCTPT);
                    return Ok(sachDtos);
                }
                else
                {
                    return NotFound(new { success = false, message = "Không tìm thấy sách nào phù hợp." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{maPT}")]
        public async Task<ActionResult<DTO_Sach_Tra>> Get_ChiTietPT_ByMaPT_API(int maPT)
        {
            try
            {
                if (maPT != 0)
                {
                    var listCTPT = _qlphieuTraService.Get_CTPT_ByMaPT(maPT);
                    List<DTO_Sach_Tra> sachDtos = _mapper.Map<List<DTO_Sach_Tra>>(listCTPT);
                    return Ok(sachDtos);
                }
                else
                {
                    return NotFound(new { success = false, message = "Không tìm thấy sách nào phù hợp." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{maPT}/{maThe}")]
        public async Task<IActionResult> TaoHoaDon_API(int maPT, int maThe)
        {
            try
            {
                // Gọi service để tạo hóa đơn và nhận về file PDF dưới dạng byte[]
                var pdfData = _qlphieuTraService.TaoHoaDonbyMapt(maPT, maThe);

                if (pdfData != null && pdfData.Length > 0) // Kiểm tra dữ liệu PDF
                {
                    // Tạo tên file PDF
                    string fileName = $"PhieuTra_{Guid.NewGuid()}.pdf";

                    // Thiết lập response để trả về file PDF
                    return File(pdfData, "application/pdf", fileName);
                }

                // Nếu không thành công, trả về lỗi
                return BadRequest(new { success = false, message = "Tạo phiếu trả thất bại." });
            }
            catch (Exception ex)
            {
                // Bắt lỗi và trả về thông báo lỗi
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi tạo phiếu trả.", error = ex.Message });
            }
        }
       
    }
}
