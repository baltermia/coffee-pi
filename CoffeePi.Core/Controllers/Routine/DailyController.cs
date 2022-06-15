using CoffeePi.Core.Repositories;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePi.Core.Controllers.Routine;

[Route("api/[controller]")]
[ApiController]
public class DailyController : Controller
{
    private readonly IDailyRoutineRepository _repo;

    public DailyController(IDailyRoutineRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DailyRoutineDto>> Get()
    {
        try
        {
            IEnumerable<DailyRoutineDto> routines = _repo.FindAll();

            return Ok(routines);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting the entity from database");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<DailyRoutineDto> Get(int id)
    {
        try
        {
            DailyRoutineDto routine = _repo.FindById(id);

            return Ok(routine);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting the entity from database");
        }
    }

    [HttpPost]
    public async Task<ActionResult<DailyRoutineDto>> Create(DailyRoutineDto routine)
    {
        try
        {
            DailyRoutineDto created = await _repo.CreateAsync(routine);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the entity in the database");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DailyRoutineDto>> Update(int id, DailyRoutineDto routine)
    {
        if (id != routine?.Id)
        {
            return BadRequest("Id doesn't match id of entity");
        }

        try
        {
            if (_repo.FindById(id) == default(DailyRoutineDto))
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
