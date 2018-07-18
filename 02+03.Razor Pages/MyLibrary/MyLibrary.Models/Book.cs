using System;
using System.Collections.Generic;

namespace MyLibrary.Models
{
    public class Book
    {
        public Book()
        {
            this.BookBorrowers = new List<BorrowerBook>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string  Description { get; set; }

        public string CoverImage { get; set; }
                
        public string Status { get; set; } //

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<BorrowerBook> BookBorrowers { get; set; }
    }
}
