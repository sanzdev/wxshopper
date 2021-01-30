using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shopper.Models;
using Shopper.Providers;
using Shopper.Tests.Helpers;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Shopper.Tests.Providers
{
    public class ProductProviderTests : IClassFixture<ChallengeMockTestFixture>
    {
        private readonly ILogger<ProductsProvider> _mockProductsProviderLogger;
        private readonly ILogger<ShopperHistoryProvider> _mockShopperHistoryProviderLogger;
        private readonly IConfiguration _config;
        private ShopperHistoryProvider _shopperHistoryProvider;
        private ProductsProvider _productsProvider;
        private HttpClient _mockHttpClient;

        public ProductProviderTests(ChallengeMockTestFixture fixture)
        {
            _config = fixture.MockConfiguration;
            _mockProductsProviderLogger = ProductProviderMockHelper.GetProductsProviderLogger();
            _mockShopperHistoryProviderLogger = ShopperHistoryProviderMockHelper.GetShopperHistoryProviderLogger();
        }

        [Fact(DisplayName = "Test sort Ascending")]
        public async Task GetProdcuts_With_SortOption_Ascending_Returns_ProductsOrderedby_Name()
        {
            _mockHttpClient = ProductProviderMockHelper.GetMockSuccessfullProductsProviderHttpClient();
            _shopperHistoryProvider = new ShopperHistoryProvider(_mockHttpClient, _mockShopperHistoryProviderLogger, _config);
            _productsProvider = new ProductsProvider(_mockHttpClient, _mockProductsProviderLogger, _config, _shopperHistoryProvider);

            var result = await _productsProvider.GetProductsAsync(SortOptions.Ascending);
            var products = result.Products.ToList();

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            products.Should().NotBeNull();
            Assert.Equal("Test Product A", products.FirstOrDefault().Name);
        }

        [Fact(DisplayName = "Test sort Descending")]
        public async Task GetProdcuts_With_SortOption_Descending_Returns_ProductsOrderedby_Name_Descending()
        {
            _mockHttpClient = ProductProviderMockHelper.GetMockSuccessfullProductsProviderHttpClient();
            _shopperHistoryProvider = new ShopperHistoryProvider(_mockHttpClient, _mockShopperHistoryProviderLogger, _config);
            _productsProvider = new ProductsProvider(_mockHttpClient, _mockProductsProviderLogger, _config, _shopperHistoryProvider);

            var result = await _productsProvider.GetProductsAsync(SortOptions.Descending);
            var products = result.Products.ToList();

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            products.Should().NotBeNull();
            Assert.Equal("Test Product F", products.FirstOrDefault().Name);
        }
    }
}
