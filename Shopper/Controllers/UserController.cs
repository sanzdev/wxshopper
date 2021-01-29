using Shopper.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shopper.Models;

namespace Shopper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserDataProvider _userDataProvider;

        public UserController(ILogger<UserController> logger, IUserDataProvider userDataProvider)
        {
            _logger = logger;
            _userDataProvider = userDataProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserTokenData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserTokenData()
        {
            _logger.LogInformation("Token request received");

            var tokenResponse = await _userDataProvider.GetUserTokenDataAsync();

            if (tokenResponse.UserTokenData == null)
                return NotFound();

            return Ok(tokenResponse.UserTokenData);
        }
    }
}
