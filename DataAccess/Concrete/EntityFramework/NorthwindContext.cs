using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : DB tabloları ile proje classlarını bağlamak için kullanılır
    public class NorthwindContext : DbContext //EF Core dan DBContext implemente edilmeli
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //hangi db ile bağlanacak ayarının yapıldığı override method
            string connection = @"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true";

            optionsBuilder.UseSqlServer(connection);
        }
        public DbSet<Product> Products { get; set; }//Hangi class hangi tabloya karşılık geliyor
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
