using System.Collections.Generic;
using System.Threading.Tasks;
using TacticalShop.ViewModels;

namespace TacticalShop.Frontend.Services
{
    public interface ICategoryApiClient
    {
        Task<IList<CategoryVm>> GetCategories();
    }
}
