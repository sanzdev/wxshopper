using Shopper.Providers;
using Shopper.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Shopper.Tests.Providers
{
    public class ShopperHistoryProviderTests : IClassFixture<ChallengeMockTestFixture>
    {
        private readonly ILogger<ShopperHistoryProvider> _mockLogger;
        private readonly IConfiguration _config;
        private ShopperHistoryProvider _shopperHistoryProvider;
        private HttpClient _mockHttpClient;

        public ShopperHistoryProviderTests(ChallengeMockTestFixture fixture)
        {
            _config = fixture.MockConfiguration;
            _mockLogger = ShopperHistoryProviderMockHelper.GetShopperHistoryProviderLogger();
        }

        [Fact(DisplayName = "Test getting shopping history.")]
        public async Task GetShopperHistory_Returns_All_History()
        {
            _mockHttpClient = ShopperHistoryProviderMockHelper.GetMockSuccessfullShopperHistoryProviderHttpClient();
            _shopperHistoryProvider = new ShopperHistoryProvider(_mockHttpClient, _mockLogger, _config);

            var result = await _shopperHistoryProvider.GetShopperHistoryAsync();
            var shopperHistory = result.ShopperHistory;

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            shopperHistory.Should().NotBeNull();
        }

        [Fact(DisplayName = "Test shopping history empty.")]
        public async Task GetShopperHistory_Returns_Empty_History()
        {
            _mockHttpClient = ShopperHistoryProviderMockHelper.GetMockFailedShopperHistoryProviderHttpClient();
            _shopperHistoryProvider = new ShopperHistoryProvider(_mockHttpClient, _mockLogger, _config);

            var result = await _shopperHistoryProvider.GetShopperHistoryAsync();
            var shopperHistory = result.ShopperHistory;

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            shopperHistory.Should().BeNull();
        }
    }
}
