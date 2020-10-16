using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArtistSearch.API.Controllers
{

    [ApiController]
    public class ErrorController : ControllerBase
    {
        ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            _logger.LogError(exception.Error, "An exception occurred.");
            return Problem();
        }
    }
}
