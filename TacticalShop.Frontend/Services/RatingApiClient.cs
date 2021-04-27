using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TacticalShop.ViewModels;

namespace TacticalShop.Frontend.Services
{
    public class RatingApiClient : IRatingApiClient
    {
        private readonly HttpClient _httpClient;

        public RatingApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RatingVm> GetRating(string userid, int id)
        {
            var response = await _httpClient.GetAsync("api/Ratings/" + userid + "/" + id);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<RatingVm>();
        }

        public async Task<RatingCreateRequest> CreateRating(RatingCreateRequest ratingCreateRequest)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(ratingCreateRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Ratings", httpContent);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<RatingCreateRequest>();
        }

        public async Task<RatingUpdateRequest> UpdateRating(RatingUpdateRequest ratingUpdateRequest)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(ratingUpdateRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/Ratings", httpContent);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<RatingUpdateRequest>();
        }
    }
}