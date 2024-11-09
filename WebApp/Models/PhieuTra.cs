using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class PhieuTra
{
    public int Mapt { get; set; }

    public DateOnly? Ngaytra { get; set; }

    public int? Manv { get; set; }

    public int? Mathe { get; set; }

    public int? Mapm { get; set; }

    public virtual ICollection<ChiTietPt> ChiTietPts { get; set; } = new List<ChiTietPt>();

    public virtual ICollection<ChiTietSachTra> ChiTietSachTras { get; set; } = new List<ChiTietSachTra>();

    public virtual NhanVien? ManvNavigation { get; set; }

    public virtual PhieuMuon? MapmNavigation { get; set; }

    public virtual TheDocGium? MatheNavigation { get; set; }
}
