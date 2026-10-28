namespace MyWebApp.Models
{
    public class Learner
    {
        public Learner()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int LearnerID { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstMidName { get; set; } = null!;
        public DateTime EnrollmentDate { get; set; }
        public int MajorID { get; set; }
        public virtual Major? Major { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
