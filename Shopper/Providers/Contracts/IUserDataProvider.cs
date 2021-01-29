using Shopper.Providers.Models;
using System.Threading.Tasks;

namespace Shopper.Providers.Contracts
{
    public interface IUserDataProvider
    {
        Task<UserTokenResponse> GetUserTokenDataAsync();
    }
}
