using Microsoft.EntityFrameworkCore;
namespace Packt.Shared
{
    public class Northwind: DbContext
    {
        public DbSet<Category> Categories{get;set;}
        public DbSet<Product> Products{get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            string path = System.IO.Path.Combine(
                @"C:\SQL-DATA\MyTest","Northwind.db"
            );
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        //Sqlite 轉decimal的問題, 要加以下code
        protected override void OnModelCreating(
            ModelBuilder modelBuilder
        ){
            modelBuilder.Entity<Product>()
                .Property(product => product.UnitPrice)
                .HasConversion<double>();

        }
    }
}