namespace WebApp.DTOs.Admin
{
    public class DKiMuonSachDTO_PM
    {
        public int MaThe { get; set; }
        public string SDT { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MaDK { get; set; }
        public DateOnly? NgayDK { get; set; }
        public DateOnly? NgayHen { get; set; }
        public int? TinhTrang { get; set; }
    }

    public class MaSachCuonSachDto
    {
        public int MaSach { get; set; }
        public List<string> MaCuonSach { get; set; }
    }
}
