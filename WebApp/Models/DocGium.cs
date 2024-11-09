using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class DocGium
{
    public int Madg { get; set; }

    public string? Hotendg { get; set; }

    public string? Gioitinh { get; set; }

    public DateOnly? Ngaysinh { get; set; }

    public string? Sdt { get; set; }

    public string? Diachi { get; set; }

    public virtual ICollection<TheDocGium> TheDocGia { get; set; } = new List<TheDocGium>();
}
