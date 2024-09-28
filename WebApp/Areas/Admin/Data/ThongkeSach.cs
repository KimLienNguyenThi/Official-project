namespace WebApp.Areas.Admin.Data
{
    public class ThongKeSach
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int? SoLuong { get; set; }
    }
    public class ThongKePhieu
    {
        public int? SoLuong { get; set; }
    }

    public class ThongKePM
    {
        public int MaPM { get; set; }
        public int MaThe { get; set; }
        public DateOnly? NgayMuon { get; set; }

        public bool Tinhtrang { get; set; }
    }
    public class ThongKePT
    {
        public int MaPT { get; set; }
        public int MaThe { get; set; }
        public DateOnly? NgayTra { get; set; }
    }

    public class ThongKeDocGia_Muon
    {
        public int MaThe { get; set; }
        public string HoTenDg { get; set; }
        public int? SoLuong { get; set; }
    }

    public class ThongKeDocGia_Dki
    {
        public int MaThe { get; set; }
        public string HoTenDg { get; set; }
        public DateOnly? NgayDki { get; set; }
    }

    public class ThongKeDoanhThu
    {
        public decimal TotalTraSach { get; set; }
        public decimal TotalThanhLy { get; set; }
        public decimal TotalTheDocGia { get; set; }
        public decimal TotalNhapSach { get; set; }
    }
}
