using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TacticalShop.Frontend.Services;

namespace TacticalShop.Frontend.Components
{
    public class BrandMenuViewComponent : ViewComponent
    {
        private readonly IBrandApiClient _brandApiClient;
        public BrandMenuViewComponent(IBrandApiClient brandApiClient)
        {
            _brandApiClient = brandApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brand = await _brandApiClient.GetBrands();
            return View(brand);
        }
    }
}
