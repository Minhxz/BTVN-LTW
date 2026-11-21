using System;
using System.Collections.Generic;

namespace BùiQuangMinh_241230793.Models;

public partial class LoaiTaiSan
{
    public int MaLoai { get; set; }

    public string TenLoai { get; set; } = null!;

    public virtual ICollection<TaiSan> TaiSan { get; set; } = new List<TaiSan>();
}
