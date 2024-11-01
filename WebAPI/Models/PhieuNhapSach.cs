using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class PhieuNhapSach
{
    public int Mapn { get; set; }

    public DateOnly? Ngaynhap { get; set; }

    public int? Manv { get; set; }

    public int? Mancc { get; set; }

    public virtual ICollection<Chitietpn> Chitietpns { get; set; } = new List<Chitietpn>();

    public virtual NhaCungCap? ManccNavigation { get; set; }

    public virtual NhanVien? ManvNavigation { get; set; }
}
