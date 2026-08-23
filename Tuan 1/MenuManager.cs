using System;
using System.Collections.Generic;

namespace StudentManagementOOP
{
    public class MenuManager
    {
        private StudentService _service = new StudentService();
        private StudentConsoleView _view = new StudentConsoleView();

        public void Run()
        {
            _service.AddStudent(new Student("SV01", "Nguyen Van A", new DateTime(2003, 1, 1), "Nam", "a@gmail.com", "0123", "IT", 8.5, "Dang hoc"));
            _service.AddStudent(new Student("SV02", "Tran Thi B", new DateTime(2003, 5, 5), "Nu", "b@gmail.com", "0124", "Kinh te", 9.2, "Dang hoc"));

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n===== QUẢN LÝ SINH VIÊN =====");
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Hiển thị danh sách");
                Console.WriteLine("3. Tìm sinh viên theo mã");
                Console.WriteLine("4. Tìm gần đúng theo họ tên");
                Console.WriteLine("5. Cập nhật sinh viên");
                Console.WriteLine("6. Xóa sinh viên");
                Console.WriteLine("7. Sắp xếp theo họ tên");
                Console.WriteLine("8. Sắp xếp theo điểm trung bình");
                Console.WriteLine("9. Hiển thị SV có điểm từ 8 trở lên");
                Console.WriteLine("10. Hiển thị SV có điểm cao nhất");
                Console.WriteLine("11. Tính điểm trung bình toàn bộ SV");
                Console.WriteLine("12. Thống kê sinh viên theo ngành");
                Console.WriteLine("13. Thống kê sinh viên theo trạng thái");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        var newStudent = _view.GetStudentInput(_service);
                        if (newStudent != null)
                        {
                            _service.AddStudent(newStudent);
                            _view.ShowMessage("Thêm thành công!");
                        }
                        break;
                    case "2":
                        _view.DisplayList(_service.GetAllStudents());
                        break;
                    case "3":
                        string searchId = _view.GetInputString("Nhập mã SV cần tìm: ");
                        var sv = _service.FindById(searchId);
                        if (sv != null) _view.DisplayList(new List<Student> { sv }, "Kết quả tìm kiếm");
                        else _view.ShowMessage("Không tìm thấy sinh viên!");
                        break;
                    case "4":
                        string searchName = _view.GetInputString("Nhập tên cần tìm: ");
                        _view.DisplayList(_service.FindByName(searchName), "Kết quả tìm kiếm theo tên");
                        break;
                    case "5":
                        string updateId = _view.GetInputString("Nhập mã SV cần cập nhật: ");
                        if (_service.FindById(updateId) == null)
                        {
                            _view.ShowMessage("Sinh viên không tồn tại!");
                        }
                        else
                        {
                            Console.WriteLine("Nhập thông tin mới:");
                            var updateData = _view.GetStudentInput(_service, true);
                            if (updateData != null && _service.UpdateStudent(updateId, updateData))
                                _view.ShowMessage("Cập nhật thành công!");
                        }
                        break;
                    case "6":
                        string deleteId = _view.GetInputString("Nhập mã SV cần xóa: ");
                        if (_service.DeleteStudent(deleteId)) _view.ShowMessage("Xóa thành công!");
                        else _view.ShowMessage("Sinh viên không tồn tại!");
                        break;
                    case "7":
                        _view.DisplayList(_service.SortByName(), "Danh sách sắp xếp theo tên");
                        break;
                    case "8":
                        _view.DisplayList(_service.SortByGpa(), "Danh sách sắp xếp theo điểm TB (Giảm dần)");
                        break;
                    case "9":
                        _view.DisplayList(_service.GetExcellentStudents(), "Sinh viên điểm >= 8");
                        break;
                    case "10":
                        _view.DisplayList(_service.GetTopGpaStudents(), "Sinh viên có điểm cao nhất");
                        break;
                    case "11":
                        _view.ShowMessage($"Điểm trung bình của toàn bộ SV: {_service.GetAverageGpaOfAll():F2}");
                        break;
                    case "12":
                        Console.WriteLine("\n--- Thống kê theo ngành ---");
                        foreach (var item in _service.GetStatsByMajor())
                            Console.WriteLine($"- Ngành {item.Key}: {item.Value} sinh viên");
                        break;
                    case "13":
                        Console.WriteLine("\n--- Thống kê theo trạng thái ---");
                        foreach (var item in _service.GetStatsByStatus())
                            Console.WriteLine($"- Trạng thái '{item.Key}': {item.Value} sinh viên");
                        break;
                    case "0":
                        exit = true;
                        _view.ShowMessage("Đã thoát chương trình.");
                        break;
                    default:
                        _view.ShowMessage("Chức năng không hợp lệ!");
                        break;
                }
            }
        }
    }
}