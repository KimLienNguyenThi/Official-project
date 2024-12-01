using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebAPI.Content;
using WebAPI.DTOs;
using WebAPI.Helper;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Services.Client
{
    public class UserAuthService
    {
        private readonly QuanLyThuVienContext _context;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private static int OTP_email;

        public UserAuthService(IMapper mapper, QuanLyThuVienContext context, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<LoginDg> CheckUserLogin(string phoneNumber, string password)
        {
            try
            {
                return await _context.LoginDgs.FirstOrDefaultAsync(u => u.Sdt == phoneNumber);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> Register(UserAuthentication newRegister)
        {
            try
            {
                LoginDg register = new LoginDg()
                {
                    Sdt = newRegister.Sdt,
                    PasswordDg = newRegister.PasswordDg,
                    Email = newRegister.Email,
                    Hoten = newRegister.HoTen,
                };

                _context.LoginDgs.Add(register);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckEMmail_Register(int otp)
        {
            try
            {
                if (otp == OTP_email)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }


        [HttpPost]
        public async Task<string> SendEMmail_Register(JsonElement infoUser)
        {
            try
            {
                if (_context.LoginDgs.Any(e => e.Sdt == infoUser.GetProperty("phoneNumber").GetString()))
                {
                    return "Số điện thoại đã tồn tại!";
                }
                else if (_context.LoginDgs.Any(e => e.Email == infoUser.GetProperty("userEmail").GetString()))
                {
                    return "Email đã tồn tại!";
                }

                // Truy cập thông tin email và các thông tin khác
                string userEmail = infoUser.GetProperty("userEmail").GetString(); // Đảm bảo tên thuộc tính đúng
                string username = infoUser.GetProperty("username").GetString(); // Truy cập tên người dùng

                // tạo các đối tượng sendEmail
                var email = new SendEmailRegister();
                Random rd = new Random();
                int random = rd.Next(100000, 1000000);
                OTP_email = random;

                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = userEmail;
                mailRequest.Subject = "Xác thực đăng ký tài khoản";
                mailRequest.Body = email.SendEmail_Register(random, username);
                await _emailService.SendEmailAsync(mailRequest);

                return "Ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        public bool CheckUserLoginByGoogle(string email)
        {
            if (_context.LoginDgs.Any(e => e.Email == email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string GetSdtByEmail(string userEmail)
        {
            try
            {
                LoginDg user = _context.LoginDgs.FirstOrDefault(s => s.Email == userEmail);

                if (user == null)
                {
                    return "Không tìm thấy số điện thoại tương ứng";
                }
                else
                {
                    return user.Sdt;
                }
            }
            catch
            {
                return "Đã xảy ra lỗi ở phía server";
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
