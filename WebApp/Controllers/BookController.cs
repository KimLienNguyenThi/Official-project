using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.DTOs;
using WebApp.Models;
using X.PagedList;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7028/api");
        private readonly HttpClient _client;

        public BookController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        public IActionResult Index(int? page)
        {
            List<SachDTO> bookList = new List<SachDTO>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Book/GetAllBook").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                bookList = JsonConvert.DeserializeObject<List<SachDTO>>(data);
            }

            int pageSize = 9;
            int pageNumber = (page ?? 1);
            IPagedList<SachDTO> pagedListSach = bookList.ToPagedList(pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = pagedListSach.PageCount;

            return View(pagedListSach);
        }

        [HttpPost]
        public IActionResult SearchBook(string tenSach)
        {

            List<SachDTO> bookList = new List<SachDTO>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Book/GetBookByName/{tenSach}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<dynamic>(data);
                bookList = responseObject.sachList.ToObject<List<SachDTO>>();
            }

            return Ok(new { success = true, sachList = bookList });
        }


        [HttpPost]
        public IActionResult GetBookByCategory(string ngonNgu, string theLoai, string namXB)
        {

            try
            {
                List<SachDTO> bookList = new List<SachDTO>();

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Book/GetBookByCategory/{ngonNgu}/{theLoai}/{namXB}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(data);
                    bookList = responseObject.sachList.ToObject<List<SachDTO>>();
                    return Ok(new { success = true, sachList = bookList });
                }
                else
                {
                    // Handle non-success status code
                    return BadRequest(new { success = false, message = "Failed to retrieve data from API." });
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, new { success = false, message = ex.Message });
            }

        }


    }
}
