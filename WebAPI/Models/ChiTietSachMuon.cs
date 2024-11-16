using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class ChiTietSachMuon
{
    public int Mapm { get; set; }

    public string Macuonsach { get; set; } = null!;

    public bool? Tinhtrang { get; set; }

    public virtual CuonSach MacuonsachNavigation { get; set; } = null!;

    public virtual PhieuMuon MapmNavigation { get; set; } = null!;
}
