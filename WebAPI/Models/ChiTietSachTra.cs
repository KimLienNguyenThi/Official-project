using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class ChiTietSachTra
{
    public int Mapt { get; set; }

    public string Macuonsach { get; set; } = null!;

    public int? Tinhtrang { get; set; }

    public virtual CuonSach MacuonsachNavigation { get; set; } = null!;

    public virtual PhieuTra MaptNavigation { get; set; } = null!;
}
