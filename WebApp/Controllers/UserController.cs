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
using WebApp.Models;
using WebApp.Admin.Data;

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
                if (string.IsNullOrEmpty(phoneNumber)) 
                {
                    return Json(new { success = false, message = "Tài khoản không được để trống!" });
                }else if(string.IsNullOrEmpty(password))
                {
                    return Json(new { success = false, message = "Mật khẩu không được để trống!" });
                }

                // Gửi yêu cầu GET và truyền dữ liệu
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/UserAuth/CheckUserLogin/{phoneNumber}/{password}").Result;


                // Kiểm tra mã trạng thái trả về từ API
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize dữ liệu trả về từ API
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<LoginDG>>(dataJson);

                    if (apiResponse != null && apiResponse.Success)
                    {
                        // Tạo danh sách các claims cho người dùng
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, apiResponse.Data.Hoten),
                            new Claim(ClaimTypes.Email, apiResponse.Data.Email),
                            new Claim("PhoneNumber", phoneNumber)
                        };

                        // Tạo ClaimsIdentity và ClaimsPrincipal
                        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

                        // Đăng nhập người dùng và tạo phiên làm việc
                        await HttpContext.SignInAsync(claimsPrincipal);

                        return Json(new { success = true, message = apiResponse.Message });
                    }
                    else
                    {
                        return Json(new { success = false, message = apiResponse.Message });
                    }
                }
                else
                {
                    // Nếu API trả về lỗi, trả về thông báo chi tiết
                    return Json(new { success = false, message = await response.Content.ReadAsStringAsync() });
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        public async Task LoginByGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }


        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            return RedirectToAction("ConfirmLoginGG", "User");
        }

        public IActionResult ConfirmLoginGG()
        {
            var user = HttpContext.User;

            // Lấy thông tin về người dùng từ claims
            var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/UserAuth/CheckUserLoginByGoogle/{userEmail}").Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse<object>>(dataJson);

                if (apiResponse != null && apiResponse.Success)
                {
                    // Add sđt vào Claim
                    HttpResponseMessage responseSdt = _client.GetAsync(_client.BaseAddress + $"/UserAuth/GetSdtByEmail/{userEmail}").Result;
                    string dataJson2 = responseSdt.Content.ReadAsStringAsync().Result;
                    var apiResponse2 = JsonConvert.DeserializeObject<APIResponse<object>>(dataJson2);

                    if (apiResponse2 != null && apiResponse2.Success)
                    {
                        // Lấy danh sách Claims hiện tại của người dùng
                        var listClaimsUser = User.Claims.ToList();

                        // Thêm Claim mới
                        listClaimsUser.Add(new Claim("PhoneNumber", apiResponse2.Message));

                        // Tạo danh sách Claims mới cho người dùng
                        var newIdentity = new ClaimsIdentity(listClaimsUser, CookieAuthenticationDefaults.AuthenticationScheme);

                        // Cập nhật danh sách Claims cho người dùng
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(newIdentity));

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public IActionResult ConfirmLoginGG(string phoneNumber)
        {

            try
            {
                var user = HttpContext.User;

                var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;
                var userName = user.FindFirst(ClaimTypes.Name)?.Value;

                LoginDgDTO model = new LoginDgDTO()
                {
                    Sdt = phoneNumber,
                    PasswordDg = "null",
                    Email = userEmail,
                    HoTen = userName
                };

                HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/UserAuth/Register", model).Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<object>>(dataJson);

                    if (apiResponse != null && apiResponse.Success)
                    {
                        // Lấy danh sách Claims hiện tại của người dùng
                        var listClaimsUser = User.Claims.ToList();

                        // Thêm Claim mới
                        listClaimsUser.Add(new Claim("PhoneNumber", phoneNumber));

                        // Tạo danh sách Claims mới cho người dùng
                        var newIdentity = new ClaimsIdentity(listClaimsUser, CookieAuthenticationDefaults.AuthenticationScheme);

                        // Cập nhật danh sách Claims cho người dùng
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(newIdentity));

                        return Json(new { success = true, message = apiResponse.Message });
                    }
                    else
                    {
                        return Json(new { success = false, message = apiResponse.Message });
                    }
                }
                else
                {
                    return Json(new { success = false, message = response.Content.ReadAsStringAsync() });
                }
            }
            catch
            {
                return Json(new { success = false });
            }
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(string phoneNumber, string password, string username, string email)
        {
            try
            {
                LoginDgDTO model = new LoginDgDTO()
                {
                    Sdt = phoneNumber,
                    PasswordDg = password,
                    Email = email,
                    HoTen = username
                };

                HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/UserAuth/Register", model).Result;

                if (response.IsSuccessStatusCode)
                {
                    string dataJson = response.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<APIResponse<object>>(dataJson);

                    if (apiResponse != null && apiResponse.Success)
                    {
                        return Json(new { success = true, message = apiResponse.Message });
                    }
                    else
                    {
                        return Json(new { success = false, message = apiResponse.Message });
                    }
                }
                else
                {
                    return Json(new { success = false, message = response.Content.ReadAsStringAsync() });
                }
            }
            catch
            {
                return Json(new { success = false, message = "Có lỗi xảy ra" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailRegister(string phoneNumber, string username, string email)
        {
            var infoUser = new
            {
                phoneNumber = phoneNumber,
                username = username,
                userEmail = email,
            };

            HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/UserAuth/SendEMmail_Register", infoUser).Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse<object>>(dataJson);

                if (apiResponse != null && apiResponse.Success)
                {
                    return Json(new { success = true, message = apiResponse.Message });
                }
                else
                {
                    return Json(new { success = false, message = apiResponse.Message });
                }
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = responseContent });
            }
        }

        [HttpGet]
        public IActionResult CheckEmailRegister(int otp)
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/UserAuth/CheckEMmail_Register/{otp}").Result;

            if (response.IsSuccessStatusCode)
            {
                string dataJson = response.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonConvert.DeserializeObject<APIResponse<object>>(dataJson);

                if (apiResponse != null && apiResponse.Success)
                {
                    return Json(new { success = true, message = apiResponse.Message });
                }
                else
                {
                    return Json(new { success = false, message = apiResponse.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = response.Content.ReadAsStringAsync() });
            }
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            ListSachMuon.listSachMuon.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "User");
        }


        [Authorize]
        public IActionResult HistoryOfBorrowingBooks()
        {
            try
            {
                List<DkiMuonSach> bookList = new List<DkiMuonSach>();
                var user = HttpContext.User;

                var sdt = user.FindFirst("PhoneNumber")?.Value;

                // gọi API lấy ra dữ liệu từ bảng DkiMuonSaches với sđt = sdt của user
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/UserAuth/HistoryOfBorrowingBooks/{sdt}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    bookList = JsonConvert.DeserializeObject<List<DkiMuonSach>>(data);
                }
                // Kiểm tra nếu user có dữ liệu ở bảng DkiMuonSaches thì trả dữ liệu ra view
                if (bookList.Count > 0)
                {
                    return View(bookList);
                }
                else
                {
                    ViewBag.MessageData = "Không có dữ liệu";
                    return View(bookList);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> CancelOrderBooks(int maDK)
        {
            try
            {
                // Tạo request URL
                string requestUrl = _client.BaseAddress + $"/UserAuth/CancelOrderBooks/{maDK}";

                // Tạo nội dung trống để gửi đi với PUT request
                using (var httpContent = new StringContent(string.Empty))
                {
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Gửi PUT request
                    HttpResponseMessage response = await _client.PutAsync(requestUrl, httpContent);

                    // Kiểm tra kết quả trả về
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true, message = "Đã hủy đơn thành công" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Hủy đơn thất bại" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult DetailsOrderBooks(int maDK)
        {
            List<ChiTietDangKyDTO> bookList = new List<ChiTietDangKyDTO>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/UserAuth/DetailsOrderBooks/{maDK}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                bookList = JsonConvert.DeserializeObject<List<ChiTietDangKyDTO>>(data);

                return Ok(bookList);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
