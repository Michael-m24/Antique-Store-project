using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreProject.Models
{
    public class Product
    {
        [Key]
        public string Model { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
    }
}