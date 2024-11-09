using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class ChiTietDk
{
    public int Madk { get; set; }

    public int Masach { get; set; }

    public int? Soluongmuon { get; set; }

    public virtual DkiMuonSach MadkNavigation { get; set; } = null!;

    public virtual Sach MasachNavigation { get; set; } = null!;
}
