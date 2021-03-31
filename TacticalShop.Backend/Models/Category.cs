using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TacticalShop.Backend.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [StringLength(30)]
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}