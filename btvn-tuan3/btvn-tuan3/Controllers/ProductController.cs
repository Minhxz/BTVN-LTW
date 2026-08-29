using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using btvn_tuan3.Models;

namespace MyAppMVC.Controllers
{
    public class ProductController : Controller
    {
        [Route("san-pham")]
        public IActionResult Index()
        {
            var data = GetSampleData();
            ViewBag.Categories = data.categories;
            ViewBag.Products = data.products;
            return View();
        }

        [Route("san-pham/chi-tiet", Name = "product-details")]
        public IActionResult Details(int id)
        {
            var data = GetSampleData();
            var product = data.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            ViewBag.Product = product;
            ViewBag.Category = data.categories.FirstOrDefault(c => c.Id == product.CategoryId);
            return View();
        }

        private (List<Category> categories, List<Product> products) GetSampleData()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Quần Áo" },
                new Category { Id = 2, Name = "Túi xách" },
                new Category { Id = 3, Name = "Đồng hồ" }
            };

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Bộ đồ bơi cho trẻ em nam", Image = Url.Content("~/Avatar/01.jfif"), Price = 60000, SalePrice = 35000, CategoryId = 1, Description = "Mô tả: Bộ đồ bơi cho trẻ em nam.", Status = true, CreatedAt = new DateTime(2021,7,15) },
                new Product { Id = 2, Name = "Bộ đồ bơi cho trẻ em nữ", Image = Url.Content("~/Avatar/02.jfif"), Price = 60000, SalePrice = 35000, CategoryId = 1, Description = "Mô tả: Bộ đồ bơi cho trẻ em nữ.", Status = true, CreatedAt = new DateTime(2021,7,15) },
                new Product { Id = 3, Name = "Bộ đồ bơi cho trẻ em từ 3-5 tuổi", Image = Url.Content("~/Avatar/03.jfif"), Price = 60000, SalePrice = 35000, CategoryId = 1, Description = "Mô tả: Bộ đồ bơi cho trẻ em 3-5 tuổi.", Status = true, CreatedAt = new DateTime(2021,7,15) },
                new Product { Id = 4, Name = "Bộ đồ bơi thời trang", Image = Url.Content("~/Avatar/04.jfif"), Price = 60000, SalePrice = 35000, CategoryId = 1, Description = "Mô tả: Bộ đồ bơi thời trang.", Status = true, CreatedAt = new DateTime(2021,7,15) },
                new Product { Id = 5, Name = "Túi thời trang mẫu mới 2021", Image = Url.Content("~/Avatar/05.jfif"), Price = 60000, SalePrice = 35000, CategoryId = 2, Description = "Mô tả: Túi thời trang.", Status = true, CreatedAt = new DateTime(2021,7,15) },
                new Product { Id = 6, Name = "Túi thời trang da cá sấu", Image = Url.Content("~/Avatar/06.jfif"), Price = 60000, SalePrice = 35000, CategoryId = 2, Description = "Mô tả: Túi da cá sấu.", Status = true, CreatedAt = new DateTime(2021,7,15) }
            };

            return (categories, products);
        }
    }
}
