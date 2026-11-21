using System.ComponentModel.DataAnnotations;

namespace BùiQuangMinh_241230793.Models;

public partial class HangHoa
{
    public int MaHang { get; set; }
    public int MaLoai { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên hàng.")]
    [StringLength(50, ErrorMessage = "Tên hàng tối đa 50 ký tự.")]
    public string TenHang { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập giá.")]
    [Range(100, 5000, ErrorMessage = "Giá phải từ 100 đến 5000.")]
    public decimal? Gia { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên file ảnh.")]
    [StringLength(100, ErrorMessage = "Tên file ảnh tối đa 100 ký tự.")]
    [RegularExpression(@"(?i)^.+\.(jpg|png|gif|tiff)$",
        ErrorMessage = "Ảnh phải có đuôi .jpg, .png, .gif hoặc .tiff.")]
    public string? Anh { get; set; }

    public virtual LoaiHang? MaLoaiNavigation { get; set; }
}