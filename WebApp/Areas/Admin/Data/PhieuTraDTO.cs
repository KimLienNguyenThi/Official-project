namespace WebApp.Areas.Admin.Data
{
    public class PhieuTra_DTO
    {

        public int MaPT { get; set; }
        public DateOnly? NgayTra { get; set; }

        public int MaNV { get; set; }

        public String HoTenNV { get; set; }

        public int MaPM { get; set; }

        public int MaThe { get; set; }
        public String HoTenDG { get; set; }
        public String SDT { get; set; }

    }

    public class PhieuTra_DTO1
    {

        public int MaPT { get; set; }
        public DateOnly? NgayMuon { get; set; }

        public DateOnly? NgayTra { get; set; }

        public int MaNV { get; set; }

        public String HoTenNV { get; set; }

        public int MaPM { get; set; }

        public int MaThe { get; set; }
        public String HoTenDG { get; set; }
        public String SDT { get; set; }

    }

    public class DTO_Sach_Tra
        {
            public int MaPT { get; set; }
            public int MaSach { get; set; }
            public string TenSach { get; set; }

           // public int SoLuongMuon { get; set; }
            public int SoLuongTra { get; set; }
            public int SoLuongLoi { get; set; }
            public int SoLuongMat { get; set; }
            public decimal PhuThu { get; set; }

        }

        public class DTO_Tao_Phieu_Tra
        {
            public int MaNhanVien { get; set; }
            public int MaTheDocGia { get; set; }
            public DateOnly? NgayMuon { get; set; }
            public DateOnly? HanTra { get; set; }
            public DateOnly? NgayTra { get; set; }

            public List<DTO_Sach_Tra> ListSachTra { get; set; }
            public int MaPhieuMuon { get; set; }

            public decimal PhuThu { get; set; } // Sử dụng decimal cho giá trị tiền tệ
        }
    public class PhieuTra_GroupMaPM_DTO
    {
        public PhieuTra_GroupKey PhieuTra_GroupKey { get; set; }
        public List<PhieuTra_DTO1> DataPhieuTras { get; set; }
    }

    public class PhieuTra_GroupKey
    {
        public int MaPM { get; set; }

        public int MaThe { get; set; }
        public String HoTenDG { get; set; }
        public String SDT { get; set; }
    }

    public class GetListPhieuTraPaging
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; } = string.Empty;
    }
  
}
