using System.ComponentModel.DataAnnotations;

namespace BùiQuangMinh_241230793.Models;

public partial class TaiSan
{
    public int MaTS { get; set; }

    public int MaLoai { get; set; }

    [Required(ErrorMessage = "Tên tài sản không được để trống.")]
    [StringLength(100)]
    public string TenTS { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập nguyên giá.")]
    [Range(1, 2000, ErrorMessage = "Nguyên giá phải từ 1 đến 2000 triệu.")]
    public decimal? NguyenGia { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên file ảnh.")]
    [StringLength(100)]
    [RegularExpression(@"(?i)^.+\.(jpg|jpeg|png|webp)$",
        ErrorMessage = "Ảnh phải có đuôi .jpg, .jpeg, .png hoặc .webp.")]
    public string? Anh { get; set; }

    public virtual LoaiTaiSan? MaLoaiNavigation { get; set; }
}