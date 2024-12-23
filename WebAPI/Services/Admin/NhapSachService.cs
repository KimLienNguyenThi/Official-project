using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Helper;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class NhapSachService
    {
        private readonly QuanLyThuVienContext _context;
        private readonly GeneratePDFService _GeneratePDFService;
        public NhapSachService(QuanLyThuVienContext context, GeneratePDFService generatePDFService)
        {
            _context = context;
            _GeneratePDFService = generatePDFService;
        }

        public async Task<PagingResult<NhaCungCap>> GetAllNCCPaging(GetListPhieuMuonPaging req)
        {
            var query =
                 (from NhaCungCap in _context.NhaCungCaps
                  where string.IsNullOrEmpty(req.Keyword) || NhaCungCap.Tenncc.Contains(req.Keyword) || NhaCungCap.Mancc.ToString().Contains(req.Keyword)
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
                catch (Exception ex)
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
                int namXBMax = _context.QuyDinhs
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
        public (List<ImportSachTemp>, List<ImportSachTemp>) ProcessExcelFile(Stream excelStream)
        {
            var result = new List<ImportSachTemp>();
            var errors = new List<ImportSachTemp>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            excelStream.Position = 0;

            var dataTable = SetColumnName(new DataTable());
            var (datas, errorMess) = ExcelHelper.ReadExcelTo<ImportSachTemp>(excelStream, dataTable);
            excelStream.Position = 0;

            using (var package = new ExcelPackage(excelStream))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    throw new Exception("File Excel không chứa sheet nào.");
                }

                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension?.Rows ?? 0;

                if (rowCount == 0)
                {
                    throw new Exception("Sheet trong file Excel không chứa dữ liệu.");
                }

                foreach (var item in datas)
                {
                    var trangThai = (item.SoLuong > 0 &&
                                     item.NamXuatBan > 0 &&
                                     DateTime.Now.Year - item.NamXuatBan <= GetNamXBMax() &&
                                     item.GiaSach > 0) ? "OK" : "Lỗi";

                    var moTaLoi = string.Empty;
                    if (string.IsNullOrWhiteSpace(item.TenSach)) moTaLoi += "Tên sách không được để trống. ";
                    if (item.NamXuatBan <= 0 || DateTime.Now.Year - item.NamXuatBan > GetNamXBMax()) moTaLoi += "Năm xuất bản không hợp lệ. ";
                    if (item.SoLuong <= 0) moTaLoi += "Số lượng phải lớn hơn 0.";
                    if (item.GiaSach <= 0) moTaLoi += "Giá sách phải lớn hơn 0.";

                    item.TrangThai = trangThai;
                    item.MoTaLoi = moTaLoi;

                    if (trangThai == "Lỗi")
                    {
                        errors.Add(item);
                    }
                    else
                    {
                        result.Add(item);
                    }
                }
            }

            return (result, errors);
        }


        public void SaveToTempTable(List<ImportSachTemp> data)
        {
            try
            {
                 _context.ImportSachTemp.AddRange(data);
                 _context.SaveChanges();    
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public DataTable SetColumnName(DataTable dt)
        {
            dt.Columns.Add("tensach");
            dt.Columns.Add("theloai");
            dt.Columns.Add("namxuatban");
            dt.Columns.Add("nxb");
            dt.Columns.Add("tacgia");
            dt.Columns.Add("soluong");
            dt.Columns.Add("ngonngu");
            dt.Columns.Add("giasach");
            dt.Columns.Add("mota");
            dt.Columns.Add("urlImage");
            return dt;
        }

        public bool InsertPhieuNhapByExcel(DTO_Tao_Phieu_Nhap_Excel obj)
        {
            if (obj.idThongTin == null || obj.idThongTin.Any() == false)
            {
                return false; // Nếu không có ID nào, không làm gì thêm
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

                    // Tạo mới phiếu nhập sách
                    var newPhieuNhap = new PhieuNhapSach
                    {
                        Ngaynhap = obj.NgayNhap,
                        Manv = obj.MaNhanVien,
                        Mancc = obj.MaNhaCungCap,
                    };

                    _context.PhieuNhapSaches.Add(newPhieuNhap);
                    _context.SaveChanges(); // Save to get the generated MaPn

                    // Lấy danh sách ID từ obj.idThongTin
                    var idList = obj.idThongTin.Select(dto => dto.ID).ToList();

                    // Lấy các bản ghi từ bảng ImportSachTemp theo danh sách ID
                    var listSachTemp = _context.ImportSachTemp
                            .Where(isTemp => idList.Contains(isTemp.Id)) // Sử dụng Contains thay vì Any
                            .Select(isTemp => new
                            {
                                isTemp.Id,
                                isTemp.TenSach,
                                isTemp.TacGia,
                                isTemp.GiaSach, // Đảm bảo lấy giá trị đúng kiểu
                                isTemp.SoLuong,
                                isTemp.NamXuatBan,
                                isTemp.TheLoai,
                                isTemp.NgonNgu,
                                isTemp.NXB,
                                isTemp.MoTa,
                                isTemp.URLImage

                            })
                            .ToList();


                    if (!listSachTemp.Any())
                    {
                        throw new Exception("Không tìm thấy sách trong ImportSachTemp.");
                    }

                    // Khai báo currentYear ngay trước khi sử dụng
                    int currentYear = DateTime.Now.Year;

                    // Lặp qua các sách trong danh sách ImportSachTemp
                    foreach (var sachTemp in listSachTemp)
                    {
                        // Kiểm tra năm xuất bản theo quy định
                        if ((currentYear - sachTemp.NamXuatBan) > namXBMax)
                        {
                            throw new Exception($"Sách '{sachTemp.TenSach}' có năm xuất bản quá cũ so với quy định ({namXBMax} năm trở lại).");
                        }

                        // Kiểm tra trong bảng Sach nếu có sách trùng khớp với tên sách, tác giả, nhà xuất bản, năm xuất bản, ngôn ngữ, thể loại
                        var existingSach = _context.Saches.FirstOrDefault(s => s.Tensach == sachTemp.TenSach && s.Tacgia == sachTemp.TacGia &&
                           s.Nxb == sachTemp.NXB && s.Namxb == sachTemp.NamXuatBan &&
                           s.Ngonngu == sachTemp.NgonNgu && s.Theloai == sachTemp.TheLoai);

                        // Nếu sách đã tồn tại, chỉ cần thêm vào ChiTiết Phiếu Nhập
                        if (existingSach != null)
                        {
                            var newChiTietPN = new Chitietpn
                            {
                                Mapn = newPhieuNhap.Mapn,
                                Masach = existingSach.Masach,
                                Giasach = sachTemp.GiaSach != 0 ? Convert.ToDecimal(sachTemp.GiaSach) : 0m, // Chuyển đổi giá trị sang decimal
                                Soluongnhap = sachTemp.SoLuong,
                            };

                            _context.Chitietpns.Add(newChiTietPN);
                        }
                        else
                        {
                            // Nếu sách chưa tồn tại, thêm sách mới vào bảng Sach
                            var newSach = new Sach
                            {
                                Tensach = sachTemp.TenSach,
                                Theloai = sachTemp.TheLoai,
                                Tacgia = sachTemp.TacGia,
                                Ngonngu = sachTemp.NgonNgu,
                                Nxb = sachTemp.NXB,
                                Namxb = sachTemp.NamXuatBan,
                                Soluonghientai = 0, // Số lượng hiện tại = 0 khi thêm mới
                                Mota = sachTemp.MoTa,
                                UrlImage = sachTemp.URLImage,
                            };

                            _context.Saches.Add(newSach);
                            _context.SaveChanges(); // Save để lấy MaSach

                            // Insert vào ChiTiết Phiếu Nhập
                            var newChiTietPN = new Chitietpn
                            {
                                Mapn = newPhieuNhap.Mapn,
                                Masach = newSach.Masach, // Lấy MaSach sau khi insert
                                Giasach = sachTemp.GiaSach != 0 ? Convert.ToDecimal(sachTemp.GiaSach) : 0m, // Chuyển đổi giá trị sang decimal
                                Soluongnhap = sachTemp.SoLuong,
                            };

                            _context.Chitietpns.Add(newChiTietPN);
                        }
                    }

                    // Lưu tất cả các thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();

                    // Xóa các bản ghi đã sử dụng trong bảng ImportSachTemp
                    var idsToDelete = listSachTemp.Select(s => s.Id).ToList();
                    var recordsToDelete = _context.ImportSachTemp.Where(isTemp => idsToDelete.Contains(isTemp.Id)).ToList();
                    _context.ImportSachTemp.RemoveRange(recordsToDelete);
                    _context.SaveChanges();

                    // Commit transaction
                    transaction.Commit();

                    // Trả về true nếu thao tác thành công
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu gặp lỗi
                    Console.WriteLine($"Error: {ex.Message}");
                    // Xử lý lỗi (ghi log hoặc thông báo)
                    return false; // Trả về false nếu có lỗi
                }
            }
        }




    }
}
