using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XmLesson12_Lab6.Data;
using XmLesson12_Lab6.Models;

namespace XmLesson12_Lab6.Controllers;

public class LearnerController : Controller
{
    private readonly SchoolContext db;
    private const int PageSize = 3;

    public LearnerController(SchoolContext context)
    {
        db = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int? mid, string? keyword, int? pageIndex, bool useAjax = true)
    {
        return View(await BuildLearnerPage(mid, keyword, pageIndex, useAjax));
    }

    [HttpGet]
    public async Task<IActionResult> LearnerFilter(int? mid, string? keyword, int? pageIndex, bool useAjax = true)
    {
        return PartialView("LearnerTable", await BuildLearnerPage(mid, keyword, pageIndex, useAjax));
    }

    private async Task<LearnerListViewModel> BuildLearnerPage(int? mid, string? keyword, int? pageIndex, bool useAjax)
    {
        var learners = db.Learners.AsNoTracking().Include(learner => learner.Major).AsQueryable();
        if (mid.HasValue)
        {
            learners = learners.Where(learner => learner.MajorID == mid.Value);
        }

        keyword = string.IsNullOrWhiteSpace(keyword) ? null : keyword.Trim();
        if (keyword is not null)
        {
            var searchTerm = keyword.ToLower();
            learners = learners.Where(learner => learner.FirstMidName.ToLower().Contains(searchTerm));
        }

        // Lọc và tìm kiếm trước khi đếm, rồi sắp xếp ổn định trước Skip/Take.
        var totalCount = await learners.CountAsync();
        var pageCount = (int)Math.Ceiling(totalCount / (double)PageSize);
        var page = Math.Clamp(pageIndex ?? 1, 1, Math.Max(1, pageCount));
        var result = await learners.OrderBy(learner => learner.LearnerID)
            .Skip(PageSize * (page - 1)).Take(PageSize).ToListAsync();
        return new LearnerListViewModel
        {
            Learners = result, MajorId = mid, Keyword = keyword, UseAjax = useAjax,
            PageIndex = page, PageSize = PageSize, TotalCount = totalCount
        };
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
