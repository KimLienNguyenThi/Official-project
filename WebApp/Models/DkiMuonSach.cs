using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class DkiMuonSach
{
    public int Madk { get; set; }

    public string? Sdt { get; set; }

    public DateOnly? Ngaydkmuon { get; set; }

    public DateOnly? Ngayhen { get; set; }

    public int? Tinhtrang { get; set; }

    public virtual ICollection<ChiTietDk> ChiTietDks { get; set; } = new List<ChiTietDk>();

    public virtual LoginDg? SdtNavigation { get; set; }
}
