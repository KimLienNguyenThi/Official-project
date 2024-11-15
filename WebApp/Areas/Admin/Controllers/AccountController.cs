using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using WebApp.Areas.Admin.Data;
using Microsoft.AspNetCore.Authorization;
using WebApp.Admin.Data;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/account")]
    public class AccountController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public AccountController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [Route("")]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");
            else
                return RedirectToAction("Index", "Home");
        }


        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Account/Login/{username}/{password}");

            if (response.IsSuccessStatusCode)
            {
                string dataJson = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse<DTO_NhanVien_LoginNV>>(dataJson);

                if (apiResponse != null && apiResponse.Success)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, apiResponse.Data.HoTenNV),
                        new Claim(ClaimTypes.Role, apiResponse.Data.ChucVu),
                        new Claim("chucvu", apiResponse.Data.ChucVu),
                        new Claim("MaNV", apiResponse.Data.MaNV.ToString()),

                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "AdminCookie");
                    var claimsPrincial = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync("AdminCookie", claimsPrincial);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = apiResponse.Message;
                    return View();
                }

            }
            else
            {
                TempData["error"] = await response.Content.ReadAsStringAsync();
                return View();
            }
        }

        [Route("Logout")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> Logout()
        {
            // Xóa phiên đăng nhập
            await HttpContext.SignOutAsync("AdminCookie");

            return RedirectToAction("Login");
        }
    }
}