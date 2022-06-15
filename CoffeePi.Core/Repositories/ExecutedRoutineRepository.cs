using CoffeePi.Data.Configuration;
using CoffeePi.Data.Mappings;
using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace CoffeePi.Core.Repositories;

public interface IExecutedRoutineRepository : IRepositoryBase<ExecutedRoutineDto> 
{
    public IEnumerable<ExecutedRoutineDto> FindFromRoutineId(int id);
}

public class ExecutedRoutineRepository : IExecutedRoutineRepository
{
    private readonly CoffeePiContext _context;

    public ExecutedRoutineRepository(CoffeePiContext context)
    {
        _context = context;
    }

    public IEnumerable<ExecutedRoutineDto> FindAll() =>
        _context
            .Set<ExecutedRoutine>()
            .AsNoTracking()
            .Select(ExecutedRoutineMappings.ToDto);

    public ExecutedRoutineDto FindById(int id) =>
        _context
            .Set<ExecutedRoutine>()
            .AsNoTracking()
            .SingleOrDefault(e => e.Id == id)
            .ToDto();

    public IEnumerable<ExecutedRoutineDto> FindFromRoutineId(int id) =>
        _context
            .Set<ExecutedRoutine>()
            .AsNoTracking()
            .Where(e => e.Routine.Id == id)
            .Select(ExecutedRoutineMappings.ToDto);

    public async Task<ExecutedRoutineDto> CreateAsync(ExecutedRoutineDto dto)
    {
        ExecutedRoutine routine = dto.ToModel();

        await _context.AddAsync(routine);

        await _context.SaveChangesAsync();

        return routine.ToDto();
    }

    public async Task UpdateAsync(ExecutedRoutineDto dto)
    {
        ExecutedRoutine routine =
            _context
                .Set<ExecutedRoutine>()
                .Single(e => e.Id == dto.Id);

        routine = dto.ToModel(routine);

        _context.Update(routine);

        await _context.SaveChangesAsync();
    }
}
