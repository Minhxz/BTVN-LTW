using Microsoft.AspNetCore.Mvc;
using BùiQuangMinh_241230793.Models;

namespace BùiQuangMinh_241230793.ViewComponents;

public class LoaiTaiSanMenuViewComponent : ViewComponent
{
    private readonly QLTaiSanContext db;

    public LoaiTaiSanMenuViewComponent(QLTaiSanContext context)
    {
        db = context;
    }

    public IViewComponentResult Invoke()
    {
        var danhSach = db.LoaiTaiSan.ToList();
        return View(danhSach);
    }
}