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
        public async Task<IActionResult> Index(int? categoryid = null, int? brandid = null)
        {

            var product = await _productApiClient.GetFilteredProducts(categoryid, brandid);
            return View(product);
        }





        public async Task<IActionResult> Detail(int productid)
        {
            var product = await _productApiClient.GetProduct(productid);
            return View(product);
        }




    }
}
