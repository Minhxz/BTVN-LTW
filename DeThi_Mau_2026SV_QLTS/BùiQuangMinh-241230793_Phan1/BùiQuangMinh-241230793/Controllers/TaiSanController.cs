using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using BùiQuangMinh_241230793.Models;

namespace BùiQuangMinh_241230793.Controllers;

public class TaiSanController : Controller
{
    private readonly QLTaiSanContext db;

    public TaiSanController(QLTaiSanContext context)
    {
        db = context;
    }

    public IActionResult Index()
    {
        ViewBag.TieuDe = "BùiQuangMinh-241230793";
        var danhSach = db.TaiSan
            .Where(t => t.NguyenGia >= 20)
            .ToList();
        return View(danhSach);
    }

    public IActionResult RenderByID(int id)
    {
        var loaiTaiSan = db.LoaiTaiSan.Find(id);
        if (loaiTaiSan == null)
        {
            return NotFound();
        }

        ViewBag.TieuDe = loaiTaiSan.TenLoai;
        var danhSach = db.TaiSan
            .Where(t => t.MaLoai == id && t.NguyenGia != null)
            .ToList();
        return PartialView("BùiQuangMinh-241230793_MainContent", danhSach);
    }
	public IActionResult Create()
	{
		ViewBag.MaLoai = new SelectList(
			db.LoaiTaiSan, "MaLoai", "TenLoai");

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(TaiSan taiSan)
	{
		if (ModelState.IsValid)
		{
			db.TaiSan.Add(taiSan);
			db.SaveChanges();

			return RedirectToAction("Index");
		}

		ViewBag.MaLoai = new SelectList(
			db.LoaiTaiSan, "MaLoai", "TenLoai", taiSan.MaLoai);

		return View(taiSan);
	}
}