using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class NhapSachService
    {
        private readonly QuanLyThuVienContext _context;
        private readonly GeneratePDFService _GeneratePDFService;
        public NhapSachService(QuanLyThuVienContext context,GeneratePDFService generatePDFService)
        {
            _context = context;
            _GeneratePDFService = generatePDFService;
        }

        public async Task<PagingResult<NhaCungCap>> GetAllNCCPaging(GetListPhieuMuonPaging req)
        {
            var query =
                 (from NhaCungCap in _context.NhaCungCaps
                  where string.IsNullOrEmpty(req.Keyword) || NhaCungCap.Tenncc.Contains(req.Keyword)  || NhaCungCap.Mancc.ToString().Contains(req.Keyword)
                  select new NhaCungCap
                  {
                      Mancc = NhaCungCap.Mancc,
                      Tenncc = NhaCungCap.Tenncc,
                      Diachincc = NhaCungCap.Diachincc,
                      Sdtncc = NhaCungCap.Sdtncc,
                  }).ToList();

            var totalRow = query.Count();

            var listNCCs = query.OrderBy(x => x.Mancc).Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToList();

            return new PagingResult<NhaCungCap>()
            {
                Results = listNCCs,
                CurrentPage = req.Page,
                RowCount = totalRow,
                PageSize = req.PageSize
            };
        }
        public async Task<PagingResult<Sach>> GetAllSachPaging(GetListPhieuMuonPaging req)
        {
            var query =
                (from SACH in _context.Saches
                
                 where string.IsNullOrEmpty(req.Keyword) || SACH.Tensach.Contains(req.Keyword)
                 select new Sach
                 {
                     Masach = SACH.Masach,
                     Tensach = SACH.Tensach,
                     Tacgia = SACH.Tacgia,
                     Theloai = SACH.Theloai,
                     Ngonngu = SACH.Ngonngu,
                     Nxb = SACH.Nxb,
                     Namxb = SACH.Namxb,
                     UrlImage = SACH.UrlImage,
                     // GiaSac = SACH.GiaSach, 
                     Soluonghientai = SACH.Soluonghientai

                 }
                 ).ToList();
            var totalRow = query.Count();

            var listSachs = query.OrderBy(x => x.Masach).Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToList();

            return new PagingResult<Sach>()
            {
                Results = listSachs,
                CurrentPage = req.Page,
                RowCount = totalRow,
                PageSize = req.PageSize
            };
        }
        public InsertRes InsertNCC(NhaCungCap obj)
        {
            var res = new InsertRes()
            {
                success = false,
                errorCode = -1,
                message = "Thêm đơn vị không thành công."
            };

            if (obj.Tenncc == "" || obj.Sdtncc == "" || obj.Diachincc == "")
            {
                return res;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingDonVi = _context.NhaCungCaps.FirstOrDefault(dv => dv.Sdtncc == obj.Sdtncc);

                    if (existingDonVi != null)
                    {
                        //throw new Exception("existingDonVi");
                        res.errorCode = -2;
                        res.message = "Số điện thoại đã tồn tại.";
                        return res;
                    }
                    else
                    {
                        var newNCC = new NhaCungCap();
                        {

                            newNCC.Mancc = obj.Mancc;
                            newNCC.Tenncc = obj.Tenncc;
                            newNCC.Diachincc = obj.Diachincc;
                            newNCC.Sdtncc = obj.Sdtncc;
                        };

                        _context.NhaCungCaps.Add(newNCC);

                        _context.SaveChanges();
                        transaction.Commit();

                        res.success = true;
                        res.message = "Thêm Thành công";
                        res.errorCode = 0;
                        return res;
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu có lỗi
                    Console.WriteLine($"Error: {ex.Message}");
                    // Xử lý lỗi và ghi log
                    return res;
                }
            }

        }
        public byte[] InsertPhieuNhap(DTO_Tao_Phieu_Nhap obj, List<string> imageUrls)
        {
            if (obj.listSachNhap.Any(sach => sach.SoLuong > 0) == false)
            {
                return null;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Lấy giá trị NamXBMax từ bảng QuyDinh
                    int namXBMax = _context.QuyDinhs.Select(qd => qd.NamXbmax).FirstOrDefault();
                    if (namXBMax == 0)
                    {
                        throw new Exception("Không tìm thấy quy định về năm xuất bản tối đa.");
                    }

                    var newPhieuNhap = new PhieuNhapSach
                    {
                        Ngaynhap = obj.NgayNhap,
                        Manv = obj.MaNhanVien,
                        Mancc = obj.MaNhaCungCap,
                    };

                    _context.PhieuNhapSaches.Add(newPhieuNhap);
                    _context.SaveChanges(); // Save to get the generated MaPn

                    // Khai báo currentYear ngay trước khi sử dụng
                    int currentYear = DateTime.Now.Year;

                    for (int i = 0; i < obj.listSachNhap.Count; i++)
                    {
                        var sachNhap = obj.listSachNhap[i];

                        // Sửa điều kiện kiểm tra năm xuất bản
                        if ((currentYear - sachNhap.NamXB) > namXBMax)
                        {
                            throw new Exception($"Sách '{sachNhap.TenSach}' có năm xuất bản quá cũ so với quy định ({namXBMax} năm trở lại).");
                        }

                        if (sachNhap.MaSach > 0)
                        {
                            var newChiTietPN = new Chitietpn
                            {
                                Mapn = newPhieuNhap.Mapn,
                                Masach = sachNhap.MaSach,
                                Giasach = sachNhap.GiaSach,
                                Soluongnhap = sachNhap.SoLuong,
                            };

                            _context.Chitietpns.Add(newChiTietPN);
                        }
                        else
                        {
                            // Use the URL from the list
                            string url = null;
                            if (i < imageUrls.Count)
                            {
                                url = imageUrls[i];
                            }
                            var newSach = new Sach
                            {
                                Tensach = sachNhap.TenSach,
                                Theloai = sachNhap.TheLoai,
                                Tacgia = sachNhap.TacGia,
                                Ngonngu = sachNhap.NgonNgu,
                                Nxb = sachNhap.NhaXB,
                                Namxb = sachNhap.NamXB,
                                Soluonghientai = 0,
                                Mota = sachNhap.MoTa,
                                UrlImage = url,
                            };

                            _context.Saches.Add(newSach);
                            _context.SaveChanges(); // Save to get the generated MaSach

                            var newChiTietPN = new Chitietpn
                            {
                                Mapn = newPhieuNhap.Mapn,
                                Masach = newSach.Masach,
                                Giasach = sachNhap.GiaSach,
                                Soluongnhap = sachNhap.SoLuong,
                            };

                            _context.Chitietpns.Add(newChiTietPN);
                        }
                    }

                    _context.SaveChanges(); // Save all changes

                    transaction.Commit();

                    // Kết hợp DTO hiện tại với MaPN để tạo PDF
                    int MaPhieuNhap = newPhieuNhap.Mapn;
                    var pdfData = _GeneratePDFService.GeneratePhieuNhapPDF(obj, MaPhieuNhap);

                    // Trả về dữ liệu PDF dưới dạng byte[]
                    return pdfData;
                }
                catch(Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu gặp lỗi
                    Console.WriteLine($"Error: {ex.Message}");
                    // Xử lý lỗi (ghi log hoặc thông báo)
                    return null; // Trả về null nếu lỗi
                }
            }
        }
        public int GetNamXBMax()
        {
            try
            {
                // Lấy giá trị NamXBMax từ bảng QuyDinh
                int namXBMax =  _context.QuyDinhs
                    .Select(qd => qd.NamXbmax)
                    .FirstOrDefault();

                return namXBMax;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Error: {ex.Message}");
                // Trả về giá trị mặc định nếu có lỗi
                return 0;
            }
        }

        public async Task<NhaCungCap> GetAllNCC(int mancc)
        {
            // Truy vấn để lấy nhà cung cấp với mancc cụ thể
            var ncc = await _context.NhaCungCaps
                                    .Where(n => n.Mancc == mancc)
                                    .Select(n => new NhaCungCap
                                    {
                                        Mancc = n.Mancc,
                                        Tenncc = n.Tenncc,
                                        Diachincc = n.Diachincc,
                                        Sdtncc = n.Sdtncc,
                                    })
                                    .FirstOrDefaultAsync(); // Lấy nhà cung cấp đầu tiên hoặc null nếu không tìm thấy

            return ncc;
        }
    }
}
