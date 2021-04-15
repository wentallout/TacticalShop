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

        [MaxLength(30)] [Required] public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}