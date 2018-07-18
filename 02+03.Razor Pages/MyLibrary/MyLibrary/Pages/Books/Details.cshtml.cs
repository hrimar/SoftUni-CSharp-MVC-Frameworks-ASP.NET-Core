using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;
using MyLibrary.Models.ViewModels;
using System.Linq;

namespace MyLibrary.Pages.Books
{
    public class DetailsModel : BaseModelController
    {
        public DetailsModel(LibraryDbContext context)
            : base(context)
        {
        }

        public DetailBookViewModel BookDetails { get; set; }

        public IActionResult OnGet(int id)
        {
            DetailBookViewModel book = null;
            using (this.Context)
            {
                book = this.Context.Books
                    .Include(b => b.Author)
                    .Where(b => b.Id == id)
                    .Select(b => new DetailBookViewModel
                    {
                        Id = b.Id,
                        ImageUrl = b.CoverImage,
                        Title = b.Title,
                        Author = b.Author.Name,
                        Description = b.Description,
                        Status = b.Status
                    })
                    .FirstOrDefault();
            }

            if (book == null)
            {
                return this.NotFound();
            }

            this.BookDetails = book;

            return this.Page();
        }
    }
}