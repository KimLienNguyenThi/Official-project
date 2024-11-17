using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class ChitietKhoThanhLy
{
    public int Masachkho { get; set; }

    public string Macuonsach { get; set; } = null!;

    public int? Vande { get; set; }

    public int? Tinhtrang { get; set; }

    public virtual ICollection<ChiTietSachThanhLy> ChiTietSachThanhLies { get; set; } = new List<ChiTietSachThanhLy>();

    public virtual CuonSach MacuonsachNavigation { get; set; } = null!;

    public virtual KhoSachThanhLy MasachkhoNavigation { get; set; } = null!;
}
