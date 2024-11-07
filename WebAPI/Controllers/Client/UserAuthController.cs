using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WebAPI.Services.Client;

namespace WebAPI.Controllers.Client
{
    [Route("api/Client/[controller]/[action]")]
    [ApiController]
    public class UserAuthController : Controller
    {
        private readonly UserAuthService _userAuthService;

        public UserAuthController(UserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        [HttpGet("{phoneNumber}/{password}")]
        public async Task<IActionResult> CheckUserLogin(string phoneNumber, string password)
        {
            try
            {
                var result = await _userAuthService.CheckUserLogin(phoneNumber, password);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");

                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu.");
            }
        }
    }
}
