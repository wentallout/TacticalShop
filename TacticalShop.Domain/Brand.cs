using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TacticalShop.Domain
{
    public class Brand
    {
        public int BrandId { get; set; }

        [MaxLength(30)] [Required] public string BrandName { get; set; }
        public List<Product> Products { get; set; }
    }
}