using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TacticalShop.Frontend.Services;

namespace TacticalShop.Frontend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        public ProductController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _productApiClient.GetProduct(id);
            return View(product);
        }

    }
}
