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
    public class TrolleyTotalController : ControllerBase
    {
        private readonly ILogger<TrolleyTotalController> _logger;
        private readonly ITrolleyCalculationProvider _trolleyCalculationProvider;

        public TrolleyTotalController(ILogger<TrolleyTotalController> logger, ITrolleyCalculationProvider trolleyCalculationProvider)
        {
            _logger = logger;
            _trolleyCalculationProvider = trolleyCalculationProvider;
        }

        [HttpPost]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TrolleyTotal([FromBody] TrolleyCalculationRequest trolley)
        {
            _logger.LogInformation("trolley calculation request received");
            _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(trolley));

            if (trolley == null)
                return Problem(statusCode: 400, detail: "Invalid request body");

            var total = await _trolleyCalculationProvider.GetTrolleyTotalAsync(trolley);
            
            return Ok(total.TrolleyTotal);
        }
    }
}
