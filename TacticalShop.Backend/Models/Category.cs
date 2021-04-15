using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TacticalShop.Backend.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [MaxLength(30)] [Required] public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}