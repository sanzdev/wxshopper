using System;
using Shopper.Models;
using Shopper.Providers.Contracts;
using Shopper.Providers.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shopper.Configuration;

namespace Shopper.Providers
{
    public class ShopperHistoryProvider : IShopperHistoryProvider
    {
        private readonly ILogger<ShopperHistoryProvider> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ShopperHistoryProvider(HttpClient httpClient, ILogger<ShopperHistoryProvider> logger, IConfiguration config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _config = config;
        }

        public async Task<ShopperHistoryResponse> GetShopperHistoryAsync()
        {
            var shopperHistory = await GetAllShopperHistoryAsync();

            if (!shopperHistory.Success)
                _logger.LogError($"Failed getting shopper history - {shopperHistory.Message}");

            return shopperHistory;
        }

        private async Task<ShopperHistoryResponse> GetAllShopperHistoryAsync()
        {
            var configuration = _config.Get<ApplicationConfig>();

            var queryParams = new Dictionary<string, string>()
            {
                { "token", configuration.WxApiConfig.Token }
            };

            var uri = QueryHelpers.AddQueryString(configuration.WxApiConfig.ShopperHistoryBaseUrl, queryParams);

            using var response = await _httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new ShopperHistoryResponse() { Success = response.IsSuccessStatusCode, StatusCode = response.StatusCode, Message = response.ReasonPhrase };

            var result = response.Content.ReadAsStringAsync().Result;
            var history = JsonConvert.DeserializeObject<IEnumerable<ShopperHistory>>(result);
            return new ShopperHistoryResponse() { Success = response.IsSuccessStatusCode, ShopperHistory = history };
        }
    }
}
