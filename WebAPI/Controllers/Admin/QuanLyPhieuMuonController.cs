using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class QuanLyPhieuMuonController : Controller
    {


        private readonly QuanLyPhieuMuonService _qlphieuMuonService;
        private readonly QuanLyThuVienContext _context;
        private readonly IMapper _mapper;
        public QuanLyPhieuMuonController(IMapper mapper, QuanLyThuVienContext context, QuanLyPhieuMuonService qlphieuMuonService)
        {
            _context = context;
            _mapper = mapper;
            _qlphieuMuonService = qlphieuMuonService;

        }

        [HttpPost]
        public async Task<ActionResult<PagingResult<PhieuMuon_GroupMaDG_DTO>>> GetListDG_PhieuMuonPaging_API([FromBody] GetListPhieuTraPaging req)
        {
            try
            {
                var docgia = await _qlphieuMuonService.GetAllDocGiaPaging(req);
                return Ok(docgia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{maThe}")]
        public async Task<ActionResult<SachMuon_allPmDTO>> Get_ChiTietPM_ByMaDG_API(int maThe)
        {
            try
            {
                if (maThe != 0)
                {
                    var listCTPM = _qlphieuMuonService.Get_ChiTietPM_ByMaDG(maThe);
                    List<SachMuon_allPmDTO> sachDtos = _mapper.Map<List<SachMuon_allPmDTO>>(listCTPM);
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

        [HttpGet("{maPM}")]
        public async Task<ActionResult<SachMuon_allPmDTO>> Get_ChiTietPM_ByMaPM_API(int maPM)
        {
            try
            {
                if (maPM != 0)
                {
                    var listCTPM = _qlphieuMuonService.Get_ChiTietPM_ByMaPM(maPM);
                    List<SachMuon_allPmDTO> sachDtos = _mapper.Map<List<SachMuon_allPmDTO>>(listCTPM);
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
    

