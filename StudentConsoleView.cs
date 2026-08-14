using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementOOP
{
    public class StudentConsoleView
    {
        public Student? GetStudentInput(StudentService service, bool isUpdate = false)
        {
            string id = "";
            if (!isUpdate)
            {
                while (true)
                {
                    Console.Write("Nhập mã sinh viên: ");
                    id = Console.ReadLine()?.Trim() ?? "";
                    if (string.IsNullOrEmpty(id)) continue;

                    if (!StudentValidator.IsIdUnique(service.GetAllStudents(), id))
                    {
                        Console.WriteLine("Lỗi: Mã sinh viên đã tồn tại!");
                        continue;
                    }
                    break;
                }
            }

            string name = "";
            while (true)
            {
                Console.Write("Nhập họ tên: ");
                name = Console.ReadLine()?.Trim() ?? "";
                if (!StudentValidator.IsNameValid(name))
                {
                    Console.WriteLine("Lỗi: Họ tên không được để trống!");
                    continue;
                }
                break;
            }

            DateTime dob;
            while (true)
            {
                Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dob))
                {
                    Console.WriteLine("Lỗi: Sai định dạng ngày!");
                    continue;
                }
                break;
            }

            Console.Write("Nhập giới tính: ");
            string gender = Console.ReadLine()?.Trim() ?? "";

            string email = "";
            while (true)
            {
                Console.Write("Nhập Email: ");
                email = Console.ReadLine()?.Trim() ?? "";
                if (!StudentValidator.IsEmailValid(email))
                {
                    Console.WriteLine("Lỗi: Email không đúng định dạng!");
                    continue;
                }
                break;
            }

            Console.Write("Nhập số điện thoại: ");
            string phone = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Nhập ngành học: ");
            string major = Console.ReadLine()?.Trim() ?? "";

            double gpa;
            while (true)
            {
                Console.Write("Nhập điểm trung bình (0-10): ");
                if (!double.TryParse(Console.ReadLine(), out gpa) || !StudentValidator.IsGpaValid(gpa))
                {
                    Console.WriteLine("Lỗi: Điểm phải là số và nằm trong khoảng 0 đến 10!");
                    continue;
                }
                break;
            }

            Console.Write("Nhập trạng thái học tập: ");
            string status = Console.ReadLine()?.Trim() ?? "";

            return new Student(id, name, dob, gender, email, phone, major, gpa, status);
        }

        public void DisplayList(List<Student> students, string message = "Danh sách sinh viên:")
        {
            Console.WriteLine($"\n--- {message} ---");
            if (!students.Any())
            {
                Console.WriteLine("Danh sách trống!");
                return;
            }
            foreach (var s in students)
            {
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine("-----------------------------");
        }

        public string GetInputString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? "";
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine($"=> {message}");
        }
    }
}