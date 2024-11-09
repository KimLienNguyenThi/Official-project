using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class CuonSach
{
    public string Macuonsach { get; set; } = null!;

    public int? Tinhtrang { get; set; }

    public int Masach { get; set; }

    public virtual ICollection<ChiTietSachTra> ChiTietSachTras { get; set; } = new List<ChiTietSachTra>();

    public virtual ChitietKhoThanhLy? ChitietKhoThanhLy { get; set; }

    public virtual Sach MasachNavigation { get; set; } = null!;

    public virtual ICollection<PhieuMuon> Mapms { get; set; } = new List<PhieuMuon>();
}
