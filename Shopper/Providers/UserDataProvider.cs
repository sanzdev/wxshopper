using Microsoft.Extensions.Logging;
using Shopper.Models;
using Shopper.Providers.Contracts;
using Shopper.Providers.Models;
using System;
using System.Threading.Tasks;

namespace Shopper.Providers
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly ILogger<UserDataProvider> _logger;
        private const string Username = "Sanath Chakkarayan";

        public UserDataProvider(ILogger<UserDataProvider> logger)
        {
            _logger = logger;
        }

        public async Task<UserTokenResponse> GetUserTokenDataAsync()
        {
            _logger.LogInformation("user data requested");
            var tokenData = new UserTokenData() { Name = Username, Token = Guid.NewGuid().ToString() };
            var response = new UserTokenResponse() { Success = true, UserTokenData = tokenData };
            return await Task.FromResult(response);
        }
    }
}
