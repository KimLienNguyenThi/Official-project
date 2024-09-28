using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using WebApp.Areas.Admin.Data;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            // check database
            DTO_NhanVien_LoginNV login = new DTO_NhanVien_LoginNV();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Account/Login/{username}/{password}").Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                login = JsonConvert.DeserializeObject<DTO_NhanVien_LoginNV>(dataJson);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, login.HoTenNV),
                    new Claim(ClaimTypes.Role, login.ChucVu),
                    new Claim("chucvu", login.ChucVu),
                    new Claim("MaNV", login.MaNV.ToString()),

                };


                var claimsIdentity = new ClaimsIdentity(claims, "AdminCookie");
                var claimsPrincial = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("AdminCookie", claimsPrincial);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Tài khoản hoặc mật khẩu không chính xác!";
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
