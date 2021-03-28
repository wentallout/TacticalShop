using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacticalShop.Backend.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ProductImageName { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}
