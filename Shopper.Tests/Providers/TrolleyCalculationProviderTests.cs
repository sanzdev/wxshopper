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
    public class TrolleyCalculationProviderTests : IClassFixture<ChallengeMockTestFixture>
    {
        private readonly ILogger<TrolleyCalculationProvider> _mockLogger;
        private readonly IConfiguration _config;
        private TrolleyCalculationProvider _trolleyCalculationProvider;
        private HttpClient _mockHttpClient;

        public TrolleyCalculationProviderTests(ChallengeMockTestFixture fixture)
        {
            _config = fixture.MockConfiguration;
            _mockLogger = TrolleyCalculationProviderMockHelper.GetTrolleycalculationProviderLogger();
        }

        [Fact(DisplayName = "Test Getting trolley totals.")]
        public async Task GetTrolley_Total_Returns_Total()
        {
            _mockHttpClient = TrolleyCalculationProviderMockHelper.GetMockSuccessfullTrolleyCalculationProviderHttpClient();
            _trolleyCalculationProvider = new TrolleyCalculationProvider(_mockHttpClient, _mockLogger, _config);
            var trolleyCalculationRequest = TrolleyCalculationProviderMockHelper.BuildTrolleyCalculationRequest();

            var result = await _trolleyCalculationProvider.GetTrolleyTotalAsync(trolleyCalculationRequest);
            var trolleyTotal = result.TrolleyTotal;

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            trolleyTotal.Should().BeGreaterThan(0);
        }
    }
}
