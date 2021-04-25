using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TacticalShop.Frontend.Services;
using TacticalShop.ViewModels;

namespace TacticalShop.Frontend.Controllers
{
    public class RatingController : BaseController
    {
        private readonly IRatingApiClient _ratingApiClient;

        public RatingController(IRatingApiClient ratingApiClient)
        {
            _ratingApiClient = ratingApiClient;
        }

        public async Task<IActionResult> CreateRating(int id, int star)
        {
            var claimIdentity = User.Identity as ClaimsIdentity;
            string userid = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ratingCreateRequest = new RatingCreateRequest
            {
                UserId = userid,
                ProductId = id,
                Star = star,
            };
            await _ratingApiClient.CreateRating(ratingCreateRequest);
            return RedirectToAction("Detail", "Product", new { id });
        }

        public async Task<IActionResult> UpdateRating(int id, int star)
        {
            var claimIdentity = User.Identity as ClaimsIdentity;
            string userid = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ratingUpdateRequest = new RatingUpdateRequest
            {
                UserId = userid,
                ProductId = id,
                Star = star,
            };
            await _ratingApiClient.UpdateRating(ratingUpdateRequest);
            return RedirectToAction("Detail", "Product", new { id });
        }
    }
}