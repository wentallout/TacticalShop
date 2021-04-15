using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace TacticalShop.Backend.Models
{
    public class Product
    {
        public Product()
        {
            Ratings = new HashSet<Rating>();
        }

        public int ProductId { get; set; }


        [MaxLength(50, ErrorMessage = "Maximum length for the Product Name is 50 characters.")]
        [Required(ErrorMessage = "Product Name is a required field.")]
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        [Required(ErrorMessage = "Product Price is a required field.")]
        public decimal ProductPrice { get; set; }

        public string ProductDescription { get; set; }

        [NotMapped] public IFormFile ProductImage { get; set; }


        [Column(TypeName = "varchar(300)")] public string ProductImageName { get; set; }


        [Column(TypeName = "varchar(20)")] public int ProductQuantity { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int StarRating { get; set; }


        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Rating> Ratings { get; set; }
    }
}