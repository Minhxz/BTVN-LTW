using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BùiQuangMinh_241230793.Models;

namespace BùiQuangMinh_241230793.Controllers;

public class HangHoaController : Controller
{
    private readonly QlhangHoaContext db;

    public HangHoaController(QlhangHoaContext context)
    {
        db = context;
    }

    public IActionResult Index()
    {
        ViewBag.TieuDe = "BùiQuangMinh-241230793";
        var danhSach = db.HangHoas.Where(h => h.Gia >= 100).ToList();
        return View(danhSach);
    }

    public IActionResult RenderByID(int id)
    {
        var loaiHang = db.LoaiHangs.Find(id);
        if (loaiHang == null)
        {
            return NotFound();
        }

        ViewBag.TieuDe = loaiHang.TenLoai;
        var danhSach = db.HangHoas
            .Where(h => h.MaLoai == id && h.Gia != null)
            .ToList();
        return PartialView("BùiQuangMinh-241230793_MainContent", danhSach);
    }

    public IActionResult Create()
    {
        ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("MaLoai,TenHang,Gia,Anh")] HangHoa hangHoa)
    {
        if (!db.LoaiHangs.Any(loai => loai.MaLoai == hangHoa.MaLoai))
        {
            ModelState.AddModelError("MaLoai", "Vui lòng chọn loại hàng hợp lệ.");
        }

        if (ModelState.IsValid)
        {
            db.HangHoas.Add(hangHoa);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.MaLoai = new SelectList(
            db.LoaiHangs, "MaLoai", "TenLoai", hangHoa.MaLoai);
        return View(hangHoa);
    }
}