namespace WebApp.Areas.Admin.Data
{
    public class PhieuMuonDTO
    {
        public int MaPM { get; set; }
        public DateOnly? NgayMuon { get; set; }

        public DateOnly? HanTra { get; set; }

        public int MaNV { get; set; }

        public String HoTenNV { get; set; }
        public int MaThe { get; set; }
        public String HoTenDG { get; set; }
        public String SDT { get; set; }
        public bool Tinhtrang { get; set; }

        public int MaDK { get; set; }

    }

    public class SachMuon_allPmDTO
    {
        public int MaPM { get; set; }
        public int MaSach { get; set; }
        public string TenSach { get; set; }

        public int SoLuongMuon { get; set; }

    }
}
