using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;

namespace Packt.Shared
{
    public class Northwind:DbContext
    {
        public DbSet<Category> Categories {get;set;}
        public DbSet<Product> Products {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            string path=System.IO.Path.Combine(@"C:\SQL-DATA\MyTest","Northwind.db");
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlite($"FileName={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Category>()
                .Property(category => category.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.Discontinued);

            modelBuilder.Entity<Product>()
                .Property(p =>p.Cost)
                .HasConversion<double>();
        }
    }
}