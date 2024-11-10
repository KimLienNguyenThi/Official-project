using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class LoginNv
{
    public string UsernameNv { get; set; } = null!;

    public string? PasswordNv { get; set; }

    public int? Manv { get; set; }

    public virtual NhanVien? ManvNavigation { get; set; }
}
