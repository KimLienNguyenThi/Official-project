using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Service_Admin;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class ThongTinDocGiaController : ControllerBase
    {
        private readonly ThongTinDocGiaService _thongTinDocGiaService;
        private readonly TheDocGiaService _theDocGiaService;

        public ThongTinDocGiaController(ThongTinDocGiaService thongTinDocGiaService, TheDocGiaService theDocGiaService)
        {
            _thongTinDocGiaService = thongTinDocGiaService;
            _theDocGiaService = theDocGiaService;
        }


        [HttpGet("{id}")]
        public ActionResult ThongTinTheDocGia(int id)
        {
            try
            {
                DTO_DocGia_TheDocGia thongTinDocGia = _theDocGiaService.GetById(id);

                if(thongTinDocGia == null)
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
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public IActionResult UpdateThongTinDocGia([FromBody] DocGium obj)
        {
            try
            {
                if (_thongTinDocGiaService.UpdateThongTinDocGia(obj))
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = null
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Số điện thoại đã tồn tại",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
