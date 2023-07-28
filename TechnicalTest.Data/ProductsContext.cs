using Microsoft.EntityFrameworkCore;
using TechnicalTest.Data.Entities;

namespace TechnicalTest.Data
{
    public class ProductsContext: DbContext
    {
       /* public ProductsContext(DbContextOptionsBuilder optionsBuilder) 
        {
            
        }*/
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
            .   WithMany(b => b.Products)
                .HasForeignKey(p => p.CategoryCode)
                .HasPrincipalKey(b => b.Code);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=localhost;Database=productv1;Username=postgres;Password=!qazxsw2");
    }
}
