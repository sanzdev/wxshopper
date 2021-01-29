using Shopper.Models;
using Shopper.Providers;
using Shopper.Providers.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Shopper.Tests.Helpers
{
    public static class ProductProviderMockHelper
    {
        public static ILogger<ProductsProvider> GetProductsProviderLogger()
        {
            return Mock.Of<ILogger<ProductsProvider>>();
        }

        public static HttpClient GetMockSuccessfullProductsProviderHttpClient()
        {
            var productResponse = new ProductResponse()
            {
                Success = true,
                Products = GetProductsOrderByName()
            };

            return MockHttpClient(productResponse.Products, HttpStatusCode.OK);
        }

        private static IEnumerable<Product> GetProductsOrderByName()
        {
            var products = "[{\"name\":\"Test Product A\",\"price\":99.99,\"quantity\":0},{\"name\":\"Test Product B\",\"price\":101.99,\"quantity\":0},{\"name\":\"Test Product C\",\"price\":10.99,\"quantity\":0},{\"name\":\"Test Product D\",\"price\":5,\"quantity\":0},{\"name\":\"Test Product F\",\"price\":999999999999,\"quantity\":0}]";
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(products);
        }

        private static HttpClient MockHttpClient<T>(T content, HttpStatusCode status)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = status,
                    Content = new StringContent(JsonConvert.SerializeObject(content))
                })
                .Verifiable();

            return new HttpClient(handlerMock.Object) { BaseAddress = new Uri("http://mockuri") };
        }
    }
}
