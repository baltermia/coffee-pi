using CoffeePi.Core.Repositories;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePi.Core.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExecutedController : Controller
    {
        private readonly IExecutedRoutineRepository _repo;

        public ExecutedController(IExecutedRoutineRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExecutedRoutineDto>> Get()
        {
            try
            {
                IEnumerable<ExecutedRoutineDto> routines = _repo.FindAll();

                return Ok(routines);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting the entity from database");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ExecutedRoutineDto> Get(int id)
        {
            try
            {
                ExecutedRoutineDto routine = _repo.FindById(id);

                return Ok(routine);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting the entity from database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExecutedRoutineDto>> Create(ExecutedRoutineDto routine)
        {
            try
            {
                ExecutedRoutineDto created = await _repo.CreateAsync(routine);

                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating the entity in the database");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExecutedRoutineDto>> Update(int id, ExecutedRoutineDto routine)
        {
            if (id != routine?.Id)
            {
                return BadRequest("Id doesn't match id of entity");
            }

            try
            {
                if (_repo.FindById(id) == default(ExecutedRoutineDto))
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
}
