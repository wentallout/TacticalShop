using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using TacticalShop.Frontend.Services;
namespace TacticalShop.Frontend.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;
        public CategoryMenuViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _categoryApiClient.GetCategories();
            return View(category);
        }
    }
}
