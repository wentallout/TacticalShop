using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TacticalShop.ViewModels;

namespace TacticalShop.Frontend.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _client;

        public ProductApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IList<ProductVm>> GetProducts(int? pagenumber, int? pagesize, int? categoryid, int? brandid)
        {
            var response = await _client.GetAsync($"api/products?pagenumber={pagenumber}&pagesize={pagesize}&categoryid={categoryid}&brandid={brandid}");

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }

        public async Task<ProductVm> GetProduct(int id)
        {
            var response = await _client.GetAsync("api/products/" + id);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductVm>();
        }
    }
}