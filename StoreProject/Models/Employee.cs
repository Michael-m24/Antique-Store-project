using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreProject.Models
{
    public class Employee
    {

        [Key]
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeePassword { get; set; }


    }
}