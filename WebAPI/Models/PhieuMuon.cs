using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class PhieuMuon
{
    public int Mapm { get; set; }

    public int? Mathe { get; set; }

    public DateOnly? Ngaymuon { get; set; }

    public DateOnly? Hantra { get; set; }

    public int? Manv { get; set; }

    public bool? Tinhtrang { get; set; }

    public int? Madk { get; set; }

    public virtual ICollection<ChiTietPm> ChiTietPms { get; set; } = new List<ChiTietPm>();

    public virtual NhanVien? ManvNavigation { get; set; }

    public virtual TheDocGium? MatheNavigation { get; set; }

    public virtual ICollection<PhieuTra> PhieuTras { get; set; } = new List<PhieuTra>();

    public virtual ICollection<CuonSach> Macuonsaches { get; set; } = new List<CuonSach>();
}
