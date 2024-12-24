using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class ThanhLySachController : Controller
    {
        private readonly QuanLyThuVienContext _context;
        private readonly IMapper _mapper;
        private readonly ThanhLySachService _thanhLySachService;


        public ThanhLySachController(IMapper mapper, QuanLyThuVienContext context, PhieuTraService phieuMuonService, ThanhLySachService thanhLySachService)
        {
            _context = context;
            _mapper = mapper;
            _thanhLySachService = thanhLySachService;
        }

        [HttpPost]
        public async Task<ActionResult<PagingResult<DonViTl>>> GetListDonViTLPaging_API([FromBody] GetListPhieuMuonPaging req)
        {
            var result = await _thanhLySachService.GetAllDonViTLPaging(req);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PagingResult<KhoSachThanhLyDTO>>> GetListKhoTLPaging_API([FromBody] GetListPhieuMuonPaging req)
        {
            var result = await _thanhLySachService.GetAllKhoTLPaging(req);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PagingResult<DTO_Sach_Nhap_Kho>>> GetListSachNhapPaging_API([FromBody] GetListPhieuMuonPaging req)
        {
            var result = await _thanhLySachService.GetAllSachNhapPaging(req);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult ThemDonViThanhLy_API([FromBody] DonViTl data)
        {
            var result = _thanhLySachService.Insertdonvi(data);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult ThemSachThanhLy_API([FromBody] SachNhapKhoDTO data)
        {

            var success = _thanhLySachService.Insertsach(data);
            if (success)
            {
                return Ok(new { success = true, message = "Thêm sách thành công." });

            }
            return BadRequest(new { success = false, message = "Thêm sách thất bại." });

        }


        [HttpPost]
        public ActionResult PhieuThanhLy_API(DTO_Tao_Phieu_TL data)
        {

            var success = _thanhLySachService.InsertPhieuThanhLy(data);
            if (success)
            { // Trả về phản hồi thành công

                return Ok(new { success = true, message = "Tạo phiếu thanh lý thành công." });

            }
            return BadRequest(new { success = false, message = "Tạo phiếu thanh lý thất bại." });


        }
    }
}
