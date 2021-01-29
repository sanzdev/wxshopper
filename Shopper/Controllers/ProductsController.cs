using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopper.Models;
using Shopper.Providers.Contracts;
using System;
using System.Threading.Tasks;

namespace Shopper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsProvider _productsProvider;

        public ProductsController(ILogger<ProductsController> logger, IProductsProvider productsProvider)
        {
            _logger = logger;
            _productsProvider = productsProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts(string sortOption)
        {
            _logger.LogInformation("product list request received");

            var sort = SortOptions.Ascending;
            if (!string.IsNullOrEmpty(sortOption) && !Enum.TryParse(sortOption, true, out sort))
                return BadRequest();

            if (!string.IsNullOrEmpty(sortOption) && !Enum.IsDefined(typeof(SortOptions), sort))
                return Problem(statusCode: 400, detail: "Unsupported sort parameter");

            var result = await _productsProvider.GetProductsAsync(sort);

            if (!result.Success || result.Products == null)
                return NotFound();

            return Ok(result.Products);
        }
    }
}
