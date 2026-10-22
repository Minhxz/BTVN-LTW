using System.ComponentModel.DataAnnotations;

namespace Lab2_Lesson10.Models;

public class Student
{
    [Display(Name = "Mã sinh viên")]
    public int Id { get; set; }

    [Display(Name = "Họ tên")]
    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(100, ErrorMessage = "Họ tên không quá 100 ký tự.")]
    public string? Name { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    [StringLength(200, ErrorMessage = "Email không quá 200 ký tự.")]
    public string? Email { get; set; }

    [Display(Name = "Mật khẩu")]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 đến 100 ký tự.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Ngành học")]
    [Required(ErrorMessage = "Vui lòng chọn ngành học.")]
    [EnumDataType(typeof(Branch), ErrorMessage = "Ngành học không hợp lệ.")]
    public Branch? Branch { get; set; }

    [Display(Name = "Giới tính")]
    [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
    [EnumDataType(typeof(Gender), ErrorMessage = "Giới tính không hợp lệ.")]
    public Gender? Gender { get; set; }

    [Display(Name = "Hệ chính quy")]
    public bool IsRegular { get; set; }

    [Display(Name = "Địa chỉ")]
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
    [StringLength(250, ErrorMessage = "Địa chỉ không quá 250 ký tự.")]
    public string? Address { get; set; }

    // Giữ tên DateOfBorth theo đề để thống nhất model và Razor.
    [Display(Name = "Ngày sinh")]
    [DataType(DataType.Date)]
    public DateTime DateOfBorth { get; set; }

    [Display(Name = "Ảnh đại diện")]
    public string? AvatarPath { get; set; }
}
