using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticalShop.ViewModels
{
    public class BrandVm
    {
        
        public int BrandId { get; set; }
        
        [StringLength(30)]
        public string BrandName { get; set; }
    }
}
