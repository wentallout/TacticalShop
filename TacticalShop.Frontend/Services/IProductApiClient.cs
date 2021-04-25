using System.Collections.Generic;
using System.Threading.Tasks;
using TacticalShop.ViewModels;

namespace TacticalShop.Frontend.Services
{
    public interface IProductApiClient
    {
        Task<IList<ProductVm>> GetProducts();

        Task<ProductVm> GetProduct(int id);

        Task<IList<ProductVm>> GetFilteredProducts(int? categoryid, int? brandid);
    }
}