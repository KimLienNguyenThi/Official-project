using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services.Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class BangQuyDinhController : ControllerBase
    {
        private BangQuyDinhService _bangQuyDinhService;

        public BangQuyDinhController(BangQuyDinhService bangQuyDinhService)
        {
            _bangQuyDinhService = bangQuyDinhService;
        }

        [HttpGet]
        public IActionResult GetInfo()
        {
            try
            {
                var quyDinh = _bangQuyDinhService.GetInfo();

                if (quyDinh != null)
                {
                    return Ok(new APIResponse<QuyDinh>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = quyDinh
                    });
                }
                else
                {
                    return Ok(new APIResponse<QuyDinh>()
                    {
                        Success = false,
                        Message = "Lấy dữ liệu không thành công",
                        Data = null
                    });
                }
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public IActionResult UpdateRegulation([FromBody] QuyDinh quyDinh)
        {
            try
            {
                if(quyDinh == null)
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Dữ liệu rỗng!",
                        Data = null
                    });
                }
                else
                {
                    if (_bangQuyDinhService.UpdateRegulation(quyDinh))
                    {
                        return Ok(new APIResponse<object>()
                        {
                            Success = true,
                            Message = "Cập nhật thành công!",
                            Data = null
                        });
                    }
                    else
                    {
                        return Ok(new APIResponse<object>()
                        {
                            Success = false,
                            Message = "Cập nhật không thành công!",
                            Data = null
                        });
                    }
                }
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
        }


    }
}
