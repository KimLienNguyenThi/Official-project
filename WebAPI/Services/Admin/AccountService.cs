using WebAPI.Areas.Admin.Data;
using WebAPI.Models;

namespace WebAPI.Service_Admin
{
    public class AccountService
    {
        private readonly QuanLyThuVienContext _context;

        public AccountService(QuanLyThuVienContext context)
        {
            _context = context;
        }


        public List<NhanVien> GetAll()
        {
            return _context.NhanViens.ToList();
        }

        public List<DTO_NhanVien_LoginNV> GetAllNhanVien()
        {
            var listNhanVien =
                (from NhanVien in _context.NhanViens
                 select new DTO_NhanVien_LoginNV
                 {
                     MaNV = NhanVien.Manv,
                     HoTenNV = NhanVien.Hotennv,
                     GioiTinh = NhanVien.Gioitinh,
                     DiaChi = NhanVien.Diachi,
                     NgaySinh = NhanVien.Ngaysinh,
                     SDT = NhanVien.Sdt,
                     ChucVu = NhanVien.Chucvu,
                 }
                 ).ToList();
            return listNhanVien;
        }

        public DTO_NhanVien_LoginNV GetById(int id)
        {
            try
            {
                var DTO_NhanVien_LoginNV = (
                    from NhanVien in _context.NhanViens
                    join LOGIN_NV in _context.LoginNvs
                    on NhanVien.Manv equals LOGIN_NV.Manv
                    where NhanVien.Manv == id
                    select new DTO_NhanVien_LoginNV
                    {
                        MaNV = NhanVien.Manv,
                        HoTenNV = NhanVien.Hotennv,
                        NgaySinh = NhanVien.Ngaysinh,
                        GioiTinh = NhanVien.Gioitinh,
                        DiaChi = NhanVien.Diachi,
                        ChucVu = NhanVien.Chucvu,
                        SDT = NhanVien.Sdt,
                        Username = LOGIN_NV.UsernameNv,
                        Password = LOGIN_NV.PasswordNv,
                    }).FirstOrDefault();

                return DTO_NhanVien_LoginNV;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetById: {ex.Message}");
                throw;
            }
        }

        public DTO_NhanVien_LoginNV Login(string username, string password)
        {
            try
            {
                 var nhanVien =
                 (from nv in _context.NhanViens
                  join lg in _context.LoginNvs
                     on nv.Manv equals lg.Manv
                  where lg.UsernameNv == username && password == lg.PasswordNv
                  select new DTO_NhanVien_LoginNV
                  {
                      MaNV = nv.Manv,
                      HoTenNV = nv.Hotennv,
                      SDT = nv.Sdt,
                      ChucVu = nv.Chucvu,
                      DiaChi = nv.Diachi
                  }).FirstOrDefault();

                  return nhanVien;
            }
            catch(Exception ex)
            {
                throw;
            }

        }

        public string CheckLogin(string username, string password)
        {
            try
            {
                if (_context.LoginNvs.Any(x => x.UsernameNv == username))
                {
                    return "Mật khẩu không chính xác";
                }
                else
                {
                    return "Tài khoản không tồn tại";
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public NhanVien GetBySDT(string sdt)
        {
            var data = _context.NhanViens.FirstOrDefault(nv => nv.Sdt == sdt);

            if(data != null)
            {
                return data;
            }
            else
            {
                return new NhanVien();
            }
        }

        public bool Insert(NhanVien obj)
        {
            // kiem tra sdt co trung hay khong
            var data = _context.NhanViens.FirstOrDefault(nv => nv.Sdt == obj.Sdt);

            if (data == null)
            {
                _context.NhanViens.Add(obj);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(NhanVien nhanvien)
        {
            var data = _context.NhanViens.FirstOrDefault(nv => nv.Sdt == nhanvien.Sdt);
            if(data != null )
            {
                _context.NhanViens.Update(nhanvien);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }


        }

        public async Task<bool> ThemNhanVien(DTO_NhanVien_LoginNV obj)
        {
            try
            {
                var existingSDT = _context.NhanViens.FirstOrDefault(nv => nv.Sdt == obj.SDT);

                if (existingSDT != null)
                {
                    return false;
                }
                else
                {
                    // Tạo một đối tượng nhan viên mới
                    var newNhanVien = new NhanVien
                    {
                        Hotennv = obj.HoTenNV,
                        Sdt = obj.SDT,
                        Gioitinh = obj.GioiTinh,
                        Ngaysinh = obj.NgaySinh,
                        Diachi = obj.DiaChi,
                        Chucvu = obj.ChucVu,
                    };

                    _context.NhanViens.Add(newNhanVien);
                    await _context.SaveChangesAsync();


                    // Mã hóa mật khẩu với bcrypt
                    //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(obj.Password, 12);

                    //Tạo một đối tượng loginNV mới
                    var newLoginNV = new LoginNv
                    {
                        Manv = newNhanVien.Manv,
                        UsernameNv = obj.Username,
                        PasswordNv = obj.Password,
                    };

                    _context.LoginNvs.Add(newLoginNV);
                    await _context.SaveChangesAsync();

                    return true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong ThemNhanVien: {ex.Message}");
                throw;
            }
        }

        public string Delete(int id)
        {
            var nvRemove = _context.NhanViens.Find(id);

            if (nvRemove != null)
            {
                _context.NhanViens.Remove(nvRemove);
                return "Xoá thành công";
            }
            else
            {
                return "Xoá không thành công";

            }
        }

        public bool UpdateThongTinNhanVien(DTO_NhanVien_LoginNV obj)
        {
            try
            {
                // Kiểm tra sdt và userName có trùng với nhân viên nào khác hay không
                var existingSDT = _context.NhanViens.FirstOrDefault(nv => nv.Sdt == obj.SDT && nv.Manv  != obj.MaNV);
                var existingUsername = _context.LoginNvs.FirstOrDefault(login => login.UsernameNv == obj.Username && login.Manv != obj.MaNV);

                if (existingSDT != null)
                {
                    //throw new Exception("Số điện thoại đã tồn tại.");
                    return false;
                }
                else
                {
                    if (existingUsername != null)
                    {
                        //throw new Exception("Username đã tồn tại.");
                        return false;
                    }
                    else
                    {
                        var nhanVien = _context.NhanViens.FirstOrDefault(t => t.Manv == obj.MaNV);
                        nhanVien.Hotennv = obj.HoTenNV;
                        nhanVien.Sdt = obj.SDT;
                        nhanVien.Ngaysinh = obj.NgaySinh;
                        nhanVien.Gioitinh = obj.GioiTinh;
                        nhanVien.Diachi = obj.DiaChi;
                        nhanVien.Chucvu = obj.ChucVu;

                        var login = _context.LoginNvs.FirstOrDefault(t => t.Manv == obj.MaNV);

                        if (obj.Password.Trim() == "")
                        {
                            login.UsernameNv = obj.Username;
                            // Lưu thay đổi vào cơ sở dữ liệu
                            _context.SaveChanges();

                            return true;
                        }
                        else
                        {

                            login.UsernameNv = obj.Username;
                            login.PasswordNv = obj.Password;

                            _context.SaveChanges();

                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update: {ex.Message}");
                throw;
            }
        }
    }
}
