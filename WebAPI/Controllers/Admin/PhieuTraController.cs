using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class PhieuTraController : Controller
    {
       
        private readonly PhieuTraService _phieuTraService;
       
        public PhieuTraController( PhieuTraService phieuTraService)
        {
           
            _phieuTraService = phieuTraService;
        }

        [HttpPost]
        public async Task<ActionResult<PagingResult<PhieuMuonDTO>>> GetListPhieuMuonPaging_API([FromBody] GetListPhieuMuonPaging req)
        {
            var result = await _phieuTraService.GetAllPhieuMuonPaging(req);
            return Ok(result);
        }
    }
}
