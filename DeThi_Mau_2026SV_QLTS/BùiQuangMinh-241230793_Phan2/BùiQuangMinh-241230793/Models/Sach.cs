using System;
using System.Collections.Generic;

namespace BùiQuangMinh_241230793.Models;

public partial class Sach
{
    public int MaSach { get; set; }

    public int MaTL { get; set; }

    public string TenSach { get; set; } = null!;

    public decimal? Gia { get; set; }

    public string? Anh { get; set; }

    public virtual TheLoai MaTLNavigation { get; set; } = null!;
}
