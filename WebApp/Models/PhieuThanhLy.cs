using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class PhieuThanhLy
{
    public int Maptl { get; set; }

    public DateOnly? Ngaytl { get; set; }

    public int? Madv { get; set; }

    public int? Manv { get; set; }

    public virtual ICollection<ChiTietPtl> ChiTietPtls { get; set; } = new List<ChiTietPtl>();

    public virtual DonViTl? MadvNavigation { get; set; }

    public virtual NhanVien? ManvNavigation { get; set; }

    public virtual ICollection<ChitietKhoThanhLy> Macuonsaches { get; set; } = new List<ChitietKhoThanhLy>();
}
