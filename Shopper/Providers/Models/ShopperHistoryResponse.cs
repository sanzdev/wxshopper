using Shopper.Models;
using System.Collections.Generic;

namespace Shopper.Providers.Models
{
    public class ShopperHistoryResponse : BaseResponse
    {
        public IEnumerable<ShopperHistory> ShopperHistory { get; set; }
    }
}
