using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class PhieuMuonService
    {
        private readonly QuanLyThuVienContext _context;
        private readonly GeneratePDFService _GeneratePDFService;

        public PhieuMuonService(QuanLyThuVienContext context, GeneratePDFService generatePDFService)
        {
            _context = context;
            _GeneratePDFService = generatePDFService;
        }


        // Lấy phiếu mượn trong số ngày gần đây dựa vào songayMax trong bảng quydinh
        public List<PhieuMuon> GetPhieuMuonInLastDay(int maThe)
        {
            // Truy vấn songayMax từ bảng quydinh
            var songayMax = _context.QuyDinhs.FirstOrDefault()?.SongayMax ?? 0;
            var currentDate = DateTime.Now.Date; // Ngày hiện tại
            var startDate = currentDate.AddDays(-songayMax); // Ngày bắt đầu tính

            return _context.Set<PhieuMuon>()
                .Where(pm => pm.Mathe == maThe
                             && pm.Ngaymuon.HasValue
                             && pm.Ngaymuon >= DateOnly.FromDateTime(startDate)
                             && pm.Ngaymuon <= DateOnly.FromDateTime(currentDate))
                .ToList();
        }


        //Kiểm tra số lượng mượn đã được 5 cuốn chưa cho một mã thẻ

        // Kiểm tra số lượng mượn đã đạt 5 cuốn chưa
        public bool HasBorrowedFiveBooks(List<PhieuMuon> phieuMuons)
        {
            var maPhieuMuons = phieuMuons.Select(pm => pm.Mapm).ToList();

            var ctPM = _context.ChiTietPms
                .Where(ct => maPhieuMuons.Contains(ct.Mapm))
                .ToList();

            var borrowedBooksCount = ctPM.Sum(ct => ct.Soluongmuon ?? 0); // Tính tổng số lượng sách mượn

            return borrowedBooksCount >= 5;
        }


        // Kiểm tra tình trạng phiếu mượn và hạn trả
        public string CheckReturnStatusAndLimit(int maThe)
        {
            // Lấy tất cả phiếu mượn của mã thẻ
            var borrowedBooks = _context.Set<PhieuMuon>()
                .Where(pm => pm.Mathe == maThe && pm.Tinhtrang == false)
                .ToList();

            foreach (var pm in borrowedBooks)
            {
                // Kiểm tra hạn trả
                if (pm.Hantra.HasValue && pm.Hantra.Value < DateOnly.FromDateTime(DateTime.Now))
                {
                    return $"Phiếu mượn {pm.Mapm} đã hết hạn trả";
                }
            }

            return "";
        }
        // Kiểm tra và xác thực phiếu mượn
        public string ValidatePhieuMuon(int maThe)
        {
            // Lấy danh sách phiếu mượn dựa vào số ngày quy định
            var phieuMuon = GetPhieuMuonInLastDay(maThe);

            // Kiểm tra số lượng sách đã mượn
            if (HasBorrowedFiveBooks(phieuMuon))
            {
                return "Đã mượn quá 5 cuốn sách.";
            }
            else
            {
                // Kiểm tra tình trạng và hạn trả
                var returnStatus = CheckReturnStatusAndLimit(maThe);
                if (!string.IsNullOrEmpty(returnStatus))
                {
                    return returnStatus;
                }
            }

            return "";
        }


        public BookDetailsDTO GetByMaCuonSach(string maCuonSach)
        {
            try
            {
                var bookDetailsDTO = (
                    from cuonSach in _context.CuonSaches
                    join sach in _context.Saches
                    on cuonSach.Masach equals sach.Masach
                    where cuonSach.Macuonsach == maCuonSach // Thêm điều kiện lọc
                    && cuonSach.Tinhtrang == 0
                    select new BookDetailsDTO
                    {
                        MaCuonSach = cuonSach.Macuonsach,
                        TenSach = sach.Tensach,
                        MaSach = cuonSach.Masach
                    }).FirstOrDefault();

                return bookDetailsDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByMaCuonSach: {ex.Message}");
                throw;
            }
        }


        public bool InsertPhieuMuon(DTO_Tao_Phieu_Muon pm)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newPhieuMuon = new PhieuMuon
                    {
                        Ngaymuon = pm.NgayMuon,
                        Hantra = pm.NgayTra,
                        Mathe = pm.MaTheDocGia,
                        Manv = pm.MaNhanVien,
                        Madk = 0,
                        Tinhtrang = false
                    };

                    _context.PhieuMuons.Add(newPhieuMuon);
                    _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    foreach (var sachMuon in pm.listSachMuon)
                    {
                        var newChiTietPM = new ChiTietPm
                        {
                            Mapm = newPhieuMuon.Mapm,
                            Masach = sachMuon.MaSach,

                            Soluongmuon = sachMuon.SoLuong
                        };

                        _context.ChiTietPms.Add(newChiTietPM);
                    }

                    _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    transaction.Commit();

                    foreach (var sachMuon in pm.listCTSachMuon)
                    {
                        var newChiTietSachPM = new ChiTietSachMuon
                        {
                            Mapm = newPhieuMuon.Mapm,
                            Macuonsach = sachMuon.MaCuonSach,

                        };

                        _context.ChiTietSachMuons.Add(newChiTietSachPM);
                    }

                    _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex);
                    return false;
                }
            }


        }

 
        public string CheckSoLuongSach(DTO_Tao_Phieu_Muon x)
        {
            // Lấy songayMax từ bảng QuyDinh
            var songayMax = _context.QuyDinhs.FirstOrDefault()?.SongayMax ?? 0;
            var currentDate = DateTime.Now.Date;
            var startDate = currentDate.AddDays(-songayMax);

            // Tính tổng số sách đã mượn trong songayMax
            var totalBorrowedBooks = _context.ChiTietPms
            .Where(ct => _context.PhieuMuons
            .Any(pm => pm.Mapm == ct.Mapm
           && pm.Mathe == x.MaTheDocGia
           && pm.Ngaymuon.HasValue
           && pm.Ngaymuon >= DateOnly.FromDateTime(startDate)
           && pm.Ngaymuon <= DateOnly.FromDateTime(currentDate)))
            .Sum(ct => ct.Soluongmuon ?? 0);


            // Tính tổng số sách muốn mượn hiện tại
            var totalCurrentBooks = x.listSachMuon.Sum(s => s.SoLuong);

            // Kiểm tra nếu tổng vượt quá giới hạn
            if (totalBorrowedBooks + totalCurrentBooks > 5)
            {
                return "Số lượng sách mượn vượt quá giới hạn cho phép (5 cuốn).";
            }

            // Kiểm tra mỗi mã sách chỉ mượn tối đa 2 cuốn
            foreach (var sachMuon in x.listSachMuon)
            {
               var sl =  x.listSachMuon.Count(a => a.MaSach == sachMuon.MaSach) ;
                if (sl > 2)
                {
                    return $"Mã sách {sachMuon.MaSach} vượt quá số lượng mượn tối đa (2 cuốn).";
                }
            }

            // Kiểm tra MaNhanVien có tồn tại không
            var nhanVien = _context.NhanViens.Find(x.MaNhanVien);
            if (nhanVien == null)
            {
                return $"NhanVien với MaNV = {x.MaNhanVien} không tồn tại.";
            }
            else
            {
                return "";
            }
        }
        public byte[] InsertAndGeneratePDF(DTO_Tao_Phieu_Muon x)
        {
            // Kiểm tra điều kiện để thêm phiếu trả
            if (x.listSachMuon == null || x.listCTSachMuon == null || x.listSachMuon.Count == 0 || x.listCTSachMuon.Count == 0)
            {
                return null;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tạo đối tượng PhieuMuon
                    var newPhieuMuon = new PhieuMuon
                    {
                        Madk = x.MaDK,
                        Mathe = x.MaTheDocGia,
                        Manv = x.MaNhanVien,
                        Ngaymuon = x.NgayMuon,
                        Hantra = x.NgayTra
                    };

                    // Thêm PhieuMuon vào context
                    _context.PhieuMuons.Add(newPhieuMuon);
                    _context.SaveChanges();

                    // Nhóm các sách theo mã sách và tính tổng số lượng của mỗi mã sách
                    var groupedBooks = x.listSachMuon
                        .GroupBy(s => s.MaSach)
                        .Select(g => new { MaSach = g.Key, TotalQuantity = g.Sum(s => s.SoLuong) })
                        .Where(g => g.TotalQuantity > 0) // Lọc ra những sách có số lượng lớn hơn 0
                        .ToList();

                    foreach (var groupedBook in groupedBooks)
                    {
                        // Kiểm tra xem mã sách đã có trong ChiTietPm chưa
                        var existingChiTietPM = _context.ChiTietPms
                            .FirstOrDefault(ct => ct.Mapm == newPhieuMuon.Mapm && ct.Masach == groupedBook.MaSach);

                        if (existingChiTietPM != null)
                        {
                            // Nếu đã có, cộng dồn số lượng sách
                            existingChiTietPM.Soluongmuon += groupedBook.TotalQuantity;

                            // Cập nhật lại đối tượng đã thay đổi
                            _context.ChiTietPms.Update(existingChiTietPM);
                        }
                        else
                        {
                            // Nếu chưa có, thêm mới ChiTietPm
                            var newChiTietPM = new ChiTietPm
                            {
                                Mapm = newPhieuMuon.Mapm,
                                Masach = groupedBook.MaSach,
                                Soluongmuon = groupedBook.TotalQuantity,
                            };
                            _context.ChiTietPms.Add(newChiTietPM);
                        }
                    }

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();




                    foreach (var ctSachMuon in x.listCTSachMuon)
                    {
                        var newChiTietCMS = new ChiTietSachMuon
                        {
                            Mapm = newPhieuMuon.Mapm,
                            Macuonsach = ctSachMuon.MaCuonSach,
                            Tinhtrang = ctSachMuon.TinhTrang,
                        };

                        _context.ChiTietSachMuons.Add(newChiTietCMS);
                    }

                    // Lưu toàn bộ thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();

                    // Commit giao dịch
                    transaction.Commit();

                    // Kết hợp DTO hiện tại với MaPM để tạo PDF
                    int maPM = newPhieuMuon.Mapm;
                    var pdfData = _GeneratePDFService.GeneratePhieuMuonPDF(x, maPM);

                    // Trả về dữ liệu PDF dưới dạng byte[]
                    return pdfData;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu gặp lỗi
                    Console.WriteLine($"Error: {ex.Message}");
                    // Xử lý lỗi (ghi log hoặc thông báo)
                    throw new Exception(ex.Message);
                }
            }
        }




        //public byte[] InsertAndGeneratePDF(DTO_Tao_Phieu_Muon x)
        //{
        //    // Kiểm tra điều kiện để thêm phiếu trả
        //    if (x.listSachMuon == null || x.listCTSachMuon == null || x.listSachMuon.Count == 0 || x.listCTSachMuon.Count == 0)
        //    {
        //        return null;
        //    }

        //    using (var transaction = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {


        //            // Tạo đối tượng PhieuMuon
        //            var newPhieuMuon = new PhieuMuon
        //            {
        //                Madk = x.MaDK,
        //                Mathe = x.MaTheDocGia,
        //                Manv = x.MaNhanVien,
        //                Ngaymuon = x.NgayMuon,
        //                Hantra = x.NgayTra
        //            };

        //            // Thêm PhieuMuon vào context
        //            _context.PhieuMuons.Add(newPhieuMuon);
        //            _context.SaveChanges();

        //            // Xử lý ListSachMuon
        //            foreach (var sachMuon in x.listSachMuon)
        //            {
        //                if (sachMuon.SoLuong == 0)
        //                {
        //                    continue;
        //                }

        //                var newChiTietPM = new ChiTietPm
        //                {
        //                    Mapm = newPhieuMuon.Mapm,
        //                    Masach = sachMuon.MaSach,
        //                    Soluongmuon = sachMuon.SoLuong,
        //                };

        //                _context.ChiTietPms.Add(newChiTietPM);
        //            }

        //            foreach (var ctSachMuon in x.listCTSachMuon)
        //            {
        //                var newChiTietCMS = new ChiTietSachMuon
        //                {
        //                    Mapm = newPhieuMuon.Mapm,
        //                    Macuonsach = ctSachMuon.MaCuonSach,
        //                    Tinhtrang = ctSachMuon.TinhTrang,
        //                };

        //                _context.ChiTietSachMuons.Add(newChiTietCMS);
        //            }

        //            // Lưu toàn bộ thay đổi vào cơ sở dữ liệu
        //            _context.SaveChanges();

        //            // Commit giao dịch
        //            transaction.Commit();

        //            // Kết hợp DTO hiện tại với MaPM để tạo PDF
        //            int maPM = newPhieuMuon.Mapm;
        //            var pdfData = _GeneratePDFService.GeneratePhieuMuonPDF(x, maPM);

        //            // Trả về dữ liệu PDF dưới dạng byte[]
        //            return pdfData;
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback(); // Rollback nếu gặp lỗi
        //            Console.WriteLine($"Error: {ex.Message}");
        //            // Xử lý lỗi (ghi log hoặc thông báo)
        //            throw new Exception(ex.Message);
        //        }
        //    }
        //}

    }


}

 

