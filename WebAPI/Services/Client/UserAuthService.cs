using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Services.Client
{
    public class UserAuthService
    {
        private readonly QuanLyThuVienContext _context;

        private readonly IMapper _mapper;
        public UserAuthService(IMapper mapper, QuanLyThuVienContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> CheckUserLogin(string phoneNumber, string password)
        {
            try
            {
                var loginDg = await _context.LoginDgs
                .FirstOrDefaultAsync(u => u.Sdt == phoneNumber);

                if (loginDg == null || loginDg.PasswordDg != password)
                {
                    return (IActionResult)Results.NotFound("Thông tin đăng nhập không hợp lệ.");
                }

                return new OkObjectResult(loginDg);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Error: {ex.Message}");

                // Trả về mã lỗi cho phía client
                return new StatusCodeResult(500);
            }

        }
    }
}
