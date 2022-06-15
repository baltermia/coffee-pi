using CoffeePi.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePi.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IPeriodicHostedService _service;

        public AdminController(IPeriodicHostedService service)
        {
            _service = service;
        }

        [HttpGet("background")]
        public ActionResult<bool> GetBackgroundState()
        {
            try
            {
                bool state = _service.Enabled;

                return Ok(state);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting background-task state information");
            }
        }

        [HttpPatch("background/{state}")]
        public ActionResult PatchBackgroundState(bool state)
        {
            try
            {
                _service.Enabled = state;

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting background-task state information");
            }
        }
    }
}
