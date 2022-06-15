using CoffeePi.Core.Repositories;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePi.Core.Controllers.Routine;

[Route("api/[controller]")]
[ApiController]
public class SingleController : Controller
{
    private readonly ISingleRoutineRepository _repo;

    public SingleController(ISingleRoutineRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SingleRoutineDto>> Get()
    {
        try
        {
            IEnumerable<SingleRoutineDto> routines = _repo.FindAll();

            return Ok(routines);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting the entity from database");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<SingleRoutineDto> Get(int id)
    {
        try
        {
            SingleRoutineDto routine = _repo.FindById(id);

            return Ok(routine);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting the entity from database");
        }
    }

    [HttpPost]
    public async Task<ActionResult<SingleRoutineDto>> Create(SingleRoutineDto routine)
    {
        try
        {
            SingleRoutineDto created = await _repo.CreateAsync(routine);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the entity in the database");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SingleRoutineDto>> Update(int id, SingleRoutineDto routine)
    {
        if (id != routine?.Id)
        {
            return BadRequest("Id doesn't match id of entity");
        }

        try
        {
            if (_repo.FindById(id) == default(SingleRoutineDto))
            {
                return NotFound(id);
            }

            await _repo.UpdateAsync(routine);

            return NoContent();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the entity in the database");
        }
    }
}
