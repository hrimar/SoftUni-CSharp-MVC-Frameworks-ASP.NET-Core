using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models.ViewModels;

namespace MyLibrary.Pages.Authors
{
    public class DetailsModel : BaseModelController
    {
        public DetailsModel(LibraryDbContext context)
            : base(context)
        {
        }

        public DetailAuthorViewModel AuthorDetails { get; set; }
        // or not to use DetailAuthorViewModel, just prop: Title, Description, ImageUrl, ...

        public IActionResult OnGet(int id)
        {
            DetailAuthorViewModel authorDetails = null;
            using (this.Context)
            {
                authorDetails = this.Context.Authors
                    .Include(a => a.Books)
                    .Where(a => a.Id == id)
                    .Select(a => new DetailAuthorViewModel
                    {
                        Name=a.Name,
                        Id = a.Id,
                        Books = a.Books
                    }).FirstOrDefault();
            }

            if (authorDetails == null)
            {
                return NotFound();
            }

            this.AuthorDetails = authorDetails;
            return this.Page();
        }
    }
}