using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace TacticalShop.Backend.Models
{
    public class Product
    {
        public int ProductId { get; set; }


        [MaxLength(50, ErrorMessage = "Maximum length for the Product Name is 50 characters.")]
        [Required(ErrorMessage = "Product Name is a required field.")]
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        [Required(ErrorMessage = "Product Price is a required field.")]
        public decimal ProductPrice { get; set; }

        public string ProductDescription { get; set; }

        [NotMapped] public IFormFile ProductImage { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ProductImageName { get; set; }

    
        [Column(TypeName = "varchar(20)")]
        public int ProductQuantity { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}