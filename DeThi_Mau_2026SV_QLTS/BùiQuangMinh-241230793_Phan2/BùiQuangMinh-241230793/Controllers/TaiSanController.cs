using Microsoft.AspNetCore.Mvc;
using BùiQuangMinh_241230793.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BùiQuangMinh_241230793.Controllers
{
    public class TaiSanController : Controller
    {
        
        private readonly QLTaiSanContext db;
        public TaiSanController(QLTaiSanContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.TieuDe= "BùiQuangMinh-241230793 ";
            var danhsach = db.TaiSan.Where (t=> t.NguyenGia>=20).ToList();
            return View(danhsach);
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
        public IActionResult Edit(int id)
        {
            var taiSan = db.TaiSan.Find(id);

            if (taiSan == null)
            {
                return NotFound();
            }

            ViewBag.MaLoai = new SelectList(
                db.LoaiTaiSan, "MaLoai", "TenLoai", taiSan.MaLoai);

            return View(taiSan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("MaTS,MaLoai,TenTS,NguyenGia,Anh")] TaiSan taiSan)
        {
            if (id != taiSan.MaTS)
            {
                return BadRequest();
            }

            var taiSanCu = db.TaiSan.Find(id);

            if (taiSanCu == null)
            {
                return NotFound();
            }

            if (!db.LoaiTaiSan.Any(loai => loai.MaLoai == taiSan.MaLoai))
            {
                ModelState.AddModelError("MaLoai", "Loại tài sản không hợp lệ.");
            }

            if (ModelState.IsValid)
            {
                taiSanCu.MaLoai = taiSan.MaLoai;
                taiSanCu.TenTS = taiSan.TenTS;
                taiSanCu.NguyenGia = taiSan.NguyenGia;
                taiSanCu.Anh = taiSan.Anh;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(
                db.LoaiTaiSan, "MaLoai", "TenLoai", taiSan.MaLoai);

            return View(taiSan);
        }
    }
}
