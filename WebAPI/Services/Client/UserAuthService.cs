using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;
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

        public List<DkiMuonSach> GetHistoryOfBorrowingBooks(string sdt)
        {
            try
            {
                var dkiMuonSach = _context.DkiMuonSaches.Where(s => s.Sdt == sdt).ToList();

                if (dkiMuonSach == null || !dkiMuonSach.Any())
                {
                    throw new KeyNotFoundException("Không tìm thấy lịch sử mượn sách cho số điện thoại này.");
                }

                return dkiMuonSach;
            }
            catch (Exception ex)
            {
                // Ném ngoại lệ để controller bắt và xử lý
                throw new ApplicationException($"Lỗi khi truy xuất dữ liệu: {ex.Message}");
            }
        }

        public async Task CancelOrderBooksAsync(int maDK)
        {
            try
            {
                // Câu lệnh SQL để cập nhật dữ liệu
                var sql = "UPDATE DkiMuonSach SET Tinhtrang = @tinhTrang WHERE MaDk = @maDK";
                var parameters = new[]
                {
                new SqlParameter("@tinhTrang", 3), // Cập nhật trạng thái Tinhtrang thành 3
                new SqlParameter("@maDK", maDK)
            };

                // Thực thi câu lệnh SQL
                await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                // Ném ngoại lệ để controller bắt và xử lý
                throw new ApplicationException($"Lỗi khi hủy đơn mượn sách: {ex.Message}");
            }
        }
        public async Task<List<ChiTietDangKyDTO>> GetDetailsOrderBooksAsync(int maDK)
        {
            try
            {
                // Lấy danh sách chi tiết đăng ký theo mã đăng ký
                var details = await _context.ChiTietDks.Where(d => d.Madk == maDK).ToListAsync();

                // Danh sách để chứa các chi tiết đăng ký không có liên kết khoá ngoại
                var chiTietDkList = new List<ChiTietDangKyDTO>();

                foreach (var d in details)
                {
                    var chiTietDk = new ChiTietDangKyDTO
                    {
                        tenSach = _context.Saches.Find(d.Masach)?.Tensach,
                        chiTietDk = new ChiTietDk
                        {
                            Madk = d.Madk,
                            Masach = d.Masach,
                            Soluongmuon = d.Soluongmuon
                        }
                    };

                    chiTietDkList.Add(chiTietDk);
                }

                return chiTietDkList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Lỗi khi lấy chi tiết đơn mượn sách: {ex.Message}");
            }
        }
    }
}
