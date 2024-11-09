namespace WebAPI.DTOs.Admin_DTO
{
    public class SachMuonDTO
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }

        public int? SoLuongMuon { get; set; }
        public decimal giasach { get; set; }
    }
    public class SachDaTraDTO
    {
        public int MaSach { get; set; }
        public int? SoLuongDaTra { get; set; }
    }
}
