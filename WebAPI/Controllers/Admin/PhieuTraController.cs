using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class PhieuTraController : Controller
    {

        private readonly QuanLyThuVienContext _context;
        private readonly IMapper _mapper;
        private readonly PhieuTraService _phieuTraService;

        public PhieuTraController(IMapper mapper, QuanLyThuVienContext context, PhieuTraService phieuTraService)
        {
            _context = context;
            _mapper = mapper;
            _phieuTraService = phieuTraService;
        }

        [HttpPost]
        public async Task<ActionResult<PagingResult<PhieuMuonDTO>>> GetListPhieuMuonPaging_API([FromBody] GetListPhieuMuonPaging req)
        {
            var result = await _phieuTraService.GetAllPhieuMuonPaging(req);
            return Ok(result);
        }
        [HttpGet("{maPM}")]
        public async Task<ActionResult<SachMuonDTO>> GetSachMuon_API(int maPM)
        {
            try
            {
                if (maPM != 0)
                {
                    // Nếu phieumuon tồn tại, tiếp tục thực hiện các thao tác khác
                    var sachMuon = _phieuTraService.getSachMuon(maPM);
                    List<SachMuonDTO> sachDtos = _mapper.Map<List<SachMuonDTO>>(sachMuon);
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

        //[HttpPost]
        //public IActionResult TaoPhieuTra_API([FromBody] DTO_Tao_Phieu_Tra data)
        //{
        //    var result = _phieuTraService.Insert(data);

        //    if (result.HasValue) // Kiểm tra xem Mapt có được trả về không (không phải null)
        //    {
        //        return Ok(new { success = true, message = "Tạo phiếu trả thành công.", mapt = result.Value });
        //    }

        //    return BadRequest(new { success = false, message = "Tạo phiếu trả thất bại." });
        //}


        //[HttpPost]
        //public IActionResult TaoPhieuTra_API([FromBody] DTO_Tao_Phieu_Tra data)
        //{
        //    try
        //    {
        //        // Gọi service để tạo phiếu trả và tạo file PDF
        //        var pdfData = _phieuTraService.InsertAndGeneratePDF(data);

        //        if (pdfData != null) // Nếu file PDF được tạo thành công
        //        {
        //            return File(pdfData, "application/pdf", "PhieuTra.pdf");
        //        }

        //        // Nếu không thành công, trả về lỗi
        //        return BadRequest(new { success = false, message = "Tạo phiếu trả thất bại." });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Bắt lỗi và trả về thông báo lỗi
        //        return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi tạo phiếu trả.", error = ex.Message });
        //    }
        //}

        [HttpPost]
        public IActionResult TaoPhieuTra_API([FromBody] DTO_Tao_Phieu_Tra data)
        {
            try
            {
                // Gọi service để tạo phiếu trả và nhận về file PDF dưới dạng byte[]
                var pdfData = _phieuTraService.InsertAndGeneratePDF(data);

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
