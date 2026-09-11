using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewComponents
{
    public class BookViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // In real app this would be asynchronous data access
            var books = await Task.FromResult(BookRepository.GetBookList());
            return View(books);
        }
    }
}
