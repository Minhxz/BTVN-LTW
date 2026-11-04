using Microsoft.EntityFrameworkCore;
using XmLesson12_Lab6.Models;

namespace XmLesson12_Lab6.Data;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new SchoolContext(
            serviceProvider.GetRequiredService<DbContextOptions<SchoolContext>>());
        context.Database.EnsureCreated();
        if (context.Majors.Any())
        {
            return;
        }

        // Seed atomically, and use generated identity values for the relationships.
        using var transaction = context.Database.BeginTransaction();
        var majors = new[]
        {
            new Major { MajorName = "IT" },
            new Major { MajorName = "Economics" },
            new Major { MajorName = "Mathematics" }
        };
        context.Majors.AddRange(majors);
        context.SaveChanges();

        var learners = new[]
        {
            new Learner
            {
                FirstMidName = "Carson", LastName = "Alexander",
                EnrollmentDate = new DateTime(2005, 9, 1), MajorID = majors[0].MajorID
            },
            new Learner
            {
                FirstMidName = "Meredith", LastName = "Alonso",
                EnrollmentDate = new DateTime(2002, 9, 1), MajorID = majors[1].MajorID
            }
        };
        context.Learners.AddRange(learners);
        // Dữ liệu riêng của Lab6: đủ 15 người học để thử 5 trang và lọc từng ngành.
        var extraLearners = new (string FirstName, string LastName, int MajorIndex)[]
        {
            ("Nam", "Lê", 2), ("Minh", "Trần", 0), ("Lan", "Nguyễn", 1),
            ("Hoa", "Phạm", 2), ("Bình", "Vũ", 0), ("An", "Đỗ", 1),
            ("Tuấn", "Bùi", 2), ("Ngọc", "Hoàng", 0), ("Mai", "Đặng", 1),
            ("Linh", "Ngô", 2), ("Hùng", "Đinh", 0), ("Hạnh", "Dương", 1),
            ("Bích", "Võ", 2)
        };
        context.Learners.AddRange(extraLearners.Select((item, index) => new Learner
        {
            FirstMidName = item.FirstName, LastName = item.LastName,
            MajorID = majors[item.MajorIndex].MajorID, EnrollmentDate = new DateTime(2023, 9, 1).AddDays(index)
        }));
        context.SaveChanges();

        var courses = new[]
        {
            new Course { CourseID = 1050, Title = "Chemistry", Credits = 3 },
            new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3 },
            new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3 }
        };
        context.Courses.AddRange(courses);
        context.SaveChanges();

        context.Enrollments.AddRange(
            new Enrollment { LearnerID = learners[0].LearnerID, CourseID = courses[0].CourseID, Grade = 5.5f },
            new Enrollment { LearnerID = learners[0].LearnerID, CourseID = courses[1].CourseID, Grade = 7.5f },
            new Enrollment { LearnerID = learners[1].LearnerID, CourseID = courses[0].CourseID, Grade = 3.5f },
            new Enrollment { LearnerID = learners[1].LearnerID, CourseID = courses[2].CourseID, Grade = 7f });
        context.SaveChanges();
        transaction.Commit();
    }
}
