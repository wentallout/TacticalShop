using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TacticalShop.Frontend.Services;

namespace TacticalShop.Frontend.Components
{
    public class ModalViewComponent : ViewComponent
    {
        private readonly IProductApiClient _productApiClient;

        public ModalViewComponent(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var product = await _productApiClient.GetProduct(id);
            return View(product);
        }
    }
}