namespace FDMC.Data
{
    using FDMC.Models;
    using Microsoft.EntityFrameworkCore;

    public class FDMCDbContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }
      
        // Connection string can be configured here or in Startup class
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseSqlServer(@"Server=DESKTOP-LPPTMS9\SQLEXPRESS;Database=FDMC_MVC;Integrated Security=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
