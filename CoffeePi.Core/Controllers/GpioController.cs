using CoffeePi.Core.Services;
using CoffeePi.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePi.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GpioController : Controller
    {
        private readonly IGpioService _gpioService;

        public GpioController(IGpioService gpioService)
        {
            _gpioService = gpioService;
        }

        [HttpPost("simulate")]
        public ActionResult SimulateButton(CoffeeButton button)
        {
            try
            {
                _gpioService.SimulatePressAsync(button);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error simulating button press");
            }
        }
    }
}
