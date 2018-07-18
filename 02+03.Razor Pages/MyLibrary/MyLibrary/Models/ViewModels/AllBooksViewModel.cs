using System;
using System.Collections.Generic;
using System.Linq;
namespace MyLibrary.Models.ViewModels
{
    public class AllBooksViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
               
        public string Status { get; set; }
               
        public string Author { get; set; }

        public int AuthorId { get; set; }

        public static Func<Book, AllBooksViewModel> FromBook
        {
            get
            {
                return book => new AllBooksViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author.Name,
                    AuthorId = book.AuthorId,
                    Status = book.Status
                };
            }
        }
    }
}
