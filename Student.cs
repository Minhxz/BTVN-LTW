using System;

namespace StudentManagementOOP
{
    public class Student
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
        public double Gpa { get; set; }
        public string Status { get; set; } = string.Empty;

        public Student() { }

        public Student(string id, string name, DateTime dateOfBirth, string gender, string email, string phone, string major, double gpa, string status)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            Phone = phone;
            Major = major;
            Gpa = gpa;
            Status = status;
        }

        public override string ToString()
        {
            return $"Mã SV: {Id,-10} | Họ tên: {Name,-20} | Ngày sinh: {DateOfBirth:dd/MM/yyyy} | Giới tính: {Gender,-5} | Điểm TB: {Gpa,-4} | Ngành: {Major,-15} | Trạng thái: {Status}";
        }
    }
}