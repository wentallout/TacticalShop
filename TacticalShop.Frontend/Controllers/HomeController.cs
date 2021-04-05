using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TacticalShop.Frontend.Models;
using TacticalShop.Frontend.Services;

namespace TacticalShop.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        private readonly IProductApiClient _productApiClient;
        public HomeController(ILogger<HomeController> logger,IProductApiClient productApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;

        }

        

        public async Task<IActionResult> Index()
        {
            var products = await _productApiClient.GetProducts();
            return View(products);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
