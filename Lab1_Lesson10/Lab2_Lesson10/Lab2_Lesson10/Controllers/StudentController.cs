using Lab2_Lesson10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab2_Lesson10.Controllers;

public class StudentController : Controller
{
    private const long MaxAvatarSize = 2 * 1024 * 1024;
    private readonly IWebHostEnvironment environment;
    private static readonly object SyncRoot = new();

    // Danh sách dùng chung giữa các request, chỉ khởi tạo một lần.
    // Chưa dùng database: khởi động lại app sẽ về 4 sinh viên mẫu.
    private static readonly List<Student> listStudents = new()
    {
        new Student { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
            Gender = Gender.Male, IsRegular = true, DateOfBorth = new DateTime(2003, 5, 12),
            Address = "A1-2018", Email = "nam@g.com" },
        new Student { Id = 102, Name = "Minh Tú", Branch = Branch.BE,
            Gender = Gender.Female, IsRegular = true, DateOfBorth = new DateTime(2003, 8, 20),
            Address = "A1-2019", Email = "tu@g.com" },
        new Student { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
            Gender = Gender.Male, IsRegular = false, DateOfBorth = new DateTime(2002, 11, 3),
            Address = "A1-2020", Email = "phong@g.com" },
        new Student { Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
            Gender = Gender.Female, IsRegular = false, DateOfBorth = new DateTime(2003, 2, 16),
            Address = "A1-2021", Email = "mai@g.com" }
    };

    public StudentController(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    [HttpGet]
    public IActionResult Index()
    {
        lock (SyncRoot)
        {
            return View(listStudents.ToList());
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadFormOptions();
        return View(new Student { IsRegular = true, Gender = Gender.Male });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Name,Email,Password,Branch,Gender,IsRegular,Address,DateOfBorth")] Student student,
        IFormFile? avatar)
    {
        if (student.DateOfBorth < new DateTime(1900, 1, 1) || student.DateOfBorth.Date > DateTime.Today)
        {
            ModelState.AddModelError(nameof(Student.DateOfBorth),
                "Ngày sinh phải từ 01/01/1900 đến hôm nay.");
        }

        var extension = avatar is null ? "" : Path.GetExtension(avatar.FileName).ToLowerInvariant();
        if (avatar is not null)
        {
            if (avatar.Length == 0 || avatar.Length > MaxAvatarSize)
            {
                ModelState.AddModelError("avatar", "Ảnh phải có dung lượng lớn hơn 0 và không quá 2 MB.");
            }
            else if (!await IsValidImageAsync(avatar, extension))
            {
                ModelState.AddModelError("avatar", "Chỉ chấp nhận ảnh JPG, JPEG, PNG, GIF hoặc WEBP hợp lệ.");
            }
        }

        if (!ModelState.IsValid)
        {
            LoadFormOptions();
            return View(student);
        }

        if (avatar is not null)
        {
            // Dùng tên ngẫu nhiên do server tạo, không dùng tên file người dùng gửi.
            var uploadDirectory = Path.Combine(environment.WebRootPath, "uploads", "students");
            var fileName = Guid.NewGuid().ToString("N") + extension;
            var filePath = Path.Combine(uploadDirectory, fileName);
            try
            {
                Directory.CreateDirectory(uploadDirectory);
                await using var stream = new FileStream(filePath, FileMode.CreateNew);
                await avatar.CopyToAsync(stream);
                student.AvatarPath = "/uploads/students/" + fileName;
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                ModelState.AddModelError("avatar", "Không thể lưu ảnh. Vui lòng chọn lại ảnh và thử lại.");
                LoadFormOptions();
                return View(student);
            }
        }

        student.Name = student.Name!.Trim();
        student.Email = student.Email!.Trim();
        student.Address = student.Address!.Trim();
        // Form minh họa nhập mật khẩu; danh sách hồ sơ không lưu mật khẩu.
        student.Password = null;

        lock (SyncRoot)
        {
            student.Id = listStudents.Count == 0 ? 101 : listStudents.Max(s => s.Id) + 1;
            listStudents.Add(student);
        }

        TempData["Success"] = "Đã thêm sinh viên " + student.Name + " (mã " + student.Id + ").";
        // Redirect sau POST để refresh trang không thêm trùng sinh viên.
        return RedirectToRoute("StudentList");
    }

    private void LoadFormOptions()
    {
        ViewBag.AllGenders = Enum.GetValues<Gender>();
        // Giá trị option khớp enum 0..3, không dùng nhầm 1..4 trong đề.
        ViewBag.AllBranches = Enum.GetValues<Branch>().Select(branch => new SelectListItem
        {
            Value = ((int)branch).ToString(),
            Text = branch switch
            {
                Branch.IT => "IT - CNTT",
                Branch.BE => "BE - Kinh tế",
                Branch.CE => "CE - Công trình",
                Branch.EE => "EE - Điện - Điện tử",
                _ => branch.ToString()
            }
        }).ToList();
    }

    private static async Task<bool> IsValidImageAsync(IFormFile file, string extension)
    {
        var header = new byte[12];
        await using var stream = file.OpenReadStream();
        var count = await stream.ReadAtLeastAsync(header, header.Length, throwOnEndOfStream: false);

        return extension switch
        {
            ".jpg" or ".jpeg" => count >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF,
            ".png" => count >= 8 && header.AsSpan(0, 8).SequenceEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }),
            ".gif" => count >= 6 && (header.AsSpan(0, 6).SequenceEqual("GIF87a"u8) || header.AsSpan(0, 6).SequenceEqual("GIF89a"u8)),
            ".webp" => count >= 12 && header.AsSpan(0, 4).SequenceEqual("RIFF"u8) && header.AsSpan(8, 4).SequenceEqual("WEBP"u8),
            _ => false
        };
    }
}
