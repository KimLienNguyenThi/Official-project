using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class NhaCungCap
{
    public int Mancc { get; set; }

    public string? Tenncc { get; set; }

    public string? Diachincc { get; set; }

    public string? Sdtncc { get; set; }

    public virtual ICollection<PhieuNhapSach> PhieuNhapSaches { get; set; } = new List<PhieuNhapSach>();
}
