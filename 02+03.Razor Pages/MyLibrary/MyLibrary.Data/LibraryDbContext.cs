namespace MyLibrary.Data
{       
    using Microsoft.EntityFrameworkCore;
    using MyLibrary.Models;

    public class LibraryDbContext : DbContext
    {
        // When we give options in StartUp.sc we have to write this an dnot to initizlize 
        // DbContext in Program.cs - Var.2
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowerBook> BorrowerBooks { get; set; }

        //// Connection string can be configured here /var.1/ or in Startup class /var.2/
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder
        //        .UseSqlServer(@"Server=DESKTOP-LPPTMS9\SQLEXPRESS;Database=MyLibrary_WepPages;Integrated Security=True;");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}

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
