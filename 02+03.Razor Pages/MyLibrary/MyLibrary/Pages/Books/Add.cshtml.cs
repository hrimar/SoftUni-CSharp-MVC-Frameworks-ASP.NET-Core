using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLibrary.Data;
using MyLibrary.Models;

namespace MyLibrary.Pages.Books
{
    public class AddModel : BaseModelController
    {
        public AddModel(LibraryDbContext context)
            : base(context)
        {
        }

        [Required]
        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [Required]
        [BindProperty]
        public string Author { get; set; }

        [BindProperty]
        [Display(Name = "Image Url")] // Instead of to writen in label in .cshtml file!
        [DataType(DataType.ImageUrl)] // !!!
        public string ImageUrl { get; set; }

        public IActionResult OnPostCreateBook()
        {
            if (!ModelState.IsValid)
            {
                // for MVC: return RedirectToRoute("default", new { controller = "Cats", action = "Details", id = cat.Id }); // gives Id param to Details(int id)
                // return RedirectToPage("Details", new { id = book.Id }); // -OK
                return this.Page();
            }

            Book book = AddBook();

            return RedirectToPage("/Books/Details", new { id = book.Id }); // -OK      
        }


        private Book AddBook()
        {
            Book book = null;
            using (this.Context)
            {
                Author author = CreateOrUpdateAuthor();

                book = new Book()
                {
                    Title = this.Title,
                    Description = this.Description,
                    AuthorId = author.Id,
                    CoverImage = this.ImageUrl,
                    Status = "At home"
                };

                this.Context.Books.Add(book);
                this.Context.SaveChanges();
            }

            return book;
        }

        private Author CreateOrUpdateAuthor()
        {
            Author author = this.Context.Authors.FirstOrDefault(a => a.Name == this.Author);
            if (author == null)
            {
                author = new Author()
                {
                    Name = this.Author
                };

                this.Context.Authors.Add(author);
                this.Context.SaveChanges(); // in order to say later: AuthorId = author.Id,
            }

            return author;
        }








        //public void OnPostGreeting(string name)
        //{            
        //    this.ViewData["Message"] = $"Hello {name}";

        //    // and URL -> https://localhost:44394/Books/Add?name=Hristo&handler=Greeting            
        //}
    }
}