using Shopper.Providers.Models;
using System.Threading.Tasks;

namespace Shopper.Providers.Contracts
{
    public interface IShopperHistoryProvider
    {
        Task<ShopperHistoryResponse> GetShopperHistoryAsync();
    }
}
