namespace MyLibrary.Data
{       
    using Microsoft.EntityFrameworkCore;
    using MyLibrary.Models;

    public class LibraryDbContext : DbContext
    {       
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {  }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowerBook> BorrowerBooks { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowerBook>()
                .HasKey(bb => new { bb.BorrowerId, bb.BookId });

            modelBuilder.Entity<Borrower>()
                .HasMany(b => b.BorrowedBooks)
                .WithOne(br => br.Borrower)
                .HasForeignKey(b => b.BorrowerId);

            modelBuilder.Entity<Book>()
               .HasMany(b => b.BookBorrowers)
               .WithOne(br => br.Book)
               .HasForeignKey(b => b.BookId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
