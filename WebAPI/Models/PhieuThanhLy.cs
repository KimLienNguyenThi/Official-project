using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class PhieuThanhLy
{
    public int Maptl { get; set; }

    public DateOnly? Ngaytl { get; set; }

    public int? Madv { get; set; }

    public int? Manv { get; set; }

    public virtual ICollection<ChiTietPtl> ChiTietPtls { get; set; } = new List<ChiTietPtl>();

    public virtual ICollection<ChiTietSachThanhLy> ChiTietSachThanhLies { get; set; } = new List<ChiTietSachThanhLy>();

    public virtual DonViTl? MadvNavigation { get; set; }

    public virtual NhanVien? ManvNavigation { get; set; }
}
