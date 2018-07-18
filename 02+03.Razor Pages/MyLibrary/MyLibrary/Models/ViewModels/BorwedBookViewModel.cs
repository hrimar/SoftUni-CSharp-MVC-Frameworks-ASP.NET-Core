using System;

namespace MyLibrary.Models.ViewModels
{
    public class BorwedBookViewModel
    {
        public int BookId { get; set; }             

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
