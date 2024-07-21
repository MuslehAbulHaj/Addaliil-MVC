using Addaliil_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Addaliil_MVC.Data
{
    public class AdaliiDbContext : DbContext
    {
        public AdaliiDbContext(DbContextOptions<AdaliiDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Category 1", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Category 2", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Category 3", DisplayOrder = 3 }
                );
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shop>().HasData(
                new Shop { Id = 1, Name = "Shop1", Description = "1", ProductId = 1 },
                new Shop { Id = 2, Name = "Shop2", Description = "2", ProductId = 2 },
                new Shop { Id = 3, Name = "Shop3", Description = "3", ProductId = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product1", Description = "1", CategoryId = 1 },
                new Product { Id = 2, Name = "Product2", Description = "2", CategoryId = 2 },
                new Product { Id = 3, Name = "Product3", Description = "3", CategoryId = 3 }
                );
        }
    }
}
