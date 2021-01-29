using Shopper.Providers;
using Shopper.Providers.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Shopper.Tests.Helpers
{
    public static class TrolleyCalculationProviderMockHelper
    {
        public static ILogger<TrolleyCalculationProvider> GetTrolleycalculationProviderLogger()
        {
            return Mock.Of<ILogger<TrolleyCalculationProvider>>();
        }

        public static HttpClient GetMockSuccessfullTrolleyCalculationProviderHttpClient()
        {
            var shopperHistoryResponse = new TrolleyCalculationResponse()
            {
                Success = true,
                TrolleyTotal = 100
            };

            return MockHttpClient(shopperHistoryResponse.TrolleyTotal, HttpStatusCode.OK);
        }

        public static TrolleyCalculationRequest BuildTrolleyCalculationRequest()
        {
            var trolley = "{\"products\":[{\"name\":\"string\",\"price\":10}],\"specials\":[{\"quantities\":[{\"name\":\"string\",\"quantity\":1}],\"total\":20}],\"quantities\":[{\"name\":\"string\",\"quantity\":1}]}";
            return JsonConvert.DeserializeObject<TrolleyCalculationRequest>(trolley);
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
