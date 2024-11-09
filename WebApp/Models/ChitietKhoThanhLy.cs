using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class ChitietKhoThanhLy
{
    public int Masachkho { get; set; }

    public string Macuonsach { get; set; } = null!;

    public int? Vande { get; set; }

    public int? Tinhtrang { get; set; }

    public virtual CuonSach MacuonsachNavigation { get; set; } = null!;

    public virtual KhoSachThanhLy MasachkhoNavigation { get; set; } = null!;

    public virtual ICollection<PhieuThanhLy> Maptls { get; set; } = new List<PhieuThanhLy>();
}
