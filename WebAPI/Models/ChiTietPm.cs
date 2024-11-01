using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class ChiTietPm
{
    public int Mapm { get; set; }

    public int? Soluongmuon { get; set; }

    public int Masach { get; set; }

    public virtual PhieuMuon MapmNavigation { get; set; } = null!;

    public virtual Sach MasachNavigation { get; set; } = null!;
}
