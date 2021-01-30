using Shopper.Models;
using Shopper.Providers.Models;
using System.Threading.Tasks;

namespace Shopper.Providers.Contracts
{
    public interface IProductsProvider
    {
        Task<ProductResponse> GetProductsAsync(SortOptions sort);
    }
}
