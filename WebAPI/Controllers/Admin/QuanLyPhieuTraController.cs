using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

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


    }
}
