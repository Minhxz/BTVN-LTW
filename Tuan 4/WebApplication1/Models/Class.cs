using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int TotalPage { get; set; }
        public string Summary { get; set; }
    }

    public static class BookRepository
    {
        public static List<Book> GetBookList()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Chi Pheo",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b1.svg",
                    Price = 100000,
                    Summary = "",
                    TotalPage = 250
                },
                new Book
                {
                    Id = 2,
                    Title = "Lão Hạc",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b2.svg",
                    Price = 700000,
                    Summary = "Nội dung giới thiệu sách...",
                    TotalPage = 400
                },
                new Book
                {
                    Id = 3,
                    Title = "Sống Mới",
                    AuthorId = 3,
                    GenreId = 2,
                    Image = "/images/products/b3.svg",
                    Price = 90000,
                    Summary = "",
                    TotalPage = 200
                },
                new Book
                {
                    Id = 4,
                    Title = "Truyện ngắn",
                    AuthorId = 4,
                    GenreId = 3,
                    Image = "/images/products/b4.svg",
                    Price = 110000,
                    Summary = "",
                    TotalPage = 180
                }
            };
        }

        public static Book GetBookById(int id)
        {
            return GetBookList().FirstOrDefault(b => b.Id == id);
        }

        public static List<SelectListItem> Authors { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Nam Cao" },
            new SelectListItem { Value = "2", Text = "Tô Hoài" },
            new SelectListItem { Value = "3", Text = "Nguyễn Nhật Ánh" },
            new SelectListItem { Value = "4", Text = "Nguyễn Ngọc Tư" }
        };

        public static List<SelectListItem> Genres { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Văn học" },
            new SelectListItem { Value = "2", Text = "Khoa học" },
            new SelectListItem { Value = "3", Text = "Lịch sử" },
            new SelectListItem { Value = "4", Text = "Tâm lý học" }
        };
    }
}
