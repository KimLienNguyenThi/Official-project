using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Chitietpn
{
    public int Mapn { get; set; }

    public int Masach { get; set; }

    public decimal? Giasach { get; set; }

    public int? Soluongnhap { get; set; }

    public virtual PhieuNhapSach MapnNavigation { get; set; } = null!;

    public virtual Sach MasachNavigation { get; set; } = null!;
}
