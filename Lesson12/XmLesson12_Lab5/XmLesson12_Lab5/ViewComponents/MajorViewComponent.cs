using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XmLesson12_Lab5.Data;

namespace XmLesson12_Lab5.ViewComponents;

public class MajorViewComponent : ViewComponent
{
    private readonly SchoolContext db;

    public MajorViewComponent(SchoolContext context)
    {
        db = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? selectedMajorId = null, bool useAjax = true)
    {
        ViewBag.SelectedMajorId = selectedMajorId;
        ViewBag.UseAjax = useAjax;
        var majors = await db.Majors.AsNoTracking().OrderBy(major => major.MajorID).ToListAsync();
        return View("RenderMajor", majors);
    }
}
