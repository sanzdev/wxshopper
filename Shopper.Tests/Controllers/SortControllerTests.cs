using Shopper.Controllers;
using Shopper.Providers;
using Shopper.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Shopper.Tests.Controllers
{
    public class SortControllerTests : IClassFixture<ChallengeMockTestFixture>
    {
        private readonly SortController _controller;

        public SortControllerTests(ChallengeMockTestFixture fixture)
        {
            var config = fixture.MockConfiguration;
            var mockShopperHistoryProviderLogger = ShopperHistoryProviderMockHelper.GetShopperHistoryProviderLogger();
            var mockProductsProviderLogger = ProductProviderMockHelper.GetProductsProviderLogger();
            var mockLogger = SortControllerMockHelper.GetProductsControllerLogger();
            
            var mockHttpClient = ProductProviderMockHelper.GetMockSuccessfullProductsProviderHttpClient();
            var shopperHistoryProvider = new ShopperHistoryProvider(mockHttpClient, mockShopperHistoryProviderLogger, config);
            var productsProvider = new ProductsProvider(mockHttpClient, mockProductsProviderLogger, config, shopperHistoryProvider);

            _controller = new SortController(mockLogger, productsProvider);
        }

        [Fact(DisplayName = "Test get products api success.")]
        public void GetProducts_When_SortOrder_Valid_Returns_OkResult()
        {
            var result = _controller.GetProducts("Low");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact(DisplayName = "Test get products api bad request.")]
        public void GetProducts_When_SortOrder_InValid_Returns_BadRequestResult()
        {
            var result = _controller.GetProducts("invalidsort");
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact(DisplayName = "Test get products with no sort.")]
        public void GetProducts_When_SortOrder_None_Returns_BadRequestResult()
        {
            var result = _controller.GetProducts(string.Empty);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
