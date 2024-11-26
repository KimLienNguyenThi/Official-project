using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class KhoSachController : ControllerBase
    {
        private readonly KhoSachService _sachService;

        private readonly QuanLyThuVienContext _context;
        private readonly IMapper _mapper;

        public KhoSachController(IMapper mapper, QuanLyThuVienContext context, KhoSachService sachService)
        {
            _context = context;
            _mapper = mapper;
            _sachService = sachService;

        }
        [HttpPost]
        public async Task<ActionResult<PagingResult<Sach>>> GetListSachPaging_API([FromBody] GetListPhieuTraPaging req)
        {
            var sach = await _sachService.GetAllSachPaging(req);
            return Ok(sach);
        }

    }
}
