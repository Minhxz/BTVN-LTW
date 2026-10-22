using Lab2_Lesson10.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2_Lesson10.ViewComponents;

public class RenderViewComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync()
    {
        var menuItems = new List<MenuItem>
        {
            new() { Id = 1, Name = "Ngành học", Link = "#" },
            new() { Id = 2, Name = "Sinh viên", Link = "~/Admin/Student/List" },
            new() { Id = 3, Name = "Môn học", Link = "#" },
            new() { Id = 4, Name = "Khóa học", Link = "#" }
        };

        return Task.FromResult<IViewComponentResult>(
            View("RenderLeftMenu", menuItems));
    }
}
