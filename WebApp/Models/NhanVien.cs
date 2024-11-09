using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class NhanVien
{
    public int Manv { get; set; }

    public string? Hotennv { get; set; }

    public string? Gioitinh { get; set; }

    public string? Diachi { get; set; }

    public DateOnly? Ngaysinh { get; set; }

    public string? Sdt { get; set; }

    public string? Chucvu { get; set; }

    public virtual ICollection<LoginNv> LoginNvs { get; set; } = new List<LoginNv>();

    public virtual ICollection<PhieuMuon> PhieuMuons { get; set; } = new List<PhieuMuon>();

    public virtual ICollection<PhieuNhapSach> PhieuNhapSaches { get; set; } = new List<PhieuNhapSach>();

    public virtual ICollection<PhieuThanhLy> PhieuThanhLies { get; set; } = new List<PhieuThanhLy>();

    public virtual ICollection<PhieuTra> PhieuTras { get; set; } = new List<PhieuTra>();

    public virtual ICollection<TheDocGium> TheDocGia { get; set; } = new List<TheDocGium>();
}
