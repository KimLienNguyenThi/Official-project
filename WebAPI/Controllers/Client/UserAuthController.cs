using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using WebAPI.DTOs;
using WebAPI.Services.Client;

namespace WebAPI.Controllers.Client
{
    [Route("api/Client/[controller]/[action]")]
    [ApiController]
    public class UserAuthController : Controller
    {
        private readonly UserAuthService _userAuthService;

        public UserAuthController(UserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        [HttpGet("{phoneNumber}/{password}")]
        public async Task<IActionResult> CheckUserLogin(string phoneNumber, string password)
        {
            try
            {
                var result = await _userAuthService.CheckUserLogin(phoneNumber, password);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");

                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu.");
            }
        }

        [HttpGet("{sdt}")]
        public IActionResult HistoryOfBorrowingBooks(string sdt)
        {
            try
            {
                var dkiMuonSach = _userAuthService.GetHistoryOfBorrowingBooks(sdt);
                return Ok(dkiMuonSach);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Đã xảy ra lỗi hệ thống.", Details = ex.Message });
            }
        }

        [HttpPut("{maDK}")]
        public async Task<IActionResult> CancelOrderBooks(int maDK)
        {
            try
            {
                await _userAuthService.CancelOrderBooksAsync(maDK);
                return Ok(new { Message = "Đơn mượn sách đã được hủy thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Hủy đơn mượn sách thất bại.", Details = ex.Message });
            }
        }

        [HttpGet("{maDK}")]
        public async Task<IActionResult> DetailsOrderBooks(int maDK)
        {
            try
            {
                var chiTietDkList = await _userAuthService.GetDetailsOrderBooksAsync(maDK);
                return Ok(chiTietDkList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Lỗi khi lấy chi tiết đơn mượn sách.", Details = ex.Message });
            }
        }

    }
}

