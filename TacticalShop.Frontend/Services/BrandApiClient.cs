using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TacticalShop.ViewModels;

namespace TacticalShop.Frontend.Services
{
    public class BrandApiClient : IBrandApiClient
    {
        private readonly HttpClient _client;

        public BrandApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IList<BrandVm>> GetBrands()
        {
            var response = await _client.GetAsync("api/brands");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<BrandVm>>();
        }
    }
}
