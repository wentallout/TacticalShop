using Microsoft.AspNetCore.Http;
using System;

namespace TacticalShop.ViewModels
{
    public class ProductCreateRequest
    {
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductDescription { get; set; }

        public IFormFile ProductImage { get; set; }

        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int ProductQuantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public ProductCreateRequest()
        {
            this.CreatedDate = DateTime.UtcNow;
        }
    }
}