namespace WebAPI.Models
{
    public class ImportSachTemp
    {
        public int Id { get; set; }
        public string TenSach { get; set; }
        public string TheLoai { get; set; }
        public int NamXuatBan { get; set; }
        public string NXB { get; set; }

        public string TacGia { get; set; }
        public int SoLuong { get; set; }

        public string NgonNgu { get; set; }

        public decimal GiaSach { get; set; }
        public string URLImage { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
        public string MoTaLoi { get; set; }
    }

}
