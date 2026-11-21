using Microsoft.AspNetCore.Mvc;
using BùiQuangMinh_241230793.Models;
namespace BùiQuangMinh_241230793.Controllers.ViewComponents;
public class LoaiHangMenuViewComponent : ViewComponent  
{
    private readonly QlhangHoaContext _db;
    public LoaiHangMenuViewComponent(QlhangHoaContext context)
    {
        _db = context;
    }
    public IViewComponentResult Invoke()
    {   
        var danhsach = _db.LoaiHangs.ToList();
        return View(danhsach);
    }
}
