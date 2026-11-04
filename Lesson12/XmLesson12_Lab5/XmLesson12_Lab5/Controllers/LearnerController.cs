using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XmLesson12_Lab5.Data;
using XmLesson12_Lab5.Models;

namespace XmLesson12_Lab5.Controllers;

public class LearnerController : Controller
{
    private readonly SchoolContext db;

    public LearnerController(SchoolContext context)
    {
        db = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int? mid, bool useAjax = true)
    {
        ViewBag.SelectedMajorId = mid;
        ViewBag.UseAjax = useAjax;
        return View(await LearnersByMajor(mid).ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> LearnerByMajorID(int? mid)
    {
        return PartialView("LearnerTable", await LearnersByMajor(mid).ToListAsync());
    }

    private IQueryable<Learner> LearnersByMajor(int? mid)
    {
        var learners = db.Learners.AsNoTracking().Include(learner => learner.Major).AsQueryable();
        if (mid.HasValue)
        {
            learners = learners.Where(learner => learner.MajorID == mid.Value);
        }

        return learners.OrderBy(learner => learner.LearnerID);
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadMajors();
        return View(new Learner { EnrollmentDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(
        [Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
    {
        ValidateMajor(learner.MajorID);
        if (ModelState.IsValid)
        {
            learner.FirstMidName = learner.FirstMidName.Trim();
            learner.LastName = learner.LastName.Trim();
            db.Learners.Add(learner);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        LoadMajors(learner.MajorID);
        return View(learner);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var learner = db.Learners.Find(id);
        if (learner is null)
        {
            return NotFound();
        }

        LoadMajors(learner.MajorID);
        return View(learner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id,
        [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
    {
        if (id != learner.LearnerID)
        {
            return NotFound();
        }

        var existingLearner = db.Learners.Find(id);
        if (existingLearner is null)
        {
            return NotFound();
        }

        ValidateMajor(learner.MajorID);
        if (ModelState.IsValid)
        {
            existingLearner.FirstMidName = learner.FirstMidName.Trim();
            existingLearner.LastName = learner.LastName.Trim();
            existingLearner.MajorID = learner.MajorID;
            existingLearner.EnrollmentDate = learner.EnrollmentDate;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.Learners.Any(item => item.LearnerID == id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        LoadMajors(learner.MajorID);
        return View(learner);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var learner = FindLearnerForDeletion(id);
        if (learner is null)
        {
            return NotFound();
        }

        if (learner.Enrollments.Count > 0)
        {
            ModelState.AddModelError(string.Empty,
                "Không thể xóa người học đã có đăng ký học phần.");
        }

        return View(learner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var learner = FindLearnerForDeletion(id);
        if (learner is null)
        {
            return NotFound();
        }

        // Check again on POST so a direct request cannot bypass the deletion rule.
        if (learner.Enrollments.Count > 0)
        {
            ModelState.AddModelError(string.Empty,
                "Không thể xóa người học đã có đăng ký học phần.");
            return View("Delete", learner);
        }

        db.Learners.Remove(learner);
        db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private Learner? FindLearnerForDeletion(int id)
    {
        return db.Learners.Include(learner => learner.Major)
            .Include(learner => learner.Enrollments)
            .FirstOrDefault(learner => learner.LearnerID == id);
    }

    private void ValidateMajor(int majorId)
    {
        if (!db.Majors.Any(major => major.MajorID == majorId))
        {
            ModelState.AddModelError(nameof(Learner.MajorID), "Vui lòng chọn chuyên ngành có trong danh sách.");
        }
    }

    private void LoadMajors(int? selectedMajorId = null)
    {
        ViewBag.MajorID = new SelectList(db.Majors.AsNoTracking().OrderBy(major => major.MajorID),
            nameof(Major.MajorID), nameof(Major.MajorName), selectedMajorId);
    }
}
