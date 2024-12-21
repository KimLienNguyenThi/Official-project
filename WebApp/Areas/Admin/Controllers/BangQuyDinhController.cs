using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebApp.Admin.Data;
using WebApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/BangQuyDinh")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]

    public class BangQuyDinhController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api/admin");
        private readonly HttpClient _client;

        public BangQuyDinhController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    return RedirectToAction("LoiPhanQuyen", "phanquyen");
                }
                else
                {
                    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/BangQuyDinh/GetInfo");

                    if (response.IsSuccessStatusCode)
                    {
                        string dataJson = response.Content.ReadAsStringAsync().Result;
                        var apiResponse = JsonConvert.DeserializeObject<APIResponse<QuyDinh>>(dataJson);

                        if (apiResponse != null && apiResponse.Success)
                        {
                            return View(apiResponse.Data);
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
            }
            catch
            {
                return View();
            }
        }

        [Route("UpdateRegulation")]
        [HttpPut]
        public async Task<IActionResult> UpdateRegulation(int nunbOfBorrowBooks, int numbOfDays, int maxYearOfPublication, string token)
        {
            try
            {
                var regulation = new QuyDinh()
                {
                    SosachmuonMax = nunbOfBorrowBooks,
                    NamXbmax = maxYearOfPublication,
                    SongayMax = numbOfDays
                };

                // gửi kèm Token
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.PutAsJsonAsync(_client.BaseAddress + "/BangQuyDinh/UpdateRegulation", regulation);

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
                        return Json(new { success = false, message = apiResponse!.Message });
                    }
                }
                else
                {
                    return Json(new { success = false, message = await response.Content.ReadAsStringAsync() });
                }
            }
            catch (Exception ex) {
                return Json(new { success = false, message = $"Xin lỗi! Hiện hệ thống đang tạm thời xảy ra lỗi {ex.Message}" });

            }
        }
    }
}
