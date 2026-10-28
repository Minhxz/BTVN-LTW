using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Lab3Validation.Models
{
    public enum Branch
    {
        [Display(Name = "Công nghệ thông tin (IT)")] IT = 1,
        [Display(Name = "Kỹ thuật sinh học (BE)")] BE = 2,
        [Display(Name = "Kỹ thuật máy tính (CE)")] CE = 3,
        [Display(Name = "Điện - Điện tử (EE)")] EE = 4
    }

    public enum Gender
    {
        [Display(Name = "Nam")] Male,
        [Display(Name = "Nữ")] Female,
        [Display(Name = "Khác")] Other
    }

    public static class EnumExtensions
    {
        // Lấy tên hiển thị (tiếng Việt) từ [Display(Name=...)] của enum
        public static string GetDisplayName(this Enum value)
        {
            var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            return member?.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? value.ToString();
        }
    }

    public class Student
    {
        public int Id { get; set; } // Mã sinh viên

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự")]
        public string? Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$",
            ErrorMessage = "Email không hợp lệ, phải có đuôi @gmail.com")]
        public string? Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 đến 100 ký tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$",
            ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, chữ số và ký tự đặc biệt")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Ngành học")]
        [Required(ErrorMessage = "Vui lòng chọn ngành học")]
        public Branch? Branch { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public Gender? Gender { get; set; }

        [Display(Name = "Hệ chính quy")]
        public bool IsRegular { get; set; } // true: chính quy, false: phi chính quy

        [Display(Name = "Địa chỉ")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string? Address { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        [Range(typeof(DateTime), "1963-01-01", "2005-12-31",
            ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true,
            ErrorMessage = "Ngày sinh phải từ 01/01/1963 đến 31/12/2005")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBorth { get; set; }

        [Display(Name = "Điểm")]
        [Required(ErrorMessage = "Điểm bắt buộc phải nhập")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải nằm trong khoảng từ 0.0 đến 10.0")]
        public double? Score { get; set; }
    }
}
