using Shopper.Controllers;
using Shopper.Providers;
using Shopper.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Shopper.Tests.Controllers
{
    public class ProductsControllerTests : IClassFixture<ChallengeMockTestFixture>
    {
        private readonly ProductsController _controller;

        public ProductsControllerTests(ChallengeMockTestFixture fixture)
        {
            var config = fixture.MockConfiguration;
            var mockShopperHistoryProviderLogger = ShopperHistoryProviderMockHelper.GetShopperHistoryProviderLogger();
            var mockProductsProviderLogger = ProductProviderMockHelper.GetProductsProviderLogger();
            var mockLogger = ProductsControllerMockHelper.GetProductsControllerLogger();
            
            var mockHttpClient = ProductProviderMockHelper.GetMockSuccessfullProductsProviderHttpClient();
            var shopperHistoryProvider = new ShopperHistoryProvider(mockHttpClient, mockShopperHistoryProviderLogger, config);
            var productsProvider = new ProductsProvider(mockHttpClient, mockProductsProviderLogger, config, shopperHistoryProvider);

            _controller = new ProductsController(mockLogger, productsProvider);
        }

        [Fact(DisplayName = "Test get products api success.")]
        public void GetProducts_When_SortOrder_Valid_ReturnsOkResult()
        {
            var result = _controller.GetProducts("Low");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact(DisplayName = "Test get products api bad request.")]
        public void GetProducts_When_SortOrder_InValid_ReturnsBadRequestResult()
        {
            var result = _controller.GetProducts("invalidsort");
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact(DisplayName = "Test get products with default sort.")]
        public void GetProducts_When_SortOrder_None_ReturnsOkResult()
        {
            var result = _controller.GetProducts(string.Empty);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
