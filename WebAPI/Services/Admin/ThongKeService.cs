using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WebAPI.Areas.Admin.Data;
using WebAPI.DTOs.Admin_DTO;
using WebAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.Service_Admin
{
    public class ThongKeService
    {
        private readonly QuanLyThuVienContext _context;

        public ThongKeService(QuanLyThuVienContext context)
        {
            _context = context;
        }

        //sách được mượn đọc top 5
        public List<ThongKeSach> GetSachMuon(DateOnly? tungay, DateOnly? denngay)
        {

            var result = (
                from pm in _context.PhieuMuons
                join ctpm in _context.ChiTietPms on pm.Mapm equals ctpm.Mapm
                join s in _context.Saches on ctpm.Masach equals s.Masach
                where pm.Ngaymuon >= tungay && pm.Ngaymuon <= denngay
                group new { ctpm, s } by new { s.Masach, s.Tensach, s.Theloai } into g
                select new ThongKeSach
                {
                    MaSach = g.Key.Masach,
                    TenSach = g.Key.Tensach,
                    TheLoai = g.Key.Theloai,
                    SoLuong = g.Sum(x => x.ctpm.Soluongmuon)
                })
               .OrderByDescending(ts => ts.SoLuong)
                .ToList();

            return result;
        }


        ////tổng số phiếu mượn
        public ThongKePhieu GetPhieuMuon(DateOnly? tungay, DateOnly? denngay)
        {

            var result = (
                from pm in _context.PhieuMuons
                where pm.Ngaymuon >= tungay && pm.Ngaymuon <= denngay
                select pm);
            var count = result.Count();


            return new ThongKePhieu
            {
                SoLuong = count
            };

        }

        //lấy phiếu mượn 
        public List<ThongKePM> GetListPM(DateOnly? tungay, DateOnly? denngay)
        {

            var query =
                (from PhieuMuon in _context.PhieuMuons

                 where PhieuMuon.Ngaymuon >= tungay && PhieuMuon.Ngaymuon <= denngay

                 select new ThongKePM
                 {
                     MaPM = PhieuMuon.Mapm,
                     MaThe = PhieuMuon.Mathe.Value,
                     NgayMuon = PhieuMuon.Ngaymuon,
                     Tinhtrang = PhieuMuon.Tinhtrang
                 }
                ).ToList();

            return query;
        }

        //tổng số lượng phiếu trả
        public ThongKePhieu GetPhieuTra(DateOnly? tungay, DateOnly? denngay)
        {

            var result = (
                from pt in _context.PhieuTras
                where pt.Ngaytra >= tungay && pt.Ngaytra <= denngay
                select pt);
            var count = result.Count();


            return new ThongKePhieu
            {
                SoLuong = count
            };

        }
        public List<ThongKePT> GetListPT(DateOnly? tungay, DateOnly? denngay)
        {

            var query =
                (from pt in _context.PhieuTras
                 where pt.Ngaytra >= tungay && pt.Ngaytra <= denngay

                 select new ThongKePT
                 {
                     MaPT = pt.Mapt,
                     MaThe = pt.Mathe.Value,
                     NgayTra = pt.Ngaytra,
                 }
                ).ToList();

            return query;
        }

        //lấy  độc giả mượn 
        public List<ThongKeDocGia_Muon> GetDocGiaMuon(DateOnly? tungay, DateOnly? denngay)
        {

            var result = (
                from DocGium in _context.DocGia
                join TheDocGium in _context.TheDocGia on DocGium.Madg equals TheDocGium.Madg
                join PhieuMuon in _context.PhieuMuons on TheDocGium.Mathe equals PhieuMuon.Mathe
                where PhieuMuon.Ngaymuon >= tungay && PhieuMuon.Ngaymuon <= denngay
                group new { TheDocGium, DocGium, PhieuMuon } by new { TheDocGium.Mathe, DocGium.Hotendg } into g
                select new ThongKeDocGia_Muon
                {
                    MaThe = g.Key.Mathe,
                    HoTenDg = g.Key.Hotendg,
                    SoLuong = g.Count()
                })
                .OrderByDescending(ts => ts.SoLuong)
                .ToList();

            return result;
        }

        //độc giả đăng ký thẻ  trong khoảng thời gian đó
        public List<ThongKeDocGia_Dki> GetDocGiaDki(DateOnly? tungay, DateOnly? denngay)
        {

            var result = (
                 from DocGium in _context.DocGia
                 join TheDocGium in _context.TheDocGia on DocGium.Madg equals TheDocGium.Madg
                 where TheDocGium.Ngaydk >= tungay && TheDocGium.Ngaydk <= denngay
                 group new { TheDocGium, DocGium } by new { TheDocGium.Mathe, DocGium.Hotendg, TheDocGium.Ngaydk } into g
                 select new ThongKeDocGia_Dki
                 {
                     MaThe = g.Key.Mathe,
                     HoTenDg = g.Key.Hotendg,
                     NgayDki = g.Key.Ngaydk,
                 })

                .ToList();

            return result;
        }


        public async Task<ThongKeDoanhThu> GetMoney(DateOnly? tungay, DateOnly? denngay)
        {
            //using (var context2 = new QuanLyThuVienContext())
            //{
            var totalMoneyData = new ThongKeDoanhThu();

            var totalPhuthu = await (
                from ct in _context.ChiTietPts
                join pt in _context.PhieuTras on ct.Mapt equals pt.Mapt
                where pt.Ngaytra >= tungay && pt.Ngaytra <= denngay
                select ct.Phuthu
            ).SumAsync();

            var totalThanhLy = await (
                from ctl in _context.ChiTietPtls
                join ptl in _context.PhieuThanhLies on ctl.Maptl equals ptl.Maptl
                where ptl.Ngaytl >= tungay && ptl.Ngaytl <= denngay
                select ctl.Giatl * ctl.Soluongtl
            ).SumAsync();

            var totalTheDocGia = await _context.TheDocGia
                .Where(x => x.Ngaydk >= tungay && x.Ngaydk <= denngay)
                .SumAsync(x => x.Tienthe);

            var totalNhapSach = await (
                from ctpn in _context.Chitietpns
                join pn in _context.PhieuNhapSaches on ctpn.Mapn equals pn.Mapn
                where pn.Ngaynhap >= tungay && pn.Ngaynhap <= denngay
                select ctpn.Giasach * ctpn.Soluongnhap
            ).SumAsync();

            //await Task.WhenAll(totalPhuthu, totalThanhLy, totalTheDocGia, totalNhapSach);


            totalMoneyData.TotalTraSach = totalPhuthu ?? 0;
            totalMoneyData.TotalThanhLy = totalThanhLy ?? 0;
            totalMoneyData.TotalTheDocGia = totalTheDocGia ?? 0;
            totalMoneyData.TotalNhapSach = totalNhapSach ?? 0;

            return totalMoneyData;
            //}
        }


    }
}
////tổng sách nhập về kho
//public ThongKePhieu GetTongSachNhap()
//{

