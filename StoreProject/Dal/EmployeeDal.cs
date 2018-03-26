using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using StoreProject.Models;


namespace StoreProject.Dal
{
    public class EmployeeDal:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().ToTable("Employee");
        }

        public DbSet<Employee> employee { get; set; }
    }
}