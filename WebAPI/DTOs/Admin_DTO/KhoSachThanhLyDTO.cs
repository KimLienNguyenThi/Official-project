namespace WebAPI.DTOs.Admin_DTO
{


    public class SachNhapKhoDTO
    {
        public int MaSachKho { get; set; }
        public int SoLuongKhoTL { get; set; }
      
    }
    public class KhoSachThanhLyDTO
    {
        public int MaSachKho { get; set; }

        public string TenSach { get; set; }
        public int SoLuongKhoTL { get; set; }
        public decimal GiaSachTL { get; set; }

        public List<DTO_ChitietkhoSachTl> listchitietkhoSachTL { get; set; }
        public KhoSachThanhLyDTO()
        {
            listchitietkhoSachTL = new List<DTO_ChitietkhoSachTl>();
        }
    }

    public class DTO_ChitietkhoSachTl
    {
        public string Macuonsach { get; set; }

        public int? Tinhtrang {  get; set; }
    }

    public class DTO_DonViTL
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public string DiaChiDV { get; set; }
        public string SDTDV { get; set; }
    }
    public class DTO_Sach_Tl
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }

        public decimal GiaSach { get; set; }
    }

    public class DTO_Tao_Phieu_TL
    {
        public int MaNhanVien { get; set; }
        public int MaDonVi { get; set; }
        //public DateTime NgayTL { get; set; } = DateTime.Now;
        public DateOnly? NgayTL { get; set; }

        public List<DTO_Sach_Tl> listSachTL { get; set; }

        public DTO_Tao_Phieu_TL()
        {
            listSachTL = new List<DTO_Sach_Tl>();
        }
    }

    public class DTO_Sach_Nhap_Kho
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuongHienTai { get; set; }

        public decimal GiaSach { get; set; }
    }
}
