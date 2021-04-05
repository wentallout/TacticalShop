using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

    namespace TacticalShop.ViewModels
    {
        public class ProductVm
        {
            
            public int ProductId { get; set; }
            public string ProductName { get; set; }

            public decimal ProductPrice { get; set; }

            public string ProductDescription { get; set; }

            
          
            public string ProductImageName { get; set; }
            public int ProductQuantity { get; set; }

            public int CategoryId { get; set; }

            

            public string CategoryName { get; set; }

            public string BrandName { get; set; }

            public DateTime CreatedDate { get; set; }

            public DateTime UpdatedDate { get; set; }


        }
    }
    

