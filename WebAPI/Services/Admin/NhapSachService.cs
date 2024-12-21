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

        public List<ImportSachTemp> ProcessExcelFile(Stream excelStream)
        {
            var result = new List<ImportSachTemp>();

            // Thiết lập LicenseContext
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Reset vị trí của stream trước khi sử dụng lại
            excelStream.Position = 0;
            var dataTable = SetColumnName(new DataTable());

            var (datas, errorMess) = ExcelHelper.ReadExcelTo<ImportSachTemp>(excelStream, dataTable);
            // Đặt lại con trỏ stream trước khi tiếp tục sử dụng với ExcelPackage
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

                //for (int row = 6; row <= rowCount; row++) // Bỏ qua tiêu đề (row 1)
                //{
                //    try
                //    {
                //        var tensach = worksheet.Cells[row, 1].Text.Trim();
                //        var theloai = worksheet.Cells[row, 2].Text.Trim();
                //        var namxb = int.TryParse(worksheet.Cells[row, 3].Text, out var nam) ? nam : -1;
                //        var nxb = worksheet.Cells[row, 4].Text.Trim();
                //        var tacgia = worksheet.Cells[row, 5].Text.Trim();
                //        var soluong = int.TryParse(worksheet.Cells[row, 6].Text, out var sl) ? sl : -1;
                //        var ngonngu = worksheet.Cells[row, 7].Text.Trim();
                //        var giasach = int.TryParse(worksheet.Cells[row, 8].Text.Trim(), out var gia) ? gia : -1; // Cập nhật GiaSach
                //        var mota = worksheet.Cells[row, 9].Text.Trim();
                //        var urlImage = worksheet.Cells[row, 10].Text.Trim();

                //        // Xử lý logic kiểm tra dữ liệu
                //        var trangThai = (soluong > 0 && namxb > 0 &&  DateTime.Now.Year- namxb <=GetNamXBMax() && giasach > 0) ? "OK" : "Lỗi";
                //        var moTaLoi = "";

                //        if (string.IsNullOrWhiteSpace(tensach)) moTaLoi += "Tên sách không được để trống. ";
                //        if (namxb <= 0 || DateTime.Now.Year - namxb > GetNamXBMax()) moTaLoi += "Năm xuất bản không hợp lệ. ";
                //        if (soluong <= 0) moTaLoi += "Số lượng phải lớn hơn 0.";
                //        if (giasach <= 0) moTaLoi += "Giá sách phải lớn hơn 0.";

                //        result.Add(new ImportSachTemp
                //        {
                //            TenSach = tensach,
                //            TheLoai = theloai,
                //            TacGia = tacgia,
                //            NgonNgu = ngonngu,
                //            NXB = nxb,
                //            NamXuatBan = namxb,
                //            URLImage = urlImage,
                //            MoTa = mota,
                //            SoLuong = soluong,
                //            GiaSach = giasach,  // Cập nhật GiaSach
                //            TrangThai = trangThai,
                //            MoTaLoi = moTaLoi
                //        });
                //    }
                //    catch (Exception ex)
                //    {
                //        result.Add(new ImportSachTemp
                //        {
                //            TenSach = "",
                //            TrangThai = "Lỗi",
                //            MoTaLoi = $"Lỗi đọc dòng {row}: {ex.Message}"
                //        });
                //    }
                //}
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

                    result.Add(item);
                }

            }

            return result;
        }

        public void SaveToTempTable(List<ImportSachTemp> data)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    using (var connection = _context.Database.GetDbConnection())
                    {
                        connection.Open();

                        foreach (var item in data)
                        {
                            var command = new SqlCommand(@"
                        INSERT INTO ImportSachTemp 
                        (TENSACH, THELOAI, TACGIA, NGONNGU, NXB, NAMXB, URL_IMAGE, MOTA, SOLUONG, TrangThai, MoTaLoi)
                        VALUES 
                        (@TenSach, @TheLoai, @TacGia, @NgonNgu, @NXB, @NamXuatBan, @URLImage, @MoTa, @SoLuong, @TrangThai, @MoTaLoi)",
                                (SqlConnection)connection);

                            command.Parameters.AddWithValue("@TenSach", (object)item.TenSach ?? DBNull.Value);
                            command.Parameters.AddWithValue("@TheLoai", (object)item.TheLoai ?? DBNull.Value);
                            command.Parameters.AddWithValue("@TacGia", (object)item.TacGia ?? DBNull.Value);
                            command.Parameters.AddWithValue("@NgonNgu", (object)item.NgonNgu ?? DBNull.Value);
                            command.Parameters.AddWithValue("@NXB", (object)item.NXB ?? DBNull.Value);
                            command.Parameters.AddWithValue("@NamXuatBan", item.NamXuatBan > 0 ? item.NamXuatBan : (object)DBNull.Value);
                            command.Parameters.AddWithValue("@URLImage", (object)item.URLImage ?? DBNull.Value);
                            command.Parameters.AddWithValue("@MoTa", (object)item.MoTa ?? DBNull.Value);
                            command.Parameters.AddWithValue("@SoLuong", item.SoLuong > 0 ? item.SoLuong : (object)DBNull.Value);
                            command.Parameters.AddWithValue("@TrangThai", item.TrangThai);
                            command.Parameters.AddWithValue("@MoTaLoi", (object)item.MoTaLoi ?? DBNull.Value);

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; // Quăng lại lỗi để xử lý phía trên
                }
            }
        }

        public DataTable SetColumnName(DataTable dt)
        {
            dt.Columns.Add("tensach");
            dt.Columns.Add("theloai");
            dt.Columns.Add("namxb");
            dt.Columns.Add("nxb");
            dt.Columns.Add("tacgia");
            dt.Columns.Add("soluong");
            dt.Columns.Add("ngonngu");
            dt.Columns.Add("giasach");
            dt.Columns.Add("mota");
            dt.Columns.Add("urlImage");
            return dt;
        }
    }
}
