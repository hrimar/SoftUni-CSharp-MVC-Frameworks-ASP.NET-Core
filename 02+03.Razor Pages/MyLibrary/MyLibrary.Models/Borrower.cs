using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Models
{
  public  class Borrower
    {
        public Borrower()
        {
            this.BorrowedBooks = new List<BorrowerBook>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string  Address { get; set; }

        public List<BorrowerBook> BorrowedBooks { get; set; }
    }
}
