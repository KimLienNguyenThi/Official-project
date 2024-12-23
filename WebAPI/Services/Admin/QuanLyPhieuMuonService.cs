using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;

namespace WebAPI.Services.Admin
{
    public class QuanLyPhieuMuonService
    {
        private readonly QuanLyThuVienContext _context;

        public QuanLyPhieuMuonService(QuanLyThuVienContext context)
        {
            _context = context;
        }

        public async Task<PagingResult<PhieuMuon_GroupMaDG_DTO>> GetAllDocGiaPaging(GetListPhieuTraPaging req)
        {

            var query =
                (from DocGia in _context.DocGia
                 join PhieuMuon in _context.PhieuMuons
                 on DocGia.Madg equals PhieuMuon.Mathe
                 join NhanVien in _context.NhanViens
                 on PhieuMuon.Manv equals NhanVien.Manv
                 where string.IsNullOrEmpty(req.Keyword) || DocGia.Hotendg.Contains(req.Keyword)
                 select new PhieuMuonDTO
                 {
                     MaPM = PhieuMuon.Mapm,
                     NgayMuon = PhieuMuon.Ngaymuon,
                     HanTra = PhieuMuon.Hantra,
                     MaThe = DocGia.Madg,
                     HoTenDG = DocGia.Hotendg,
                     SDT = DocGia.Sdt,
                     MaNV = NhanVien.Manv,
                     Tinhtrang = (bool)PhieuMuon.Tinhtrang

                 }).AsQueryable()
            .GroupBy(g => new { g.MaThe, g.HoTenDG, g.SDT }, (key, g) => new PhieuMuon_GroupMaDG_DTO
            {
                DocGia_GroupKey = new DocGia_GroupKey()
                {
                    MaThe = key.MaThe,
                    HoTenDG = key.HoTenDG,
                    SDT = key.SDT,

                },
                DataPhieuMuons = g.Select(phieumuon => new PhieuMuonDTO
                {
                    MaPM = phieumuon.MaPM,
                    NgayMuon = phieumuon.NgayMuon,
                    HanTra = phieumuon.HanTra,
                    MaThe = phieumuon.MaThe,
                    HoTenDG = phieumuon.HoTenDG,
                    SDT = phieumuon.SDT,
                    MaNV = phieumuon.MaNV,
                    Tinhtrang = phieumuon.Tinhtrang,
                }).ToList(),
                //CountRow = g.Count()
            });
            var totalRow = await query.CountAsync();

            var listPhieumuons = await query.OrderByDescending(x => x.DocGia_GroupKey.MaThe).Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToListAsync();

            return new PagingResult<PhieuMuon_GroupMaDG_DTO>()
            {
                Results = listPhieumuons,
                CurrentPage = req.Page,
                RowCount = totalRow,
                PageSize = req.PageSize
            };
        }


        private List<PhieuMuon_GroupMaDG_DTO> listPhieumuon_All;
        // public IEnumerable<DTO_Sach_Tra> Get_ChiTietPT_ByMaPM(int maPM)
        public List<SachMuon_allPmDTO> Get_ChiTietPM_ByMaDG(int maThe)
        {

            // Using LINQ to query the data
            var listPhieumuon_All =
                (from chiTietPM in _context.ChiTietPms
                 join sach in _context.Saches on chiTietPM.Masach equals sach.Masach
                 join phieuMuon in _context.PhieuMuons on chiTietPM.Mapm equals phieuMuon.Mapm
                

                 where phieuMuon.Mathe == maThe
                 select new SachMuon_allPmDTO
                 {
                     MaPM = phieuMuon.Mapm,
                     MaSach = sach.Masach,
                     
                     TenSach = sach.Tensach,
                     SoLuongMuon = chiTietPM.Soluongmuon ?? 0,

                 }).ToList();

            foreach (var dto in listPhieumuon_All)
            {
                // Lấy danh sách chi tiết sách trả cho từng phiếu trả
                dto.listCTQLSachMuon = _context.ChiTietSachMuons
                    .Where(ctsm => ctsm.Mapm == dto.MaPM)
                    .Join(_context.CuonSaches,
                          ctsm => ctsm.Macuonsach,
                          cs => cs.Macuonsach,
                          (ctsm, cs) => new { ctsm, cs })
                    .Where(joined => joined.cs.Masach == dto.MaSach)
                    .Select(joined => new DTO_CT_Sach_Muon_QL
                    {
                        MaPM = joined.ctsm.Mapm,
                        MaCuonSach = joined.ctsm.Macuonsach,
                         
                    })
                    .Distinct() // Loại bỏ bản ghi trùng lặp
                    .ToList();
            }



            // Returning the distinct list based on specified fields
            // Loại bỏ các đối tượng trùng lặp trong listPhieutra_All
            listPhieumuon_All = listPhieumuon_All
                .GroupBy(x => new { x.MaPM, x.MaSach }) // Nhóm theo MaPT và MaSach
                .Select(g => g.First()) // Lấy phần tử đầu tiên trong mỗi nhóm
                .ToList();

            return listPhieumuon_All;
        }

        public List<SachMuon_allPmDTO> Get_ChiTietPM_ByMaPM(int maPM)
        {

            // Using LINQ to query the data
            var listPhieumuon_All =
                (from chiTietPM in _context.ChiTietPms
                 join sach in _context.Saches on chiTietPM.Masach equals sach.Masach
                 join phieuMuon in _context.PhieuMuons on chiTietPM.Mapm equals phieuMuon.Mapm
                 
                 where phieuMuon.Mapm == maPM
                 select new SachMuon_allPmDTO
                 {
                     MaPM = phieuMuon.Mapm,
                     MaSach = sach.Masach,
                     
                     TenSach = sach.Tensach,
                     SoLuongMuon = chiTietPM.Soluongmuon ?? 0,

                 }).ToList();
            foreach (var dto in listPhieumuon_All)
            {
                // Lấy danh sách chi tiết sách trả cho từng phiếu trả
                dto.listCTQLSachMuon = _context.ChiTietSachMuons
                    .Where(ctsm => ctsm.Mapm == dto.MaPM)
                    .Join(_context.CuonSaches,
                          ctsm => ctsm.Macuonsach,
                          cs => cs.Macuonsach,
                          (ctsm, cs) => new { ctsm, cs })
                    .Where(joined => joined.cs.Masach == dto.MaSach)
                    .Select(joined => new DTO_CT_Sach_Muon_QL
                    {
                        MaPM = joined.ctsm.Mapm,
                        MaCuonSach = joined.ctsm.Macuonsach,

                    })
                    .Distinct() // Loại bỏ bản ghi trùng lặp
                    .ToList();
            }

            // Returning the distinct list based on specified fields
            listPhieumuon_All = listPhieumuon_All
                .GroupBy(x => new { x.MaPM, x.MaSach }) // Nhóm theo MaPT và MaSach
                .Select(g => g.First()) // Lấy phần tử đầu tiên trong mỗi nhóm
                .ToList();

            return listPhieumuon_All;
        }
    }
}

