using System.Threading.Tasks;
using TacticalShop.ViewModels;

namespace TacticalShop.Frontend.Services
{
    public interface IRatingApiClient
    {
        Task<RatingVm> GetRating(string userid, int productid);
        Task<RatingCreateRequest> CreateRating(RatingCreateRequest ratingCreateRequest);
        Task<RatingUpdateRequest> UpdateRating(RatingUpdateRequest ratingUpdateRequest);
    }
}
