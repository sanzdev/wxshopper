using Shopper.Providers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Shopper.Tests.Helpers
{
    public static class UserDataProviderMockHelper
    {
        public static ILogger<UserDataProvider> GetUserDataProviderLogger()
        {
            return Mock.Of<ILogger<UserDataProvider>>();
        }
    }
}
