using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class BangQuyDinhService
    {
        private readonly QuanLyThuVienContext _context;

        public BangQuyDinhService(QuanLyThuVienContext context)
        {
            _context = context;
        }

        public QuyDinh GetInfo()
        {
            return _context.QuyDinhs.FirstOrDefault()!;
        }

        public bool UpdateRegulation(QuyDinh quyDinh)
        {
            try
            {
                _context.QuyDinhs.Update(quyDinh);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
