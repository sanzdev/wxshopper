using Shopper.Models;
using System.Collections.Generic;

namespace Shopper.Providers.Models
{
    public class ProductResponse : BaseResponse
    {
        public IEnumerable<Product> Products { get; set; }

        public List<string> PopularProducts { get; set; }
    }
}