//    // Tính tổng số lượng sách hiện tại trong kho
//    int? tongSoLuong = _context.Chitietpns.Sum(s => s.SoLuongNhap);

//    // Tạo một danh sách chứa một đối tượng ThongKePhieu với tổng số lượng sách
//    var result = new ThongKePhieu
//    {

//        SoLuong = tongSoLuong.Value

//    };

//    return result;
//}


////tổng sách trong kho hiện tại
//public ThongKePhieu GetTongSachHienTai()
//{

//    // Tính tổng số lượng sách hiện tại trong kho
//    int? tongSoLuong = _context.Saches.Sum(s => s.SoLuongHientai);

//    // Tạo một danh sách chứa một đối tượng ThongKePhieu với tổng số lượng sách
//    var result = new ThongKePhieu
//    {

//        SoLuong = tongSoLuong.Value

//    };

//    return result;
//}





////sách được nhập về trong khoảng tgian đó
//public List<ThongKeSach> GetSachNhap(DateOnly tungay, DateOnly denngay)
//{

//    var result = (
//        from pn in _context.PhieuNhapSaches
//        join ctpn in _context.Chitietpns on pn.MaPn equals ctpn.MaPn
//        join s in _context.Saches on ctpn.MaSach equals s.MaSach
//        where pn.NgayNhap >= tungay && pn.NgayNhap <= denngay
//        group new { ctpn, s } by new { s.MaSach, s.TenSach } into g
//        select new ThongKeSach
//        {
//            MaSach = g.Key.MaSach,
//            TenSach = g.Key.TenSach,
//            SoLuong = g.Sum(x => x.ctpn.SoLuongNhap)
//        })
//        .OrderByDescending(ts => ts.SoLuong)
//        .ToList();

//    return result;
//}

////tổng sách mượn
//public ThongKePhieu GetTongSachMuon()
//{


//    int? tongSoLuong = _context.ChiTietPms.Sum(s => s.Soluongmuon);


//    var result = new ThongKePhieu
//    {

//        SoLuong = tongSoLuong.Value

//    };

//    return result;
//}