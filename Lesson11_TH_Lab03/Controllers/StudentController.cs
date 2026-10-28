using Lab3Validation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab3Validation.Controllers
{
    public class StudentController : Controller
    {
        // Danh sách sinh viên lưu trong bộ nhớ (static để dùng chung giữa các request)
        private static readonly List<Student> listStudents = new()
        {
            new Student { Id = 101, Name = "Nguyễn Văn An", Email = "an.nguyen@gmail.com", Password = "Abcd@1234",
                Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "Hà Nội",
                DateOfBorth = new DateTime(2003, 5, 12), Score = 8.5 },
            new Student { Id = 102, Name = "Trần Thị Bình", Email = "binh.tran@gmail.com", Password = "Xyz@98765",
                Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "Hải Phòng",
                DateOfBorth = new DateTime(2002, 11, 3), Score = 9.0 },
            new Student { Id = 103, Name = "Lê Hoàng Cường", Email = "cuong.le@gmail.com", Password = "Qwer#5678",
                Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "Đà Nẵng",
                DateOfBorth = new DateTime(2001, 1, 25), Score = 7.25 }
        };

        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                s.Id = listStudents.Count == 0 ? 1 : listStudents.Max(x => x.Id) + 1;
                listStudents.Add(s);
                return RedirectToAction(nameof(Index));
            }
            // Dữ liệu sai -> hiển thị lại form cùng thông báo lỗi
            LoadLists();
            return View(s);
        }

        private void LoadLists()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        }
    }
}
