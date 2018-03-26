using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreProject.Models;
using System.Data.Entity;

namespace StoreProject.Dal
{
    public class CustomersDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customers>().ToTable("Customers");
        }

        public DbSet<Customers> customers { get; set; }
    }

}