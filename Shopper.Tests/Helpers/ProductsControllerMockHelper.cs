using Shopper.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Shopper.Tests.Helpers
{
    public static class ProductsControllerMockHelper
    {
        public static ILogger<ProductsController> GetProductsControllerLogger()
        {
            return Mock.Of<ILogger<ProductsController>>();
        }

    }
}
