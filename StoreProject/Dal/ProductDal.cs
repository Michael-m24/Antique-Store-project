using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StoreProject.Models;
namespace StoreProject.Dal
{
    public class ProductDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Product");
        }

        public DbSet<Product> product{ get; set; }



    }
}