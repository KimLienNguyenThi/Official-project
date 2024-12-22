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
                // Lấy bản ghi đầu tiên trong bảng QuyDinhs
                var regulation = _context.QuyDinhs.First();
                if (regulation == null)
                {
                    return false; // Không tìm thấy bản ghi
                }

                // Cập nhật các thuộc tính
                regulation.NamXbmax = quyDinh.NamXbmax;
                regulation.SosachmuonMax = quyDinh.SosachmuonMax;
                regulation.SongayMax = quyDinh.SongayMax;
                // Cập nhật thêm các thuộc tính khác theo yêu cầu

                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); // Ghi log lỗi
                return false;
            }
        }


    }
}
