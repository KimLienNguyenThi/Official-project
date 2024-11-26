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

        [HttpPost]
        public IActionResult TaoPhieuTra_API([FromBody] DTO_Tao_Phieu_Tra data)
        {
            var result = _phieuTraService.Insert(data);
            if (result)
            {
                return Ok(new { success = true, message = "Tạo phiếu trả thành công." });
            }
            return BadRequest(new { success = false, message = "Tạo phiếu trả thất bại." });
        }
    }
}
