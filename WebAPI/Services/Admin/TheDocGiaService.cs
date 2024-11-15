using WebAPI.Models;
using WebAPI.Areas.Admin.Data;

namespace WebAPI.Services.Admin
{
    public class TheDocGiaService
    {
        private readonly QuanLyThuVienContext _context;

        public TheDocGiaService(QuanLyThuVienContext context)
        {
            _context = context;
        }

        public List<DTO_DocGia_TheDocGia> GetAllTheDocGia()
        {
            var listTheDocGia =
                (from DocGia in _context.DocGia
                 join TheDocGia in _context.TheDocGia
                    on DocGia.Madg equals TheDocGia.Madg
                 select new DTO_DocGia_TheDocGia
                 {
                     MaThe = TheDocGia.Mathe,
                     HoTenDG = DocGia.Hotendg,
                     GioiTinh = DocGia.Gioitinh,
                     NgaySinh = DocGia.Ngaysinh,
                     SDT = DocGia.Sdt,
                     DiaChi = DocGia.Diachi,
                     NgayDangKy = TheDocGia.Ngaydk,
                     NgayHetHan = TheDocGia.Ngayhh,
                     TienThe = (decimal)TheDocGia.Tienthe,
                 }
                 ).ToList();

            return listTheDocGia;
        }

        public bool Update(DTO_DocGia_TheDocGia obj)
        {
            try
            {
                var theDocGiaToUpdate = _context.TheDocGia.FirstOrDefault(t => t.Mathe == obj.MaThe);

                theDocGiaToUpdate.Ngayhh = obj.NgayHetHan;
                theDocGiaToUpdate.Tienthe = (int?)obj.TienThe;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update: {ex.Message}");
                return false;
            }
        }


        public bool DangKyTheDocGia(DTO_DocGia_TheDocGia obj)
        {
            try
            {
                var existingDocGia = _context.DocGia.FirstOrDefault(dg => dg.Sdt == obj.SDT);

                if (existingDocGia != null)
                {
                    return false;
                }
                else
                {
                    // Tạo một đối tượng DocGia mới
                    var newDocGia = new DocGium
                    {
                        Hotendg = obj.HoTenDG,
                        Gioitinh = obj.GioiTinh,
                        Ngaysinh = obj.NgaySinh,
                        Sdt = obj.SDT,
                        Diachi = obj.DiaChi,
                    };

                    // Thêm đối tượng DocGia mới vào DbSet DocGias
                    _context.DocGia.Add(newDocGia);
                    _context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu

                    // Tạo một đối tượng TheDocGia mới
                    var newTheDocGia = new TheDocGium
                    {
                        Madg = newDocGia.Madg, // Sử dụng MaDG từ đối tượng DocGia vừa thêm
                        Ngaydk = obj.NgayDangKy,
                        Ngayhh = obj.NgayHetHan,
                        Tienthe = (int)obj.TienThe,
                        Manv = obj.MaNhanVien,
                    };

                    // Thêm đối tượng TheDocGia mới vào DbSet TheDocGias
                    _context.TheDocGia.Add(newTheDocGia);
                    _context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong DangKyTheDocGia: {ex.Message}");
                throw new Exception();
            }
        }

        public DTO_DocGia_TheDocGia GetById(int id)
        {
            try
            {
                var DTO_DocGia_TheDocGia = (
                    from DocGia in _context.DocGia
                    join TheDocGia in _context.TheDocGia
                    on DocGia.Madg equals TheDocGia.Madg
                    where TheDocGia.Mathe == id
                    select new DTO_DocGia_TheDocGia
                    {
                        MaThe = TheDocGia.Mathe,
                        MaDocGia = DocGia.Madg,
                        MaNhanVien = (int)TheDocGia.Manv,
                        HoTenDG = DocGia.Hotendg,
                        SDT = DocGia.Sdt,
                        DiaChi = DocGia.Diachi,
                        GioiTinh = DocGia.Gioitinh,
                        NgaySinh = DocGia.Ngaysinh,
                        NgayDangKy = TheDocGia.Ngaydk,
                        NgayHetHan = TheDocGia.Ngayhh,
                        TienThe = (int)TheDocGia.Tienthe,
                    }).FirstOrDefault();

                return DTO_DocGia_TheDocGia;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetById: {ex.Message}");
                throw;
            }
        }
    }
}
