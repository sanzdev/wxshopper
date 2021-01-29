using Shopper.Models;
using Shopper.Providers.Contracts;
using Shopper.Providers.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Shopper.Configuration;

namespace Shopper.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ILogger<ProductsProvider> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IShopperHistoryProvider _shopperHistoryProvider;

        public ProductsProvider(HttpClient httpClient, ILogger<ProductsProvider> logger, IConfiguration config, IShopperHistoryProvider shopperHistoryProvider)
        {
            _httpClient = httpClient;
            _logger = logger;
            _config = config;
            _shopperHistoryProvider = shopperHistoryProvider;
        }

        public async Task<ProductResponse> GetProductsAsync(SortOptions sort)
        {
            var response = await GetAllProductsAsync();

            if (!response.Success)
                _logger.LogError($"Failed getting products - {response.Message}");

            response.Products = sort switch
            {
                SortOptions.Low => response.Products.OrderBy(item => item.Price),
                SortOptions.High => response.Products.OrderByDescending(item => item.Price),
                SortOptions.Ascending => response.Products.OrderBy(item => item.Name),
                SortOptions.Descending => response.Products.OrderByDescending(item => item.Name),
                SortOptions.Recommended => await OrderByRecommendationAsync(response),
                _ => response.Products.OrderBy(item => item.Name)
            };

            return response;
        }

        private async Task<IOrderedEnumerable<Product>> OrderByRecommendationAsync(ProductResponse response)
        {
            var popularProductsResponse = await GetMostPopularProductsAsync();

            if (popularProductsResponse.Success)
                return response.Products.OrderBy(item => popularProductsResponse.PopularProducts.IndexOf(item.Name) == -1 ?
                    int.MaxValue : popularProductsResponse.PopularProducts.IndexOf(item.Name));

            response.Success = popularProductsResponse.Success;
            response.StatusCode = popularProductsResponse.StatusCode;
            response.Message = popularProductsResponse.Message;
            _logger.LogError($"Failed getting popular products - {response.Message}");
            return null;
        }

        private async Task<ProductResponse> GetAllProductsAsync()
        {
            var configuration = _config.Get<ApplicationConfig>();

            var queryParams = new Dictionary<string, string>()
            {
                { "token", configuration.WxApiConfig.Token }
            };

            var uri = QueryHelpers.AddQueryString(configuration.WxApiConfig.ProductsBaseUrl, queryParams);

            using var response = await _httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new ProductResponse() { Success = response.IsSuccessStatusCode, StatusCode = response.StatusCode, Message = response.ReasonPhrase };

            var result = response.Content.ReadAsStringAsync().Result;
            var latestRate = JsonConvert.DeserializeObject<IEnumerable<Product>>(result);
            return new ProductResponse() { Success = response.IsSuccessStatusCode, StatusCode = response.StatusCode, Products = latestRate };

        }

        private async Task<ProductResponse> GetMostPopularProductsAsync()
        {
            var shopperHistoryProvider = await _shopperHistoryProvider.GetShopperHistoryAsync();

            if (!shopperHistoryProvider.Success)
                return new ProductResponse() { Success = shopperHistoryProvider.Success, StatusCode = shopperHistoryProvider.StatusCode, Message = shopperHistoryProvider.Message };

            var products = new List<Product>();
            foreach (var data in shopperHistoryProvider.ShopperHistory)
            {
                products.AddRange(data.Products);
            }

            var topProducts = products.GroupBy(item => item.Name)
                .Select(item => new
                {
                    Name = item.Key,
                    Count = item.Sum(p => p.Quantity)
                })
                .OrderByDescending(item => item.Count)
                .Select(s => s.Name).ToList();

            return new ProductResponse() { Success = shopperHistoryProvider.Success, StatusCode = shopperHistoryProvider.StatusCode, PopularProducts = topProducts };
        }
    }
}
