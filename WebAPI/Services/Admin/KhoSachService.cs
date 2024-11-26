using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class KhoSachService
    {
        private readonly QuanLyThuVienContext _context;

        public KhoSachService(QuanLyThuVienContext context)
        {
            _context = context;
        }
        public async Task<PagingResult<Sach>> GetAllSachPaging(GetListPhieuTraPaging req)
        {
            var query =
                (from SachDTO in _context.Saches
                
                 where string.IsNullOrEmpty(req.Keyword) || SachDTO.Tensach.Contains(req.Keyword) || SachDTO.Theloai.Contains(req.Keyword) ||
                  SachDTO.Ngonngu.Contains(req.Keyword) ||
                  SachDTO.Tacgia.Contains(req.Keyword)
                 select new Sach
                 {
                     Masach = SachDTO.Masach,
                     Tensach = SachDTO.Tensach,
                     Tacgia = SachDTO.Tacgia,
                     Theloai = SachDTO.Theloai,
                     Ngonngu = SachDTO.Ngonngu,
                     Namxb = SachDTO.Namxb,
                     Nxb = SachDTO.Nxb,
                     Soluonghientai = SachDTO.Soluonghientai,
                     Mota = SachDTO.Mota
                 });


            var totalRow = await query.CountAsync();

            var listsachs = await query.OrderBy(x => x.Masach)
                                       .Skip((req.Page - 1) * req.PageSize)
                                       .Take(req.PageSize)
                                       .ToListAsync();

            return new PagingResult<Sach>
            {
                Results = listsachs,
                CurrentPage = req.Page,
                RowCount = totalRow,
                PageSize = req.PageSize
            };
        }
    }
}
