using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StoreProject.Models
{
    public class Customers
    {
        [Key]
        [MaxLength(10, ErrorMessage = "max lenght is 10")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "max lenght is 10")]
        public string LastName { get; set; }


        [Required]
        public string CustomerPassword { get; set; }

     

        [Required]
        [RegularExpression("[a-z0-9A-Z._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$", ErrorMessage = "English char and numbers ")]
        public string Email { get; set; }


        public string PhoneNumber { get; set; }

    }
}