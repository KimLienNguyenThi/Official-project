using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class PhieuMuonService
    {
        private readonly QuanLyThuVienContext _context;

        public PhieuMuonService(QuanLyThuVienContext context)
        {
            _context = context;
        }


        // Lấy phiếu mượn trong số ngày gần đây dựa vào songayMax trong bảng quydinh
        public List<PhieuMuon> GetPhieuMuonInLastDay(int maThe)
        {
            // Truy vấn songayMax từ bảng quydinh
            var songayMax = _context.QuyDinhs.FirstOrDefault()?.SongayMax ?? 0;
            var currentDate = DateTime.Now.Date; // Ngày hiện tại
            var startDate = currentDate.AddDays(-songayMax); // Ngày bắt đầu tính

            return _context.Set<PhieuMuon>()
                .Where(pm => pm.Mathe == maThe
                             && pm.Ngaymuon.HasValue
                             && pm.Ngaymuon >= DateOnly.FromDateTime(startDate)
                             && pm.Ngaymuon <= DateOnly.FromDateTime(currentDate))
                .ToList();
        }


        //Kiểm tra số lượng mượn đã được 5 cuốn chưa cho một mã thẻ

        // Kiểm tra số lượng mượn đã đạt 5 cuốn chưa
        public bool HasBorrowedFiveBooks(List<PhieuMuon> phieuMuons)
        {
            var maPhieuMuons = phieuMuons.Select(pm => pm.Mapm).ToList();

            var ctPM = _context.ChiTietPms
                .Where(ct => maPhieuMuons.Contains(ct.Mapm))
                .ToList();

            var borrowedBooksCount = ctPM.Sum(ct => ct.Soluongmuon ?? 0); // Tính tổng số lượng sách mượn

            return borrowedBooksCount >= 5;
        }


        // Kiểm tra tình trạng phiếu mượn và hạn trả
        public string CheckReturnStatusAndLimit(int maThe)
        {
            // Lấy tất cả phiếu mượn của mã thẻ
            var borrowedBooks = _context.Set<PhieuMuon>()
                .Where(pm => pm.Mathe == maThe && pm.Tinhtrang == false)
                .ToList();

            foreach (var pm in borrowedBooks)
            {
                // Kiểm tra hạn trả
                if (pm.Hantra.HasValue && pm.Hantra.Value < DateOnly.FromDateTime(DateTime.Now))
                {
                    return $"Phiếu mượn {pm.Mapm} đã hết hạn trả";
                }
            }

            return "";
        }
        // Kiểm tra và xác thực phiếu mượn
        public string ValidatePhieuMuon(int maThe)
        {
            // Lấy danh sách phiếu mượn dựa vào số ngày quy định
            var phieuMuon = GetPhieuMuonInLastDay(maThe);

            // Kiểm tra số lượng sách đã mượn
            if (HasBorrowedFiveBooks(phieuMuon))
            {
                return "Đã mượn quá 5 cuốn sách.";
            }
            else
            {
                // Kiểm tra tình trạng và hạn trả
                var returnStatus = CheckReturnStatusAndLimit(maThe);
                if (!string.IsNullOrEmpty(returnStatus))
                {
                    return returnStatus;
                }
            }

            return "";
        }


        public BookDetailsDTO GetByMaCuonSach(string maCuonSach)
        {
            try
            {
                var bookDetailsDTO = (
                    from cuonSach in _context.CuonSaches
                    join sach in _context.Saches
                    on cuonSach.Masach equals sach.Masach
                    where cuonSach.Macuonsach == maCuonSach // Thêm điều kiện lọc
                    select new BookDetailsDTO
                    {
                        MaCuonSach = cuonSach.Macuonsach,
                        TenSach = sach.Tensach,
                        MaSach = cuonSach.Masach
                    }).FirstOrDefault();

                return bookDetailsDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByMaCuonSach: {ex.Message}");
                throw;
            }
        }

    }


}

 

