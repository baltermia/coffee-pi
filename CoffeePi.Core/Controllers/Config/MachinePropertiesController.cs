using CoffeePi.Core.Repositories;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePi.Core.Controllers.Config;

[Route("api/[controller]")]
[ApiController]
public class MachinePropertiesController : Controller
{
    private readonly IMachinePropertiesRepository _repo;

    public MachinePropertiesController(IMachinePropertiesRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<MachinePropertiesDto>> GetMachineProperties()
    {
        try
        {
            MachinePropertiesDto props = await _repo.GetPropertiesAsync();

            return Ok(props);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting machine properties");
        }
    }

    [HttpPatch("running/{state}")]
    public async Task<ActionResult> PatchRunningState(bool state)
    {
        try
        {
            await _repo.SetRunningStateAsync(state);

            return NoContent();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error setting machine running property");
        }
    }

    [HttpPatch("refill-beans")]
    public async Task<ActionResult> PatchRefillBeans()
    {
        try
        {
            await _repo.RefillBeansAsync();

            return NoContent();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error refilling beans in machine properties");
        }
    }

    [HttpPatch("refill-water")]
    public async Task<ActionResult> PatchRefillWater()
    {
        try
        {
            await _repo.RefillWaterAsync();

            return NoContent();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error refilling water in machine properties");
        }
    }
}
