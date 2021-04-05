using TacticalShop.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TacticalShop.Frontend.Services
{
    public interface IProductApiClient
    {
        Task<IList<ProductVm>> GetProducts();
    }
}