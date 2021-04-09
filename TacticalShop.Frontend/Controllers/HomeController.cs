using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using TacticalShop.Frontend.Models;
using TacticalShop.Frontend.Services;

namespace TacticalShop.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductApiClient _productApiClient;
        public HomeController(ILogger<HomeController> logger, IProductApiClient productApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;

        }



        public async Task<IActionResult> Index()
        {
            var products = await _productApiClient.GetProducts();
            return View(products);
        }

        public IActionResult About()
        {

            return View();
        }

        public IActionResult Faq()
        {

            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
