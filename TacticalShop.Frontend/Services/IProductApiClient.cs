using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacticalShop.ViewModels;

namespace TacticalShop.Frontend.Services
{
    public interface IProductApiClient
    {
        Task<IList<ProductVm>> GetProducts();
    }
}
