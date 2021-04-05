using Microsoft.AspNetCore.Http;
using TacticalShop.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TacticalShop.Frontend.Services;

namespace TacticalShop.Frontend.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<ProductVm>> GetProducts()
        {
            var httpClient = _httpClientFactory.CreateClient("local");
            var response = await httpClient.GetAsync("https://localhost:44341/api/products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IList<ProductVm>>();
        }
    }
}
