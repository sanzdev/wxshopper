using Shopper.Models;

namespace Shopper.Providers.Models
{
    public class TrolleyCalculationRequest
    {
        public Product[] Products { get; set; }
        public Special[] Specials { get; set; }
        public Quantities[] Quantities { get; set; }
    }
}
