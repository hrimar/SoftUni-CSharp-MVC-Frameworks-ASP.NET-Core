namespace MyLibrary.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class Author
    {
        public Author()
        {
            this.Books = new List<Book>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
