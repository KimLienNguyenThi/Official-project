using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class PhieuTraService
    {
        private readonly QuanLyThuVienContext _context;
        private readonly GeneratePDFService _GeneratePDFService;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public PhieuTraService(QuanLyThuVienContext context, GeneratePDFService generatePDFService)
        {
            _context = context;
            _GeneratePDFService = generatePDFService;
           // _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagingResult<PhieuMuonDTO>> GetAllPhieuMuonPaging(GetListPhieuMuonPaging req)
        {
            var query =
                (from PhieuMuon in _context.PhieuMuons
                 join DocGia in _context.DocGia
                    on PhieuMuon.Mathe equals DocGia.Madg
                 join CHITIETPM in _context.ChiTietPms
                    on PhieuMuon.Mapm equals CHITIETPM.Mapm
                 where PhieuMuon.Tinhtrang == false
                       && (string.IsNullOrEmpty(req.Keyword) || DocGia.Hotendg.Contains(req.Keyword) || DocGia.Sdt.Contains(req.Keyword))
                 select new PhieuMuonDTO
                 {
                     MaPM = PhieuMuon.Mapm,
                     MaThe = DocGia.Madg,
                     HoTenDG = DocGia.Hotendg,
                     SDT = DocGia.Sdt,
                     NgayMuon = PhieuMuon.Ngaymuon,
                     HanTra = PhieuMuon.Hantra
                 }
                ).Distinct();

            var totalRow = await query.CountAsync();

            var listPhieumuons = await query.OrderByDescending(x => x.MaPM).Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToListAsync();

            return new PagingResult<PhieuMuonDTO>
            {
                Results = listPhieumuons,
                CurrentPage = req.Page,
                RowCount = totalRow,
                PageSize = req.PageSize
            };
        }

        public IEnumerable<SachMuonDTO> getSachMuon(int MaPm)
        {// Lấy danh sách sách đã trả
            var listSachTra = (
                from phieuTra in _context.PhieuTras
                join chiTietPT in _context.ChiTietPts on phieuTra.Mapt equals chiTietPT.Mapt
                join chiTietSachTra in _context.ChiTietSachTras on chiTietPT.Mapt equals chiTietSachTra.Mapt
                join cuonSach in _context.CuonSaches on chiTietSachTra.Macuonsach equals cuonSach.Macuonsach
                where phieuTra.Mapm == MaPm && cuonSach.Masach == chiTietPT.Masach
                group new { chiTietPT.Masach, chiTietSachTra.Macuonsach } by new { chiTietPT.Masach, chiTietSachTra.Macuonsach } into g
                select new
                {
                    MaSach = g.Key.Masach,
                    MacuonSach = g.Key.Macuonsach
                }
            ).ToList();

            // Lấy danh sách sách mượn
            var sachMuonList = (
                from chiTietPM in _context.ChiTietPms
                join chiTietSachMuon in _context.ChiTietSachMuons on chiTietPM.Mapm equals chiTietSachMuon.Mapm
                join sach in _context.Saches on chiTietPM.Masach equals sach.Masach
                join chitietPN in _context.Chitietpns on chiTietPM.Masach equals chitietPN.Masach
                join cuonSach in _context.CuonSaches on chiTietSachMuon.Macuonsach equals cuonSach.Macuonsach
                where chiTietPM.Mapm == MaPm && cuonSach.Masach == chiTietPM.Masach
                group new { sach, chitietPN, cuonSach, chiTietSachMuon } by new { sach.Masach, sach.Tensach, chiTietSachMuon.Macuonsach } into g
                select new
                {
                    MaSach = g.Key.Masach,
                    TenSach = g.Key.Tensach,
                    MacuonSach = g.Key.Macuonsach,
                    Giasach = g.Max(x => x.chitietPN.Giasach)
                }
            ).AsEnumerable()
            .Select(x =>
            {
                // Kiểm tra nếu cuốn sách này đã trả
                bool daTra = listSachTra.Any(s => s.MaSach == x.MaSach && s.MacuonSach == x.MacuonSach);

                // Loại bỏ sách đã trả
                if (!daTra)
                {
                    return new SachMuonDTO
                    {
                        MaSach = x.MaSach,
                        TenSach = x.TenSach,
                        MACUONSACH = x.MacuonSach,
                        giasach = x.Giasach ?? 0 // Giá mặc định nếu null
                    };
                }
                return null;
            })
            .Where(x => x != null)
            .ToList();

            // Trả về danh sách sách mượn còn lại
            return sachMuonList;


        }

        public byte[] InsertAndGeneratePDF(DTO_Tao_Phieu_Tra x)
        {
            // Kiểm tra điều kiện để thêm phiếu trả
            if (x.ListSachTra == null || x.ListSachTra.Count == 0 ||
                !x.ListSachTra.Any(sach => sach.SoLuongLoi > 0 || sach.SoLuongTra > 0 || sach.SoLuongMat > 0))
            {
                return null;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Kiểm tra MaNhanVien có tồn tại không
                    var nhanVien = _context.NhanViens.Find(x.MaNhanVien);
                    if (nhanVien == null)
                    {
                        throw new Exception($"NhanVien with MaNV = {x.MaNhanVien} does not exist.");
                    }

                    // Kiểm tra phiếu mượn có tồn tại không
                    var phieuMuon = _context.PhieuMuons.FirstOrDefault(p => p.Mapm == x.MaPhieuMuon);
                    if (phieuMuon == null)
                    {
                        return null;
                    }

                    // Tạo đối tượng PhieuTra
                    var newPhieuTra = new PhieuTra
                    {
                        Ngaytra = x.NgayTra,
                        Mathe = phieuMuon.Mathe,
                        Manv = x.MaNhanVien,
                        Mapm = x.MaPhieuMuon,
                    };

                    // Thêm PhieuTra vào context
                    _context.PhieuTras.Add(newPhieuTra);
                    _context.SaveChanges(); // Lưu để có thể lấy MaPT từ newPhieuTra

                    // Xử lý ListSachTra
                    foreach (var sachTra in x.ListSachTra)
                    {
                        if (sachTra.SoLuongTra == 0 && sachTra.SoLuongLoi == 0 && sachTra.SoLuongMat == 0)
                        {
                            continue;
                        }

                        var newChiTietPT = new ChiTietPt
                        {
                            Mapt = newPhieuTra.Mapt,
                            Masach = sachTra.MaSach,
                            Soluongtra = sachTra.SoLuongTra,
                            Soluongloi = sachTra.SoLuongLoi,
                            Soluongmat = sachTra.SoLuongMat,
                            Phuthu = sachTra.PhuThu,
                        };

                        _context.ChiTietPts.Add(newChiTietPT);

                        // Xử lý ListCTSachTra của mỗi sách (nếu có)
                        if (sachTra.ListCTSachTra != null)
                        {
                            foreach (var ctSachTra in sachTra.ListCTSachTra)
                            {
                                // Kiểm tra xem MaCuonSach có tồn tại không
                                var cuonSach = _context.CuonSaches.FirstOrDefault(cs => cs.Macuonsach == ctSachTra.MaCuonSach);
                                if (cuonSach == null)
                                {
                                    throw new Exception($"CuonSach with Macuonsach = {ctSachTra.MaCuonSach} does not exist.");
                                }

                                var newChiTietCTS = new ChiTietSachTra
                                {
                                    Mapt = newPhieuTra.Mapt,
                                    Macuonsach = ctSachTra.MaCuonSach,
                                    Tinhtrang = ctSachTra.Tinhtrang,
                                };

                                _context.ChiTietSachTras.Add(newChiTietCTS);
                            }
                        }
                    }

                    // Lưu toàn bộ thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();

                    // Commit giao dịch
                    transaction.Commit();

                    // Kết hợp DTO hiện tại với MaPT để tạo PDF
                    int MaPhieuTra = newPhieuTra.Mapt;
                    var pdfData = _GeneratePDFService.GeneratePhieuTraPDF(x, MaPhieuTra);

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




        //public int? Insert(DTO_Tao_Phieu_Tra x)
        //{
        //    // Kiểm tra điều kiện để thêm phiếu trả
        //    if (x.ListSachTra == null || x.ListSachTra.Count == 0 ||
        //        !x.ListSachTra.Any(sach => sach.SoLuongLoi > 0 || sach.SoLuongTra > 0 || sach.SoLuongMat > 0))
        //    {
        //        return null;
        //    }

        //    using (var transaction = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            // Kiểm tra MaNhanVien có tồn tại không
        //            var nhanVien = _context.NhanViens.Find(x.MaNhanVien);
        //            if (nhanVien == null)
        //            {
        //                throw new Exception($"NhanVien with MaNV = {x.MaNhanVien} does not exist.");
        //            }

        //            // Kiểm tra phiếu mượn có tồn tại không
        //            var phieuMuon = _context.PhieuMuons.FirstOrDefault(p => p.Mapm == x.MaPhieuMuon);
        //            if (phieuMuon == null)
        //            {
        //                return null;
        //            }

        //            // Tạo đối tượng PhieuTra
        //            var newPhieuTra = new PhieuTra
        //            {
        //                Ngaytra = x.NgayTra,
        //                Mathe = phieuMuon.Mathe,
        //                Manv = x.MaNhanVien,
        //                Mapm = x.MaPhieuMuon,
        //            };

        //            // Thêm PhieuTra vào context
        //            _context.PhieuTras.Add(newPhieuTra);
        //            _context.SaveChanges(); // Lưu để có thể lấy MaPT từ newPhieuTra

        //            // Xử lý ListSachTra
        //            foreach (var sachTra in x.ListSachTra)
        //            {
        //                if (sachTra.SoLuongTra == 0 && sachTra.SoLuongLoi == 0 && sachTra.SoLuongMat == 0)
        //                {
        //                    continue;
        //                }

        //                var newChiTietPT = new ChiTietPt
        //                {
        //                    Mapt = newPhieuTra.Mapt,
        //                    Masach = sachTra.MaSach,
        //                    Soluongtra = sachTra.SoLuongTra,
        //                    Soluongloi = sachTra.SoLuongLoi,
        //                    Soluongmat = sachTra.SoLuongMat,
        //                    Phuthu = sachTra.PhuThu,
        //                };

        //                _context.ChiTietPts.Add(newChiTietPT);

        //                // Xử lý ListCTSachTra của mỗi sách (nếu có)
        //                if (sachTra.ListCTSachTra != null)
        //                {
        //                    foreach (var ctSachTra in sachTra.ListCTSachTra)
        //                    {
        //                        // Kiểm tra xem MaCuonSach có tồn tại không
        //                        var cuonSach = _context.CuonSaches.FirstOrDefault(cs => cs.Macuonsach == ctSachTra.MaCuonSach);
        //                        if (cuonSach == null)
        //                        {
        //                            throw new Exception($"CuonSach with Macuonsach = {ctSachTra.MaCuonSach} does not exist.");
        //                        }

        //                        var newChiTietCTS = new ChiTietSachTra
        //                        {
        //                            Mapt = newPhieuTra.Mapt,
        //                            Macuonsach = ctSachTra.MaCuonSach, // Khóa ngoại được gán giá trị hợp lệ
        //                            Tinhtrang = ctSachTra.Tinhtrang,
        //                        };

        //                        _context.ChiTietSachTras.Add(newChiTietCTS);
        //                    }
        //                }
        //            }

        //            // Lưu toàn bộ thay đổi vào cơ sở dữ liệu
        //            _context.SaveChanges();

        //            // Commit giao dịch
        //            transaction.Commit();

        //            return newPhieuTra.Mapt; // Trả về Mapt vừa được thêm

        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback(); // Rollback nếu gặp lỗi
        //            Console.WriteLine($"Error: {ex.Message}");
        //            // Xử lý lỗi (ghi log hoặc thông báo)
        //            return null; // Trả về null nếu lỗi
        //        }
        //    }
        //}



    }
}
