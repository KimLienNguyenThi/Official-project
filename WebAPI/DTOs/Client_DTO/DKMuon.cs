namespace WebAPI.DTOs.Client_DTO
{
    public class DKMuon
    {
        public string? Sdt { get; set; }

        public DateOnly? Ngaydkmuon { get; set; }

        public DateOnly? Ngayhen { get; set; }

        public int? Tinhtrang { get; set; }

        public List<SachDK> ListSach { get; set; }
    }

    public class SachDK
    {
        public int MaDK { get; set; }
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int? Soluongmuon { get; set; }
    }
}
