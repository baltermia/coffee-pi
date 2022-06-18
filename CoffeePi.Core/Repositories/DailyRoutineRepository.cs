using CoffeePi.Data.Configuration;
using CoffeePi.Data.Mappings;
using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace CoffeePi.Core.Repositories;

public interface IDailyRoutineRepository : IRepositoryBase<DailyRoutineDto> { }

public class DailyRoutineRepository : IDailyRoutineRepository
{
    private readonly CoffeePiContext _context;

    public DailyRoutineRepository(CoffeePiContext context)
    {
        _context = context;
    }

    public IEnumerable<DailyRoutineDto> FindAll() =>
        _context
            .Set<CoffeeRoutine>()
            .OfType<DailyRoutine>()
            .AsNoTracking()
            .Include(e => e.Executions)
            .Select(CoffeeRoutineMappings.ToDto);

    public DailyRoutineDto FindById(int id) =>
        _context
            .Set<CoffeeRoutine>()
            .OfType<DailyRoutine>()
            .AsNoTracking()
            .Include(e => e.Executions)
            .SingleOrDefault(e => e.Id == id)
            .ToDto();

    public async Task<DailyRoutineDto> CreateAsync(DailyRoutineDto dto)
    {
        DailyRoutine routine = dto.ToModel();

        routine.Enabled = true;

        await _context.AddAsync<CoffeeRoutine>(routine);

        await _context.SaveChangesAsync();

        return routine.ToDto();
    }

    public async Task UpdateAsync(DailyRoutineDto dto)
    {
        DailyRoutine routine =
            _context
                .Set<CoffeeRoutine>()
                .OfType<DailyRoutine>()
                .Include(e => e.Executions)
                .Single(e => e.Id == dto.Id);

        routine = dto.ToModel(routine);

        _context.Update<CoffeeRoutine>(routine);

        await _context.SaveChangesAsync();
    }
}
