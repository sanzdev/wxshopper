using Shopper.Models;

namespace Shopper.Providers.Models
{
    public class TrolleyCalculationRequest
    {
        public Product[] Products { get; set; }
        public Special[] Specials { get; set; }
        public Quantity[] Quantities { get; set; }
    }

    public class Special
    {
        public Quantity[] Quantities { get; set; }
        public int Total { get; set; }
    }

    public class Quantity
    {
        public string Name { get; set; }
        public int quantity { get; set; }
    }
}
