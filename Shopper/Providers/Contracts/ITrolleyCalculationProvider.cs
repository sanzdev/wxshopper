using Shopper.Providers.Models;
using System.Threading.Tasks;

namespace Shopper.Providers.Contracts
{
    public interface ITrolleyCalculationProvider
    {
        Task<TrolleyCalculationResponse> GetTrolleyTotalAsync(TrolleyCalculationRequest request);
    }
}
