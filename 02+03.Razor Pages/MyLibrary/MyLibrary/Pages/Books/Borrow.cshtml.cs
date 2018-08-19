namespace MyLibrary.Pages.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq; 
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyLibrary.Data;
    using MyLibrary.Models;

    public class BorrowModel : BaseModelController
    {
        public BorrowModel(LibraryDbContext context) 
            : base(context)
        {
            this.Borrowers = new List<SelectListItem>();
            this.StartDate = DateTime.Now;
        }

        [BindProperty]
        [Required(ErrorMessage = "You have to specify a borrower.")]
        [Display(Name = "Borrower")]
        public int BorrowerId { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

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
                ModelState.AddModelError("__MODEL__", "You have a mistake!");
                return Page();              
            }

            book.Status = "Borrowed";

            var borrowedBook = new BorrowerBook()
            {
                BookId = book.Id,
                BorrowerId = borrower.Id,
                StartDate = this.StartDate,
                EndDate = this.EndDate
            };

            this.Context.BorrowerBooks.Add(borrowedBook);
            this.Context.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}