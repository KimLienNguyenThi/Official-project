using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class CuonSach
{
    public string Macuonsach { get; set; } = null!;

    public int? Tinhtrang { get; set; }

    public int Masach { get; set; }

    public virtual ICollection<ChiTietSachMuon> ChiTietSachMuons { get; set; } = new List<ChiTietSachMuon>();

    public virtual ICollection<ChiTietSachTra> ChiTietSachTras { get; set; } = new List<ChiTietSachTra>();

    public virtual ChitietKhoThanhLy? ChitietKhoThanhLy { get; set; }

    public virtual Sach MasachNavigation { get; set; } = null!;
}
