using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class LoginDg
{
    public string Sdt { get; set; } = null!;

    public string? PasswordDg { get; set; }

    public string? Hoten { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<DkiMuonSach> DkiMuonSaches { get; set; } = new List<DkiMuonSach>();
}
