using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class QuanLyPhieuTraService
    {
        private readonly QuanLyThuVienContext _context;

        public QuanLyPhieuTraService(QuanLyThuVienContext context)
        {
            _context = context;
        }

        public async Task<PagingResult<PhieuTra_GroupMaPM_DTO>> GetAllPhieuTraPaging(GetListPhieuTraPaging req)
        {
            var query =
                (from PhieuTra in _context.PhieuTras
                 join PhieuMuon in _context.PhieuMuons
                 on PhieuTra.Mapm equals PhieuMuon.Mapm
                 join DocGia in _context.DocGia
                    on PhieuTra.Mathe equals DocGia.Madg
                 join NhanVien in _context.NhanViens
                 on PhieuTra.Manv equals NhanVien.Manv
                 where string.IsNullOrEmpty(req.Keyword) || DocGia.Hotendg.Contains(req.Keyword) || DocGia.Sdt.Contains(req.Keyword)
                 select new PhieuTra_DTO1
                 {
                     MaPT = PhieuTra.Mapt,
                     NgayMuon = PhieuMuon.Ngaymuon,
                     NgayTra = PhieuTra.Ngaytra,
                     MaPM = PhieuTra.Mapm.Value,
                     MaThe = DocGia.Madg,
                     HoTenDG = DocGia.Hotendg,
                     SDT = DocGia.Sdt,
                     MaNV = NhanVien.Manv
                 }).AsQueryable()
            .GroupBy(g => new { g.MaPM, g.HoTenDG, g.MaThe, g.SDT }, (key, g) => new PhieuTra_GroupMaPM_DTO
            {
                PhieuTra_GroupKey = new PhieuTra_GroupKey()
                {
                    HoTenDG = key.HoTenDG,
                    SDT = key.SDT,
                    MaThe = key.MaThe,
                    MaPM = key.MaPM,
                },
                DataPhieuTras = g.Select(phieutra => new PhieuTra_DTO1
                {
                    MaPT = phieutra.MaPT,
                    NgayMuon = phieutra.NgayMuon,
                    NgayTra = phieutra.NgayTra,
                    MaPM = phieutra.MaPM,
                    MaThe = phieutra.MaThe,
                    HoTenDG = phieutra.HoTenDG,
                    SDT = phieutra.SDT,
                    MaNV = phieutra.MaNV
                }).ToList(),
                //CountRow = g.Count()
            });
            var totalRow = await query.CountAsync();

            var listPhieutras = await query.OrderByDescending(x => x.PhieuTra_GroupKey.MaPM).Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToListAsync();

            return new PagingResult<PhieuTra_GroupMaPM_DTO>()
            {
                Results = listPhieutras,
                CurrentPage = req.Page,
                RowCount = totalRow,
                PageSize = req.PageSize
            };
        }

        public List<DTO_Sach_Tra> Get_ChiTietPT_ByMaPM(int maPM)
        {
            // Lấy thông tin cơ bản từ phiếu trả và chi tiết phiếu trả
            var listPhieutra_All =
                (from pt in _context.PhieuTras
                 join ctpt in _context.ChiTietPts on pt.Mapt equals ctpt.Mapt
                 join s in _context.Saches on ctpt.Masach equals s.Masach
                 join pm in _context.PhieuMuons on pt.Mapm equals pm.Mapm
                 join ctpm in _context.ChiTietPms on pm.Mapm equals ctpm.Mapm
                 where pt.Mapm == maPM
                 group new { pt, ctpt, s, ctpm } by new
                 {
                     pt.Mapt,
                     s.Masach,
                     s.Tensach,
                     ctpm.Soluongmuon,
                     ctpt.Soluongtra,
                     ctpt.Soluongloi,
                     ctpt.Soluongmat,
                     ctpt.Phuthu
                 } into groupedData
                 select new DTO_Sach_Tra
                 {
                     MaPT = groupedData.Key.Mapt,
                     MaSach = groupedData.Key.Masach,
                     TenSach = groupedData.Key.Tensach,
                     SoLuongMuon = groupedData.Key.Soluongmuon ?? 0,
                     SoLuongTra = groupedData.Key.Soluongtra ?? 0,
                     SoLuongLoi = groupedData.Key.Soluongloi ?? 0,
                     SoLuongMat = groupedData.Key.Soluongmat ?? 0,
                     PhuThu = groupedData.Key.Phuthu ?? 0
                 }).ToList();

            // Lấy chi tiết cuốn sách cho từng phiếu trả và gán vào ListCTSachTra
            foreach (var dto in listPhieutra_All)
            {
                // Lấy danh sách chi tiết sách trả cho từng phiếu trả
                dto.ListCTSachTra = _context.ChiTietSachTras
                    .Where(ctst => ctst.Mapt == dto.MaPT)
                    .Join(_context.CuonSaches,
                          ctst => ctst.Macuonsach,
                          cs => cs.Macuonsach,
                          (ctst, cs) => new { ctst, cs })
                    .Where(joined => joined.cs.Masach == dto.MaSach)
                    .Select(joined => new DTO_CT_Sach_Tra
                    {
                        MaPT = joined.ctst.Mapt,
                        MaCuonSach = joined.ctst.Macuonsach,
                        Tinhtrang = joined.ctst.Tinhtrang ?? 0
                    })
                    .Distinct() // Loại bỏ bản ghi trùng lặp
                    .ToList();
            }

            // Loại bỏ các đối tượng trùng lặp trong listPhieutra_All
            listPhieutra_All = listPhieutra_All
                .GroupBy(x => new { x.MaPT, x.MaSach }) // Nhóm theo MaPT và MaSach
                .Select(g => g.First()) // Lấy phần tử đầu tiên trong mỗi nhóm
                .ToList();

            return listPhieutra_All;
        }
        public List<DTO_Sach_Tra> Get_CTPT_ByMaPT(int maPT)
        {
            // Lấy thông tin cơ bản từ phiếu trả và chi tiết phiếu trả
            var listPhieutra_All =
                (from pt in _context.PhieuTras
                 join ctpt in _context.ChiTietPts on pt.Mapt equals ctpt.Mapt
                 join s in _context.Saches on ctpt.Masach equals s.Masach
                 join pm in _context.PhieuMuons on pt.Mapm equals pm.Mapm
                 join ctpm in _context.ChiTietPms on pm.Mapm equals ctpm.Mapm
                 where pt.Mapt == maPT
                 group new { pt, ctpt, s, ctpm } by new
                 {
                     pt.Mapt,
                     s.Masach,
                     s.Tensach,
                     ctpm.Soluongmuon,
                     ctpt.Soluongtra,
                     ctpt.Soluongloi,
                     ctpt.Soluongmat,
                     ctpt.Phuthu
                 } into groupedData
                 select new DTO_Sach_Tra
                 {
                     MaPT = groupedData.Key.Mapt,
                     MaSach = groupedData.Key.Masach,
                     TenSach = groupedData.Key.Tensach,
                     SoLuongMuon = groupedData.Key.Soluongmuon ?? 0,
                     SoLuongTra = groupedData.Key.Soluongtra ?? 0,
                     SoLuongLoi = groupedData.Key.Soluongloi ?? 0,
                     SoLuongMat = groupedData.Key.Soluongmat ?? 0,
                     PhuThu = groupedData.Key.Phuthu ?? 0
                 }).ToList();

            // Lấy chi tiết cuốn sách cho từng phiếu trả và gán vào ListCTSachTra
            foreach (var dto in listPhieutra_All)
            {
                // Lấy danh sách chi tiết sách trả cho từng phiếu trả
                dto.ListCTSachTra = _context.ChiTietSachTras
                    .Where(ctst => ctst.Mapt == dto.MaPT)
                    .Join(_context.CuonSaches,
                          ctst => ctst.Macuonsach,
                          cs => cs.Macuonsach,
                          (ctst, cs) => new { ctst, cs })
                    .Where(joined => joined.cs.Masach == dto.MaSach)
                    .Select(joined => new DTO_CT_Sach_Tra
                    {
                        MaPT = joined.ctst.Mapt,
                        MaCuonSach = joined.ctst.Macuonsach,
                        Tinhtrang = joined.ctst.Tinhtrang ?? 0
                    })
                    .Distinct() // Loại bỏ bản ghi trùng lặp
                    .ToList();
            }

            // Loại bỏ các đối tượng trùng lặp trong listPhieutra_All
            listPhieutra_All = listPhieutra_All
                .GroupBy(x => new { x.MaPT, x.MaSach }) // Nhóm theo MaPT và MaSach
                .Select(g => g.First()) // Lấy phần tử đầu tiên trong mỗi nhóm
                .ToList();

            return listPhieutra_All;
        }
    }
}

