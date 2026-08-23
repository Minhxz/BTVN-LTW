using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementOOP
{
    public class StudentService
    {
        private List<Student> students;

        public StudentService()
        {
            students = new List<Student>();
        }

        public List<Student> GetAllStudents() => students;

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public Student? FindById(string id)
        {
            return students.FirstOrDefault(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public List<Student> FindByName(string keyword)
        {
            return students.Where(s => s.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool UpdateStudent(string id, Student updatedData)
        {
            var student = FindById(id);
            if (student == null) return false;

            student.Name = updatedData.Name;
            student.DateOfBirth = updatedData.DateOfBirth;
            student.Gender = updatedData.Gender;
            student.Email = updatedData.Email;
            student.Phone = updatedData.Phone;
            student.Major = updatedData.Major;
            student.Gpa = updatedData.Gpa;
            student.Status = updatedData.Status;
            return true;
        }

        public bool DeleteStudent(string id)
        {
            var student = FindById(id);
            if (student == null) return false;

            students.Remove(student);
            return true;
        }

        public List<Student> SortByName()
        {
            return students.OrderBy(s => s.Name).ToList();
        }

        public List<Student> SortByGpa()
        {
            return students.OrderByDescending(s => s.Gpa).ToList();
        }

        public List<Student> GetExcellentStudents()
        {
            return students.Where(s => s.Gpa >= 8).ToList();
        }

        public List<Student> GetTopGpaStudents()
        {
            if (!students.Any()) return new List<Student>();
            double maxGpa = students.Max(s => s.Gpa);
            return students.Where(s => s.Gpa == maxGpa).ToList();
        }

        public double GetAverageGpaOfAll()
        {
            if (!students.Any()) return 0;
            return students.Average(s => s.Gpa);
        }

        public Dictionary<string, int> GetStatsByMajor()
        {
            return students.GroupBy(s => s.Major)
                           .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> GetStatsByStatus()
        {
            return students.GroupBy(s => s.Status)
                           .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}