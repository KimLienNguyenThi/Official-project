using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPI.DTOs;
using WebAPI.Services.Client;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.Responses;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers.Client
{
    [Route("api/Client/[controller]/[action]")]
    [ApiController]
    public class UserAuthController : Controller
    {
        private readonly UserAuthService _userAuthService;
        private readonly JwtService _jwtService;
        private static int OTP_email;


        public UserAuthController(UserAuthService userAuthService, JwtService jwtService)
        {
            _userAuthService = userAuthService;
            _jwtService = jwtService;
        }

        [HttpPost()]
        public async Task<IActionResult> GetTokenJWT(LoginDg model)
        {
            if (model == null)
            {
                return Ok(new APIResponse<LoginResponseModel>()
                {
                    Success = false,
                    Message = "Thông tin rỗng!",
                    Data = null
                });
            }
            else
            {
                return Ok(new APIResponse<LoginResponseModel>()
                {
                    Success = true,
                    Message = "Cấp Token thành công!",
                    Data = await _jwtService.CreateTokenUser(model)
                });
            }
        }

        [HttpGet("{phoneNumber}/{password}")]
        public async Task<IActionResult> CheckUserLogin(string phoneNumber, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(phoneNumber))
                {
                    return Ok(new APIResponse<LoginResponseModel>()
                    {
                        Success = false,
                        Message = "Tài khoản không được để trống!",
                        Data = null
                    });
                }
                else if (string.IsNullOrEmpty(password))
                {
                    return Ok(new APIResponse<LoginResponseModel>()
                    {
                        Success = false,
                        Message = "Mật khẩu không được để trống!",
                        Data = null
                    });
                }
                else
                {
                    var result = await _userAuthService.CheckUserLogin(phoneNumber, password);

                    if (result == null)
                    {
                        return Ok(new APIResponse<LoginResponseModel>()
                        {
                            Success = false,
                            Message = "Tài khoản không tồn tại!",
                            Data = null
                        });
                    }
                    else if (result.PasswordDg != password)
                    {
                        return Ok(new APIResponse<LoginResponseModel>()
                        {
                            Success = false,
                            Message = "Mật khẩu không đúng!",
                            Data = null
                        });
                    }
                    else
                    {
                        return Ok(new APIResponse<LoginResponseModel>()
                        {
                            Success = true,
                            Message = "Xác thực thành công!",
                            Data = await _jwtService.CreateTokenUser(result)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");

                return BadRequest("Đã xảy ra lỗi khi xử lý yêu cầu.");
            }
        }

        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> GetInfoLogin(string phoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phoneNumber))
                    return Ok(new APIResponse<LoginDg>()
                    {
                        Success = false,
                        Message = "Số điện thoại trống!!!",
                        Data = null,
                    });
                else
                {
                    var result = await _userAuthService.GetInfoLogin(phoneNumber);

                    if (result == null)
                    {
                        return Ok(new APIResponse<LoginDg>()
                        {
                            Success = true,
                            Message = "Không tìm thấy tài khoản!",
                            Data = null,
                        });
                    }
                    else
                    {
                        return Ok(new APIResponse<LoginDg>()
                        {
                            Success = true,
                            Message = "Đăng nhập thành công!",
                            Data = result,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserAuthentication newRegister)
        {
            try
            {
                if (newRegister == null)
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Thông tin người dùng không hợp lệ",
                        Data = null
                    });
                }

                if (await _userAuthService.Register(newRegister))
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = true,
                        Message = "Đăng ký thành công",
                        Data = null
                    });
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Không thể thực hiện lưu trữ ở server",
                        Data = null
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPost]
        public async Task<IActionResult> SendEMmail_Register([FromBody] JsonElement infoUser)
        {
            try
            {
                if (infoUser.ValueKind == JsonValueKind.Object)
                {
                    if (await _userAuthService.SendEMmail_Register(infoUser) == "Ok")
                    {
                        return Ok(new APIResponse<object>()
                        {
                            Success = true,
                            Message = "Ok",
                            Data = null
                        });
                    }
                    else
                    {
                        return Ok(new APIResponse<object>()
                        {
                            Success = false,
                            Message = await _userAuthService.SendEMmail_Register(infoUser),
                            Data = null
                        });
                    }

                }
                return Ok(new APIResponse<object>()
                {
                    Success = false,
                    Message = "infoUser xảy ra lỗi",
                    Data = null
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{otp}")]
        public async Task<IActionResult> CheckEMmail_Register(int otp)
        {
            try
            {
                if (otp != 0)
                {
                    if (_userAuthService.CheckEMmail_Register(otp))
                    {
                        return Ok(new APIResponse<object>()
                        {
                            Success = true,
                            Message = "Xác thực thành công!",
                            Data = null
                        });
                    }
                    else
                    {
                        return Ok(new APIResponse<object>()
                        {
                            Success = false,
                            Message = "Mã OTP sai!!!",
                            Data = null
                        });
                    }
                }
                else
                {
                    return Ok(new APIResponse<object>()
                    {
                        Success = false,
                        Message = "Mã OTP không hợp lệ!",
                        Data = null
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{email}")]
        public async Task<IActionResult> CheckUserLoginByGoogle(string email)
        {
            if (email != null)
            {
                if (_userAuthService.CheckUserLoginByGoogle(email))
                {
                    var phoneNumber = _userAuthService.GetSdtByEmail(email);
                    var loginDG = _userAuthService.GetInfoLogin(phoneNumber);

                    return Ok(new APIResponse<LoginResponseModel>()
                    {
                        Success = true,
                        Message = "Email đã tồn tại trong hệ thống",
                        Data = await _jwtService.CreateTokenUser(await loginDG)
                    });
                }
                else
                {
                    return Ok(new APIResponse<LoginResponseModel>()
                    {
                        Success = false,
                        Message = "Không tìm thấy email trong hệ thống",
                        Data = null
                    });
                }

            }
            else
            {
                return Ok(new APIResponse<LoginResponseModel>()
                {
                    Success = false,
                    Message = "Email không hợp lệ",
                    Data = null
                });
            }
        }


        [HttpGet("{userEmail}")]
        public IActionResult GetSdtByEmail(string userEmail)
        {
            if (userEmail != null)
            {
                return Ok(new APIResponse<object>()
                {
                    Success = true,
                    Message = _userAuthService.GetSdtByEmail(userEmail),
                    Data = null
                });
            }
            else
            {
                return Ok(new APIResponse<object>()
                {
                    Success = false,
                    Message = "Email không hợp lệ",
                    Data = null
                });
            }
        }

        [HttpGet("{sdt}")]
        public IActionResult HistoryOfBorrowingBooks(string sdt)
        {
            try
            {
                var dkiMuonSach = _userAuthService.GetHistoryOfBorrowingBooks(sdt);
                return Ok(dkiMuonSach);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Đã xảy ra lỗi hệ thống.", Details = ex.Message });
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPut("{maDK}")]
        public async Task<IActionResult> CancelOrderBooks(int maDK)
        {
            try
            {
                await _userAuthService.CancelOrderBooksAsync(maDK);
                return Ok(new { Message = "Đơn mượn sách đã được hủy thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Hủy đơn mượn sách thất bại.", Details = ex.Message });
            }
        }

        [HttpGet("{maDK}")]
        public async Task<IActionResult> DetailsOrderBooks(int maDK)
        {
            try
            {
                var chiTietDkList = await _userAuthService.GetDetailsOrderBooksAsync(maDK);
                return Ok(chiTietDkList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Lỗi khi lấy chi tiết đơn mượn sách.", Details = ex.Message });
            }
        }
    }
}

