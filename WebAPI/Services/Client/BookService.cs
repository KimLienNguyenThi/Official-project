using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WebAPI.Services.Client
{
    public class BookService
    {
        private readonly QuanLyThuVienContext _context;

        private readonly IMapper _mapper;

        public BookService(IMapper mapper, QuanLyThuVienContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Sach>> GetAll()
        {
            return await _context.Saches.ToListAsync();
        }


        public async Task<List<Sach>> GetBookByName(string tenSach)
        {
            try
            {
                // Loại bỏ khoảng trắng ở đầu và cuối
                tenSach = tenSach?.Trim();

                // Tìm kiếm sách theo tên hoặc tác giả
                var sachLoc = await _context.Saches
                    .Where(item => item.Tensach.Contains(tenSach) || item.Tacgia.Contains(tenSach))
                    .ToListAsync();

                // Trả về danh sách sách phù hợp (có thể là rỗng nếu không có sách phù hợp)
                return sachLoc;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần và trả về một danh sách trống để báo lỗi phía client
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Sach>(); // Trả về danh sách trống khi có lỗi
            }
        }


        public async Task<List<Sach>> GetBookByCategory(string ngonNgu, string theLoai, string namXB)
        {
            try
            {
                var sachLoc = _context.Saches.AsQueryable();

                if (ngonNgu != "All")
                {
                    sachLoc = sachLoc.Where(m => m.Ngonngu == ngonNgu);
                }

                if (theLoai != "All")
                {
                    sachLoc = sachLoc.Where(m => m.Theloai == theLoai);
                }

                if (namXB != "All")
                {
                    if (int.TryParse(namXB, out int namXBValue))
                    {
                        sachLoc = sachLoc.Where(m => m.Namxb == namXBValue);
                    }
                    else
                    {
                        // Bạn có thể throw một exception nếu cần thiết
                        throw new ArgumentException("Giá trị năm xuất bản không hợp lệ.");
                    }
                }

                // Thực hiện truy vấn và trả về danh sách
                return await sachLoc.ToListAsync();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Sach>(); // Trả về danh sách rỗng khi có lỗi


            }
        }
    }
}
