namespace MyLibrary.Pages.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyLibrary.Data;
    using MyLibrary.Models;

    public class ReturnModel : BaseModelController
    {
        public ReturnModel(LibraryDbContext context)
            : base(context)
        {
            this.Borrowers = new List<SelectListItem>();
            this.EndDate = DateTime.Now;
        }

        [BindProperty]
        [Required(ErrorMessage = "You have to specify a borrower.")]
        [Display(Name = "Borrower")]
        public int BorrowerId { get; set; }
                

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public IEnumerable<SelectListItem> Borrowers { get; set; }

        public void OnGet()
        {
            this.Borrowers = this.Context.Borrowers
                .Select(b => new SelectListItem()
                {
                    Text = b.Name,
                    Value = b.Id.ToString()
                })
                .ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            var borrower = this.Context.Borrowers.Find(this.BorrowerId);
            int bookId = Convert.ToInt32(this.RouteData.Values["id"]);
            var book = this.Context.Books.Find(bookId);
            if (borrower == null || book == null)
            {
                return Page();
            }

            book.Status = "At home";

            var borrowedBook = new BorrowerBook()
            {
                BookId = 0,
                BorrowerId = 0,
                EndDate = this.EndDate
            };

            this.Context.BorrowerBooks.Add(borrowedBook);
            this.Context.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}