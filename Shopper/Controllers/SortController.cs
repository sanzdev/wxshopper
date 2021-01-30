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
    public class SortController : ControllerBase
    {
        private readonly ILogger<SortController> _logger;
        private readonly IProductsProvider _productsProvider;

        public SortController(ILogger<SortController> logger, IProductsProvider productsProvider)
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

            if (string.IsNullOrEmpty(sortOption))
                return BadRequest(new { title = "Bad Request", status = 400, message = "Accepted sort options are Low, High, Ascending, Descending and Recommended",  });

            if (!Enum.TryParse(sortOption, true, out SortOptions sort))
                return BadRequest();

            if (!Enum.IsDefined(typeof(SortOptions), sort))
                return Problem(statusCode: 400, detail: "Accepted sort options are Low, High, Ascending, Descending and Recommended");

            var result = await _productsProvider.GetProductsAsync(sort);

            if (!result.Success || result.Products == null)
                return NotFound();

            return Ok(result.Products);
        }
    }
}
