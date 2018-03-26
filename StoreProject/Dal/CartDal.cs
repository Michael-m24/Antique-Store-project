using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreProject.Models;
using System.Data.Entity;

namespace StoreProject.Dal
{
    public class CartDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cart>().ToTable("Cart");
        }

        public DbSet<Cart> cart { get; set; }

    }
}