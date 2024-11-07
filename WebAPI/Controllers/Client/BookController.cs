using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services.Client;

namespace WebAPI.Controllers.Client
{
    [Route("api/Client/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAll();
                return Ok(books);  // ASP.NET Core sẽ tự động serialize danh sách thành JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{tenSach}")]
        public async Task<IActionResult> GetBookByName( string tenSach)
        {
            try
            {
                var sachLoc = await _bookService.GetBookByName(tenSach);

                if (sachLoc.Any())
                {
                    return Ok(sachLoc);
                }
                else
                {
                    return NotFound(new { Message = "Không tìm thấy sách nào phù hợp với bộ lọc." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { Message = "Đã xảy ra lỗi khi tìm kiếm sách." });
            }
        }


        [HttpGet("{ngonNgu}/{theLoai}/{namXB}")]
        public async Task<IActionResult> GetBookByCategory(string ngonNgu, string theLoai, string namXB)
        {

            try
            {
                var sachLoc = await _bookService.GetBookByCategory(ngonNgu, theLoai, namXB);

                if (sachLoc.Any())
                {
                    return Ok(sachLoc);
                }
                else
                {
                    return NotFound(new { Message = "Không tìm thấy sách nào phù hợp với bộ lọc." });
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { Message = "Đã xảy ra lỗi khi tìm kiếm sách." });
            }
            // Gọi phương thức trong dịch vụ
           

        }

    }
 }
