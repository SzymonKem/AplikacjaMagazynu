using Microsoft.EntityFrameworkCore;
namespace magazyn.Models
{


    public class LaptopyDbContext : DbContext
    {
        public DbSet<Laptop> Laptopy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Laptopy.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Laptop>().ToTable("laptopy");
        }
    }
}

