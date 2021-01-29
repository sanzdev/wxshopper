using System.Threading.Tasks;
using Shopper.Providers;
using Shopper.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Shopper.Tests.Providers
{
    public class UserDataProviderTests 
    {
        private readonly ILogger<UserDataProvider> _mockLogger;
        private UserDataProvider _userDataProvider;

        public UserDataProviderTests()
        {
            _mockLogger = UserDataProviderMockHelper.GetUserDataProviderLogger();
        }

        [Fact(DisplayName = "Test get user data.")]
        public async Task GetUserTokenData_Returns_User_Token()
        {
            _userDataProvider = new UserDataProvider(_mockLogger);

            var result = await _userDataProvider.GetUserTokenDataAsync();
            var tokenData = result.UserTokenData;

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            tokenData.Should().NotBeNull();
            tokenData.Name.Should().NotBeNullOrEmpty();
            tokenData.Token.Should().NotBeNullOrEmpty();
        }
    }
}
