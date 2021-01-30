using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shopper.Configuration;
using Shopper.Providers.Contracts;
using Shopper.Providers.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shopper.Providers
{
    public class TrolleyCalculationProvider : ITrolleyCalculationProvider
    {
        private readonly ILogger<TrolleyCalculationProvider> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public TrolleyCalculationProvider(HttpClient httpClient, ILogger<TrolleyCalculationProvider> logger, IConfiguration config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _config = config;
        }

        public async Task<TrolleyCalculationResponse> GetTrolleyTotalAsync(TrolleyCalculationRequest request)
        {
            var trolleyTotalResponse = await GetTrolleyTotalsAsync(request);

            if (!trolleyTotalResponse.Success)
                _logger.LogError($"Error getting trolley total - {trolleyTotalResponse.Message}");

            return trolleyTotalResponse;
        }

        private async Task<TrolleyCalculationResponse> GetTrolleyTotalsAsync(TrolleyCalculationRequest trolley)
        {
            var configuration = _config.Get<ApplicationConfig>();

            var queryParams = new Dictionary<string, string>()
            {
                { "token", configuration.WxApiConfig.Token }
            };

            var uri = QueryHelpers.AddQueryString(configuration.WxApiConfig.TrolleyCalculatorBaseUrl, queryParams);

            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create(trolley)
            };

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return new TrolleyCalculationResponse() {Success = response.IsSuccessStatusCode, StatusCode = response.StatusCode, Message = response.ReasonPhrase };

            var result = response.Content.ReadAsStringAsync().Result;
            var total = JsonConvert.DeserializeObject<decimal>(result);
            return new TrolleyCalculationResponse() { Success = true, TrolleyTotal = total };
        }
    }
}
