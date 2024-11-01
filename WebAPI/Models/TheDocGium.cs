using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class TheDocGium
{
    public int Mathe { get; set; }

    public DateOnly? Ngaydk { get; set; }

    public DateOnly? Ngayhh { get; set; }

    public int? Tienthe { get; set; }

    public int? Manv { get; set; }

    public int? Madg { get; set; }

    public string? Email { get; set; }

    public virtual DocGium? MadgNavigation { get; set; }

    public virtual NhanVien? ManvNavigation { get; set; }

    public virtual ICollection<PhieuMuon> PhieuMuons { get; set; } = new List<PhieuMuon>();

    public virtual ICollection<PhieuTra> PhieuTras { get; set; } = new List<PhieuTra>();
}
