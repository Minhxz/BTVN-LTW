using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StudentManagementOOP
{
    public static class StudentValidator
    {
        public static bool IsIdUnique(List<Student> students, string id)
        {
            return !students.Any(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsNameValid(string? name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        public static bool IsGpaValid(double gpa)
        {
            return gpa >= 0 && gpa <= 10;
        }

        public static bool IsEmailValid(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}