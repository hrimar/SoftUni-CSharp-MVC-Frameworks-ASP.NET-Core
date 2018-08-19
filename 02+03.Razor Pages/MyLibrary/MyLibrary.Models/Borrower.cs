namespace MyLibrary.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class Borrower
    {
        public Borrower()
        {
            this.BorrowedBooks = new List<BorrowerBook>();
        }

        public int Id { get; set; }
        
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string  Address { get; set; }

        public List<BorrowerBook> BorrowedBooks { get; set; }
    }
}
