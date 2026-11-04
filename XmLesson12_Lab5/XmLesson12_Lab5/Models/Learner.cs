using System.ComponentModel.DataAnnotations;

namespace XmLesson12_Lab5.Models
{
    public class Learner
    {
        public Learner()
        {
            Enrollments = new HashSet<Enrollment>();
        }
        public int LearnerID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ.")]
        [Display(Name = "Họ")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Vui lòng nhập tên và tên đệm.")]
        [Display(Name = "Tên và tên đệm")]
        public string FirstMidName { get; set; } = string.Empty;
        [Display(Name = "Ngày nhập học")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        [Display(Name = "Chuyên ngành")]
        public int MajorID { get; set; }
        public virtual Major? Major { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
