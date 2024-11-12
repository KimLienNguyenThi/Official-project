using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;
using WebAPI.DTOs.Client_DTO;
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


        [HttpPost]
        public IActionResult DangKyMuon([FromBody] DKMuon data)
        {
            var result = _borrowBookService.Insert(data);
            if (result)
            {
                return Ok(new { success = true, message = "Đăng ký sách mượn thành công." });
            }
            return BadRequest(new { success = false, message = "Đăng ký sách mượn thất bại." });
        }
    }
}
