using CoffeePi.Data.Configuration;
using CoffeePi.Data.Mappings;
using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace CoffeePi.Core.Repositories;

public interface ISingleRoutineRepository : IRepositoryBase<SingleRoutineDto> { }

public class SingleRoutineRepository : ISingleRoutineRepository
{
    private readonly CoffeePiContext _context;

    public SingleRoutineRepository(CoffeePiContext context)
    {
        _context = context;
    }

    public IEnumerable<SingleRoutineDto> FindAll() =>
        _context
            .Set<CoffeeRoutine>()
            .OfType<SingleRoutine>()
            .AsNoTracking()
            .Include(e => e.Execution)
            .Select(CoffeeRoutineMappings.ToDto);

    public SingleRoutineDto FindById(int id) =>
        _context
            .Set<CoffeeRoutine>()
            .OfType<SingleRoutine>()
            .AsNoTracking()
            .Include(e => e.Execution)
            .SingleOrDefault(e => e.Id == id)
            .ToDto();

    public async Task<SingleRoutineDto> CreateAsync(SingleRoutineDto dto)
    {
        SingleRoutine routine = dto.ToModel();

        routine.Enabled = true;

        await _context.AddAsync<CoffeeRoutine>(routine);

        await _context.SaveChangesAsync();

        return routine.ToDto();
    }

    public async Task UpdateAsync(SingleRoutineDto dto)
    {
        SingleRoutine routine =
            _context
                .Set<CoffeeRoutine>()
                .OfType<SingleRoutine>()
                .Include(e => e.Execution)
                .Single(e => e.Id == dto.Id);

        routine = dto.ToModel(routine);

        _context.Update<CoffeeRoutine>(routine);

        await _context.SaveChangesAsync();
    }
}
