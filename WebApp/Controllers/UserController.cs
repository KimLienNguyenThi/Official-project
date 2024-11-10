using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using WebApp.Responses;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/Client");
        private readonly HttpClient _client;

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Login(string phoneNumber, string password)
        {
            try
            {

                // Gửi yêu cầu GET và truyền dữ liệu
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/UserAuth/CheckUserLogin/{phoneNumber}/{password}").Result;


                // Kiểm tra mã trạng thái trả về từ API
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize dữ liệu trả về từ API
                    string data = await response.Content.ReadAsStringAsync();
                    LoginDG user = JsonConvert.DeserializeObject<LoginDG>(data);

                    // Tạo danh sách các claims cho người dùng
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Hoten),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("PhoneNumber", phoneNumber)
            };

                    // Tạo ClaimsIdentity và ClaimsPrincipal
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

                    // Đăng nhập người dùng và tạo phiên làm việc
                    await HttpContext.SignInAsync(claimsPrincipal);

                    return Json(new { success = true });
                }
                else
                {
                    // Nếu API trả về lỗi, trả về thông báo chi tiết
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"API Error: {errorMessage}" });
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


        //public async Task LoginByGoogle()
        //{
        //    await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
        //    {
        //        RedirectUri = Url.Action("GoogleResponse")
        //    });
        //}


        //public async Task<IActionResult> GoogleResponse()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
        //    {
        //        claim.Issuer,
        //        claim.OriginalIssuer,
        //        claim.Type,
        //        claim.Value
        //    });

        //    return RedirectToAction("ConfirmLoginGG", "User");
        //}


        //public IActionResult ConfirmLoginGG()
        //{
        //    var user = HttpContext.User;

        //    // Lấy thông tin về người dùng từ claims
        //    var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

        //    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/UserAuth/CheckUserLoginByGoogle/{userEmail}").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Add sđt vào Claim
        //        HttpResponseMessage respon = _client.GetAsync(_client.BaseAddress + $"/UserAuth/GetSdtByEmail/{userEmail}").Result;
        //        string data = respon.Content.ReadAsStringAsync().Result;

        //        // Lấy danh sách Claims hiện tại của người dùng
        //        var listClaimsUser = User.Claims.ToList();

        //        // Thêm Claim mới
        //        listClaimsUser.Add(new Claim("PhoneNumber", data));

        //        // Tạo danh sách Claims mới cho người dùng
        //        var newIdentity = new ClaimsIdentity(listClaimsUser, CookieAuthenticationDefaults.AuthenticationScheme);

        //        // Cập nhật danh sách Claims cho người dùng
        //        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(newIdentity));

        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        return View();

        //    }
        //}


        //[HttpPost]
        //public IActionResult ConfirmLoginGG(string phoneNumber)
        //{

        //    try
        //    {
        //        var user = HttpContext.User;

        //        var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;
        //        var userName = user.FindFirst(ClaimTypes.Name)?.Value;

        //        LoginDgDTO model = new LoginDgDTO()
        //        {
        //            Sdt = phoneNumber,
        //            PasswordDg = "null",
        //            Email = userEmail,
        //            HoTen = userName
        //        };

        //        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/UserAuth/Register", model).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Lấy danh sách Claims hiện tại của người dùng
        //            var listClaimsUser = User.Claims.ToList();

        //            // Thêm Claim mới
        //            listClaimsUser.Add(new Claim("PhoneNumber", phoneNumber));

        //            // Tạo danh sách Claims mới cho người dùng
        //            var newIdentity = new ClaimsIdentity(listClaimsUser, CookieAuthenticationDefaults.AuthenticationScheme);

        //            // Cập nhật danh sách Claims cho người dùng
        //            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(newIdentity));

        //            return Json(new { success = true });
        //        }
        //        else
        //        {
        //            return Json(new { success = false });
        //        }
        //    }
        //    catch
        //    {
        //        return Json(new { success = false });
        //    }
        //}



        //public IActionResult Register()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult Register(string phoneNumber, string password, string username, string email)
        //{
        //    try
        //    {
        //        LoginDgDTO model = new LoginDgDTO()
        //        {
        //            Sdt = phoneNumber,
        //            PasswordDg = password,
        //            Email = email,
        //            HoTen = username
        //        };

        //        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/UserAuth/Register", model).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return Json(new { success = true });
        //        }
        //        else
        //        {
        //            return Json(new { success = false });
        //        }
        //    }
        //    catch
        //    {
        //        return Json(new { success = false });
        //    }
        //}


        //[Authorize]
        //public async Task<IActionResult> Logout()
        //{
        //    ListSachMuon.listSachMuon.Clear();
        //    await HttpContext.SignOutAsync();
        //    return RedirectToAction("Index", "User");
        //}


        //[Authorize]
        //public IActionResult HistoryOfBorrowingBooks()
        //{
        //    try
        //    {
        //        List<DkiMuonSach> bookList = new List<DkiMuonSach>();
        //        var user = HttpContext.User;

        //        var sdt = user.FindFirst("PhoneNumber")?.Value;

        //        // gọi API lấy ra dữ liệu từ bảng DkiMuonSaches với sđt = sdt của user
        //        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/UserAuth/HistoryOfBorrowingBooks/{sdt}").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string data = response.Content.ReadAsStringAsync().Result;
        //            bookList = JsonConvert.DeserializeObject<List<DkiMuonSach>>(data);
        //        }
        //        // Kiểm tra nếu user có dữ liệu ở bảng DkiMuonSaches thì trả dữ liệu ra view
        //        if (bookList.Count > 0)
        //        {
        //            return View(bookList);
        //        }
        //        else
        //        {
        //            ViewBag.MessageData = "Không có dữ liệu";
        //            return View(bookList);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }

        //}

        //[HttpPut]
        //public async Task<IActionResult> CancelOrderBooks(int maDK)
        //{

        //    var requestUrl = new Uri(_client.BaseAddress + $"/UserAuth/CancelOrderBooks/{maDK}");

        //    using (var httpContent = new StringContent(string.Empty))
        //    {
        //        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        var response = await _client.PutAsync(requestUrl, httpContent);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return Json(new { success = true, message = "Đã hủy đơn thành công" });
        //        }
        //        else
        //        {
        //            return Json(new { success = false, message = "Hủy đơn thất bại" });
        //        }
        //    }
        //}

        //[HttpPost]
        //public IActionResult DetailsOrderBooks(int maDK)
        //{
        //    List<ChiTietDangKyDTO> bookList = new List<ChiTietDangKyDTO>();
        //    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/UserAuth/DetailsOrderBooks/{maDK}").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = response.Content.ReadAsStringAsync().Result;
        //        bookList = JsonConvert.DeserializeObject<List<ChiTietDangKyDTO>>(data);

        //        return Ok(bookList);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

    }
}
