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
        /* public IActionResult Index()
         {
             return View();
         }*/


    }
}
