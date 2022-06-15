using CoffeePi.Core.Repositories;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePi.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoutineController : Controller
{
    private readonly ICoffeeRoutineRepository _repo;

    public RoutineController(ICoffeeRoutineRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CoffeeRoutineDto>> Get()
    {
        try
        {
            IEnumerable<CoffeeRoutineDto> routines = _repo.FindAll();

            return Ok(routines);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting the entity from database");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<CoffeeRoutineDto> Get(int id)
    {
        try
        {
            CoffeeRoutineDto routine = _repo.FindById(id);

            return Ok(routine);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting the entity from database");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CoffeeRoutineDto>> Create(CoffeeRoutineDto routine)
    {
        try
        {
            CoffeeRoutineDto created = await _repo.CreateAsync(routine);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the entity in the database");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CoffeeRoutineDto>> Update(int id, CoffeeRoutineDto routine)
    {
        if (id != routine?.Id)
        {
            return BadRequest("Id doesn't match id of entity");
        }

        try
        {
            if (_repo.FindById(id) == default(CoffeeRoutineDto))
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
