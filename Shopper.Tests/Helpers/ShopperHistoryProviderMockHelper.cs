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
    public static class ShopperHistoryProviderMockHelper
    {
        public static ILogger<ShopperHistoryProvider> GetShopperHistoryProviderLogger()
        {
            return Mock.Of<ILogger<ShopperHistoryProvider>>();
        }

        public static HttpClient GetMockSuccessfullShopperHistoryProviderHttpClient()
        {
            var shopperHistoryResponse = new ShopperHistoryResponse()
            {
                Success = true,
                ShopperHistory = new List<ShopperHistory>()
            };

            return MockHttpClient(shopperHistoryResponse.ShopperHistory, HttpStatusCode.OK);
        }

        public static HttpClient GetMockFailedShopperHistoryProviderHttpClient()
        {
            var shopperHistoryResponse = new ShopperHistoryResponse()
            {
                Success = false,
                ShopperHistory = null
            };

            return MockHttpClient(shopperHistoryResponse.ShopperHistory, HttpStatusCode.OK);
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
