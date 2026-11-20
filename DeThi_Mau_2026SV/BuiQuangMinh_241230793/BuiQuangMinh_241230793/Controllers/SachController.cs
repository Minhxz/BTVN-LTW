using Microsoft.AspNetCore.Mvc;
using BuiQuangMinh_241230793.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuiQuangMinh_241230793.Controllers
{
    public class SachController : Controller
    {
        private readonly QLNhaSachContext db;
        public SachController(QLNhaSachContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            ViewBag.TieuDe = "Bùi Quang Minh_241230793";
            var danhSach = db.Sach
                .Where(s => s.Gia >=100)
                .ToList();
            return View(danhSach);
        }
        public IActionResult RenderByID(int id)
        {
            var theloai = db.TheLoai.Find(id);
            if( theloai == null)
            {
                return NotFound();  
            }
            ViewBag.TieuDe = theloai.TenTL;
            var danhsach= db.Sach
            .Where ( s => s.MaTL == id && s.Gia !=null)
            .ToList();
            
            return PartialView("BuiQuangMinh_241230793_MainContent", danhsach);
        }
        public IActionResult Create()
        {
            ViewBag.MaTL = new SelectList(db.TheLoai, "MaTL", "TenTL");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Sach.Add(sach);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MaTL = new SelectList(
                db.TheLoai, "MaTL", "TenTL", sach.MaTL);

            return View(sach);
        }
    }
}
