using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Sach
{
    public int Masach { get; set; }

    public string? Tensach { get; set; }

    public string? Theloai { get; set; }

    public string? Tacgia { get; set; }

    public string? Ngonngu { get; set; }

    public string? Nxb { get; set; }

    public int? Namxb { get; set; }

    public string? UrlImage { get; set; }

    public string? Mota { get; set; }

    public int? Soluonghientai { get; set; }

    public virtual ICollection<ChiTietDk> ChiTietDks { get; set; } = new List<ChiTietDk>();

    public virtual ICollection<ChiTietPm> ChiTietPms { get; set; } = new List<ChiTietPm>();

    public virtual ICollection<ChiTietPt> ChiTietPts { get; set; } = new List<ChiTietPt>();

    public virtual ICollection<Chitietpn> Chitietpns { get; set; } = new List<Chitietpn>();

    public virtual ICollection<CuonSach> CuonSaches { get; set; } = new List<CuonSach>();
}
