using System.Collections.Generic;
using System.Threading.Tasks;
using TacticalShop.ViewModels;

namespace TacticalShop.Frontend.Services
{
    public interface IProductApiClient
    {
        Task<IList<ProductVm>> GetProducts(int? pagenumber, int? pagesize, int? categoryid, int? brandid);

        Task<ProductVm> GetProduct(int id);
    }
}