using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services.Client;

namespace WebAPI.Controllers.Client
{
    [Route("api/Client/[controller]/[action]")]
    [ApiController]
    public class BorrowBookController : Controller
    {
        private readonly BorrowBookService _borrowBookService;

        public BorrowBookController(BorrowBookService borrowBookService)
        {
            _borrowBookService = borrowBookService;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooksForBorrow([FromQuery] int[] maSach)
        {
            try
            {
                var sachMuon = await _borrowBookService.GetBooksForBorrow(maSach);

                if (sachMuon != null && sachMuon.Count > 0)
                {
                    return Ok(sachMuon); // Trả về danh sách sách
                }
                else
                {
                    return Ok(new List<Sach>()); // Trả về danh sách rỗng nếu không tìm thấy
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Trả về lỗi nếu có lỗi
            }
        }



        //[HttpGet]
        //public async Task<IActionResult> GetAllBooks()
        //{
        //    try
        //    {
        //        var books = await _bookService.GetAll();
        //        return Ok(books);  // ASP.NET Core sẽ tự động serialize danh sách thành JSON
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("{tenSach}")]
        //public async Task<IActionResult> GetBookByName(string tenSach)
        //{
        //    try
        //    {
        //        var sachLoc = await _bookService.GetBookByName(tenSach);

        //        if (sachLoc.Any())
        //        {
        //            return Ok(sachLoc);
        //        }
        //        else
        //        {
        //            return NotFound(new { Message = "Không tìm thấy sách nào phù hợp với bộ lọc." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Ghi log lỗi nếu cần
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(500, new { Message = "Đã xảy ra lỗi khi tìm kiếm sách." });
        //    }
        //}


        //[HttpGet("{ngonNgu}/{theLoai}/{namXB}")]
        //public async Task<IActionResult> GetBookByCategory(string ngonNgu, string theLoai, string namXB)
        //{

        //    try
        //    {
        //        var sachLoc = await _bookService.GetBookByCategory(ngonNgu, theLoai, namXB);

        //        if (sachLoc.Any())
        //        {
        //            return Ok(sachLoc);
        //        }
        //        else
        //        {
        //            return NotFound(new { Message = "Không tìm thấy sách nào phù hợp với bộ lọc." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Ghi log lỗi nếu cần
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(500, new { Message = "Đã xảy ra lỗi khi tìm kiếm sách." });
        //    }
        //    // Gọi phương thức trong dịch vụ


        //}




    }
}
