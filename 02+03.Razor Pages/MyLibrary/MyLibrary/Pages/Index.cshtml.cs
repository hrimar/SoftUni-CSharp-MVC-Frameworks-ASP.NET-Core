using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models.ViewModels;

namespace MyLibrary.Pages
{
    public class IndexModel : BaseModelController
    {       
        public IndexModel(LibraryDbContext context)
            : base(context)
        {           
        }

        public List<AllBooksViewModel> AllBooks { get; set; }

        public void OnGet()
        {
            using (this.Context)
            {
                this.AllBooks = this.Context.Books
                    //.Include(b=>b.Author)
                    .Select(b => new AllBooksViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author.Name,
                        AuthorId = b.AuthorId,
                        Status = b.Status
                    })
                    .OrderBy(b => b.Title)
                    .ToList();

                ////or
                //this.AllBooks = this.Context.Books
                //    .Include(b => b.Author)
                //    .Select(AllBooksViewModel.FromBook)
                //    .OrderBy(b => b.Title)
                //    .ToList();
            }
        }
    }
}
