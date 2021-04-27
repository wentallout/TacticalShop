using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TacticalShop.Frontend.Services;

namespace TacticalShop.Frontend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IRatingApiClient _ratingApiClient;

        public ProductController(IProductApiClient productApiClient, IRatingApiClient ratingApiClient)
        {
            _productApiClient = productApiClient;
            _ratingApiClient = ratingApiClient;
        }

        // public async Task<IActionResult> Index(int? categoryid = null, int? brandid = null)
        // {
        //     var product = await _productApiClient.GetFilteredProducts(categoryid, brandid);
        //     return View(product);
        // }

        public async Task<IActionResult> Index(int? pagenumber = 1, int? pagesize = 6, int? categoryid = null, int? brandid = null)
        {
            var product =
                await _productApiClient.GetProducts(pagenumber, pagesize, categoryid, brandid);
            return View(product);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productApiClient.GetProduct(id);
            if (User.Identity.IsAuthenticated)
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                string userid = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var rating = await _ratingApiClient.GetRating(userid, id);
                ViewData["RatingData"] = rating;
            }
            return View(product);
        }
    }
}