// private List<PhieuTra_GroupMaPM_DTO> listPhieutra_All;
// public IEnumerable<DTO_Sach_Tra> Get_ChiTietPT_ByMaPM(int maPM)
//public List<DTO_Sach_Tra> Get_ChiTietPT_ByMaPM(int maPM)
//{
//    var listPhieutra_All =
//        (from pt in _context.PhieuTras
//         join ctpt in _context.ChiTietPts on pt.Mapt equals ctpt.Mapt
//         join ctst in _context.ChiTietSachTras on ctpt.Mapt equals ctst.Mapt
//         join s in _context.Saches on ctpt.Masach equals s.Masach
//         join cs in _context.CuonSaches on ctst.Macuonsach equals cs.Macuonsach
//         join pm in _context.PhieuMuons on pt.Mapm equals pm.Mapm
//         join ctpm in _context.ChiTietPms on pm.Mapm equals ctpm.Mapm
//         where pt.Mapm == maPM && cs.Masach == ctpt.Masach
//         group new { pt, ctpt, s, ctpm } by new
//         {
//             pt.Mapt,
//             s.Masach,
//             s.Tensach,
//             ctpm.Soluongmuon,
//             ctpt.Soluongtra,
//             ctpt.Soluongloi,
//             ctpt.Soluongmat,
//             ctpt.Phuthu
//         } into groupedData
//         select new DTO_Sach_Tra
//         {
//             MaPT = groupedData.Key.Mapt,
//             MaSach = groupedData.Key.Masach,
//             TenSach = groupedData.Key.Tensach,
//             SoLuongMuon = groupedData.Key.Soluongmuon ?? 0,
//             SoLuongTra = groupedData.Key.Soluongtra ?? 0,
//             SoLuongLoi = groupedData.Key.Soluongloi ?? 0,
//             SoLuongMat = groupedData.Key.Soluongmat ?? 0,
//             PhuThu = groupedData.Key.Phuthu ?? 0,
//             ListCTSachTra = groupedData
//                 .SelectMany(g => _context.ChiTietSachTras
//                     .Where(ctst => ctst.Mapt == g.pt.Mapt)
//                     .Select(ctst => new DTO_CT_Sach_Tra
//                     {
//                         MaPT = ctst.Mapt,
//                         MaCuonSach = ctst.Macuonsach,
//                         Tinhtrang = ctst.Tinhtrang ?? 0
//                     }))
//                 .Distinct()
//                 .ToList()
//         }).ToList();

//    return listPhieutra_All;
//}