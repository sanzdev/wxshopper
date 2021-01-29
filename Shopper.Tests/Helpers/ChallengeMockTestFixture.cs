using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Shopper.Tests.Helpers
{
    public class ChallengeMockTestFixture 
    {
        public IConfiguration MockConfiguration { get; private set; }

        public ChallengeMockTestFixture()
        {
            MockConfiguration = GetMockConfiguration();
        }

        private static IConfiguration GetMockConfiguration()
        {
            var configurations = new Dictionary<string, string>
            {
                {"WxApiConfig:ProductsBaseUrl", string.Empty},
                {"WxApiConfig:ShopperHistoryBaseUrl", string.Empty},
                {"WxApiConfig:TrolleyCalculatorBaseUrl", string.Empty},
                {"WxApiConfig:Token", string.Empty}
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(configurations)
                .Build();
            return mockConfiguration;
        }
    }
}
