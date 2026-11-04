using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XmLesson12_Lab6.Data;

namespace XmLesson12_Lab6.ViewComponents;

public class MajorViewComponent : ViewComponent
{
    private readonly SchoolContext db;

    public MajorViewComponent(SchoolContext context)
    {
        db = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? selectedMajorId = null, bool useAjax = true, string? keyword = null)
    {
        ViewBag.SelectedMajorId = selectedMajorId;
        ViewBag.UseAjax = useAjax;
        ViewBag.Keyword = keyword;
        var majors = await db.Majors.AsNoTracking().OrderBy(major => major.MajorID).ToListAsync();
        return View("RenderMajor", majors);
    }
}
