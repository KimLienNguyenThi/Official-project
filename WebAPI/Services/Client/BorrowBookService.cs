using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
    }
}
