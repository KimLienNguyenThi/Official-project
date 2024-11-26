using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class TheDocGiaController : ControllerBase
    {
        private readonly TheDocGiaService _theDocGiaService;

        public TheDocGiaController(TheDocGiaService theDocGiaService)
        {
            _theDocGiaService = theDocGiaService;
        }

        [HttpGet]
        public IActionResult GetAllTheDocGia()
        {
            try
            {
                List<DTO_DocGia_TheDocGia> listTheDocGia = _theDocGiaService.GetAllTheDocGia();

                return Ok(new APIResponse<List<DTO_DocGia_TheDocGia>>()
                {
                    Success = true,
                    Message = "Lấy dữ liệu thành công",
                    Data = listTheDocGia
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult ThongTinTheDocGia(int id)
        {
            try
            {
                DTO_DocGia_TheDocGia thongTinDocGia = _theDocGiaService.GetById(id);

                if (thongTinDocGia != null)
                {
                    return Ok(new APIResponse<DTO_DocGia_TheDocGia>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = thongTinDocGia
                    });
                }
                else
                {
                    return Ok(new APIResponse<DTO_DocGia_TheDocGia>()
                    {
                        Success = false,
                        Message = "Không tìm thấy độc giả",
                        Data = thongTinDocGia
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult Update([FromBody] DTO_DocGia_TheDocGia obj)
        {
            try
            {
                if (_theDocGiaService.Update(obj))
                    return Ok();
                else
                    return BadRequest("Không cập nhật được! Lỗi từ server");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult DangKyTheDocGia([FromBody] DTO_DocGia_TheDocGia obj)
        {
            try
            {
                var tdg = _theDocGiaService.DangKyTheDocGia(obj);

                if (tdg != null)
                {
                    return Ok(new APIResponse<DTO_DocGia_TheDocGia>()
                    {
                        Success = true,
                        Message = "Đăng ký thành công",
                        Data = tdg 
                    });
                }
                else
                {
                    return Ok(new APIResponse<DTO_DocGia_TheDocGia>()
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
