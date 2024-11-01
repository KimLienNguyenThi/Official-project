using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class ChiTietPtl
{
    public int Maptl { get; set; }

    public int Masachkho { get; set; }

    public int? Soluongtl { get; set; }

    public decimal? Giatl { get; set; }

    public virtual PhieuThanhLy MaptlNavigation { get; set; } = null!;

    public virtual KhoSachThanhLy MasachkhoNavigation { get; set; } = null!;
}
