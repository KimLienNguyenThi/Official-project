using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class DonViTl
{
    public int Madv { get; set; }

    public string? Tendv { get; set; }

    public string? Diachidv { get; set; }

    public string? Sdtdv { get; set; }

    public virtual ICollection<PhieuThanhLy> PhieuThanhLies { get; set; } = new List<PhieuThanhLy>();
}
