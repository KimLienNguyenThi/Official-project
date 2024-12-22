using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class QuyDinh
{
    public int MaQuyDinh {  get; set; }
    public int NamXbmax { get; set; }

    public int? SosachmuonMax { get; set; }

    public int? SongayMax { get; set; }
}
