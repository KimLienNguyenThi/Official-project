using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class ChiTietPt
{
    public int Mapt { get; set; }

    public int Masach { get; set; }

    public int? Soluongtra { get; set; }

    public int? Soluongloi { get; set; }

    public int? Soluongmat { get; set; }

    public decimal? Phuthu { get; set; }

    public virtual PhieuTra MaptNavigation { get; set; } = null!;

    public virtual Sach MasachNavigation { get; set; } = null!;
}
