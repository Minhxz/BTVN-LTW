using System;
using System.Collections.Generic;

namespace BùiQuangMinh_241230793.Models;

public partial class LoaiHang
{
    public int MaLoai { get; set; }

    public string TenLoai { get; set; } = null!;

    public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();
}
