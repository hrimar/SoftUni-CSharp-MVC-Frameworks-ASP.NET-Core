namespace MyLibrary.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Book
    {
        public Book()
        {
            this.BookBorrowers = new List<BorrowerBook>();
        }

        public int Id { get; set; }


        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        public string  Description { get; set; }

        public string CoverImage { get; set; }
                
        public string Status { get; set; } //

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<BorrowerBook> BookBorrowers { get; set; }
    }
}
