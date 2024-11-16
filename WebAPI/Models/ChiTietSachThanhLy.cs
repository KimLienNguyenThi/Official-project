using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class ChiTietSachThanhLy
{
    public int Maptl { get; set; }

    public string Macuonsach { get; set; } = null!;

    public int? Tinhtrang { get; set; }

    public virtual ChitietKhoThanhLy MacuonsachNavigation { get; set; } = null!;

    public virtual PhieuThanhLy MaptlNavigation { get; set; } = null!;
}
