﻿namespace MyLibrary.Pages.Books
{
    using System.Collections.Generic;
    using System.Linq;
    using MyLibrary.Data;
    using MyLibrary.Models.ViewModels;

    public class StatusModel : BaseModelController
    {
        public StatusModel(LibraryDbContext context)
            : base(context)
        {           
        }

        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public List<BorwedBookViewModel> BorowedBooks { get; set; }
                
        public void OnGet(int id)
        {
            this.BookTitle = this.Context.Books.Find(id).Title;

            this.BorowedBooks = this.Context.BorrowerBooks
                .Where(b => b.BookId == id)
                .Select(b=>new BorwedBookViewModel
                {
                    BookId = id,                   
                    StartDate= b.StartDate,
                    EndDate = b.EndDate
        })
                .ToList();
        }
    }
}