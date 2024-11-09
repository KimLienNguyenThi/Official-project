using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using X.PagedList;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using WebApp.Responses;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
       
        Uri baseAddress = new Uri("https://localhost:7028/api/Client");
        private readonly HttpClient _client;

        public BookController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        public async Task<IActionResult> Index(int? page)
        {
            List<dynamic> bookList = new List<dynamic>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Book/GetAllBooks").Result;

            // Ghi lại thông tin về trạng thái phản hồi
            Debug.WriteLine($"Response Status Code: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Response Data: {data}"); // Ghi lại nội dung trả về

                bookList = JsonConvert.DeserializeObject<List<dynamic>>(data);
                Debug.WriteLine($"Number of books retrieved: {bookList.Count}");
            }
            else
            {
                Debug.WriteLine("Failed to retrieve books.");
            }

            // Cài đặt phân trang
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var pagedBooks = bookList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)bookList.Count / pageSize);

            return View(pagedBooks);
        }



        [HttpPost]
        public async Task<IActionResult> GetBookByName(string tenSach)
        {
            List<GetBookByNameResDto> bookList = new List<GetBookByNameResDto>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Book/GetBookByName/{Uri.EscapeDataString(tenSach)}").Result;

            Debug.WriteLine($"Response Status Code: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Response Data: {data}"); // Ghi lại nội dung trả về

                bookList = JsonConvert.DeserializeObject<List<GetBookByNameResDto>>(data) ?? bookList;
                Debug.WriteLine($"Number of books retrieved: {bookList.Count}");
            }
            else
            {
                Debug.WriteLine("Failed to retrieve books.");
            }

            Debug.WriteLine(JsonConvert.SerializeObject(bookList));

            return Ok(new { success = true, sachList = bookList });
        }


        [HttpPost]
        public async Task<IActionResult> GetBookByCategory(string ngonNgu, string theLoai, string namXB)
        {
          
            List<GetBookByNameResDto> bookList = new List<GetBookByNameResDto>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/Book/GetBookByCategory/{ngonNgu}/{theLoai}/{namXB}").Result;

            Debug.WriteLine($"Response Status Code: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Response Data: {data}"); // Ghi lại nội dung trả về

                bookList = JsonConvert.DeserializeObject<List<GetBookByNameResDto>>(data) ?? bookList;
                Debug.WriteLine($"Number of books retrieved: {bookList.Count}");
            }
            else
            {
                Debug.WriteLine("Failed to retrieve books.");
            }

            Debug.WriteLine(JsonConvert.SerializeObject(bookList));

            return Ok(new { success = true, sachList = bookList });
            

        }


    }
}
