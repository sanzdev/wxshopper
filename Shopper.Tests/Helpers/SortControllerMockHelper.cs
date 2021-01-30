using Shopper.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Shopper.Tests.Helpers
{
    public static class SortControllerMockHelper
    {
        public static ILogger<SortController> GetProductsControllerLogger()
        {
            return Mock.Of<ILogger<SortController>>();
        }

    }
}
