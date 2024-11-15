using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Service_Admin;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<NhanVien> nhanViens = _accountService.GetAll();

                if (nhanViens != null)
                {
                    return Ok(new APIResponse<List<NhanVien>>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = nhanViens
                    });
                }
                else
                {           
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Không tìm thấy dữ liệu trong database",
                        Data = null
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult GetAllNhanVien()
        {
            try
            {
                List<DTO_NhanVien_LoginNV> nhanViens = _accountService.GetAllNhanVien();

                if (nhanViens != null)
                {
                    return Ok(new APIResponse<List<DTO_NhanVien_LoginNV>>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = nhanViens
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Không tìm thấy dữ liệu trong database",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                DTO_NhanVien_LoginNV nhanViens = _accountService.GetById(id);

                if (nhanViens != null)
                {
                    return Ok(new APIResponse<DTO_NhanVien_LoginNV>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu nhân viên thành công",
                        Data = nhanViens
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Không có dữ liệu của nhân viên",
                        Data = null
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                DTO_NhanVien_LoginNV nhanViens = _accountService.Login(username, password);

                if (nhanViens != null)
                {
                    return Ok(new APIResponse<DTO_NhanVien_LoginNV>()
                    {
                        Success = true,
                        Message = "Đăng nhập thành công",
                        Data = nhanViens
                    });
                }
                else
                {
                    string checkLogin = _accountService.CheckLogin(username, password);

                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = checkLogin,
                        Data = null
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult GetBySDT(string sdt)
        {
            try
            {
                NhanVien nhanVien = _accountService.GetBySDT(sdt);

                if (nhanVien != null)
                {
                    return Ok(new APIResponse<NhanVien>()
                    {
                        Success = true,
                        Message = "Lấy dữ liệu thành công",
                        Data = nhanVien
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Không tìm thấy dữ liệu ở database",
                        Data = null
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] NhanVien obj)
        {
            try
            {
                if (_accountService.Insert(obj))
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = true,
                        Message = "Thêm dữ liệu thành công",
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


        [HttpPost]
        public async Task<IActionResult> ThemNhanVien([FromBody] DTO_NhanVien_LoginNV obj)
        {
            try
            {
                if (await _accountService.ThemNhanVien(obj))
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = true,
                        Message = "Thêm dữ liệu thành công",
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

        [HttpPost]
        public IActionResult UpdateThongTinNhanVien([FromBody] DTO_NhanVien_LoginNV obj)
        {
            try
            {
                if (_accountService.UpdateThongTinNhanVien(obj))
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = true,
                        Message = "Cập nhật dữ liệu thành công",
                        Data = null
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Số điện thoại hoặc tên đã tồn tại",
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