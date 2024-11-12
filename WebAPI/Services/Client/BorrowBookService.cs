using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs;
using WebAPI.DTOs.Client_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Client
{

    public class BorrowBookService
    {
        private readonly QuanLyThuVienContext _context;

        private readonly IMapper _mapper;

        public BorrowBookService(IMapper mapper, QuanLyThuVienContext context)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<Sach>> GetBooksForBorrow(int[] maSach)
        {
            try
            {
                // Lấy danh sách sách theo danh sách mã sách
                var sachLoc = await _context.Saches
                    .Where(s => maSach.Contains(s.Masach))
                    .ToListAsync();

                // Sử dụng mapper để chuyển đổi sang DTO nếu cần
                var sachMuon = _mapper.Map<List<Sach>>(sachLoc);

                return sachMuon; // Trả về danh sách sách được tìm thấy
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching books: {ex.Message}", ex);
            }
        }

        public bool Insert(DKMuon x)
        {
            if (x.ListSach.Any(sach => sach.Soluongmuon > 0 ) == false)
            {
                return false;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tạo đối tượng dăng ký sách mượn từ DKMuon
                    var newDK = new DkiMuonSach
                    {
                        Sdt = x.Sdt,
                        Ngaydkmuon = x.Ngaydkmuon,
                        Ngayhen = x.Ngayhen,
                        Tinhtrang = x.Tinhtrang,
                    };

                    // Thêm PhieuTra vào Context
                    _context.DkiMuonSaches.Add(newDK);
                    _context.SaveChanges(); // Lưu để có thể lấy MaPT của newPhieuTra

                    // Duyệt qua danh sách sách trả và tạo đối tượng ChiTietPT cho mỗi cuốn sách
                    foreach (var sachdki in x.ListSach)
                    {
                        if (sachdki.Soluongmuon == 0 )
                        {
                            continue;
                        }
                        var newChiTietDKi = new ChiTietDk
                        {
                            Madk = newDK.Madk,
                            Masach = sachdki.MaSach,

                            Soluongmuon = sachdki.Soluongmuon,
                           
                        };

                        // Thêm ChiTietPT vào Context
                        _context.ChiTietDks.Add(newChiTietDKi);
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu khi mọi thứ đã thành công
                    _context.SaveChanges();

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu có lỗi
                    Console.WriteLine($"Error: {ex.Message}");
                    // Xử lý lỗi và ghi log
                    return false;
                }
            }
        }

    }
}
