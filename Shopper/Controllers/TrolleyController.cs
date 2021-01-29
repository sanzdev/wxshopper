using Shopper.Providers.Contracts;
using Shopper.Providers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Shopper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrolleyController : ControllerBase
    {
        private readonly ILogger<TrolleyController> _logger;
        private readonly ITrolleyCalculationProvider _trolleyCalculationProvider;

        public TrolleyController(ILogger<TrolleyController> logger, ITrolleyCalculationProvider trolleyCalculationProvider)
        {
            _logger = logger;
            _trolleyCalculationProvider = trolleyCalculationProvider;
        }

        [HttpPost]
        [Route("TrolleyTotal")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TrolleyTotal([FromBody] TrolleyCalculationRequest trolley)
        {
            _logger.LogInformation("product list request received");

            if (trolley == null)
                return BadRequest();

            var total = await _trolleyCalculationProvider.GetTrolleyTotalAsync(trolley);
            
            return Ok(total.TrolleyTotal);
        }
    }
}
