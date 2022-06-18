using CoffeePi.Data.Configuration;
using CoffeePi.Data.Mappings;
using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace CoffeePi.Core.Repositories;

public interface IWeeklyRoutineRepository : IRepositoryBase<WeeklyRoutineDto> { }

public class WeeklyRoutineRepository : IWeeklyRoutineRepository
{
    private readonly CoffeePiContext _context;

    public WeeklyRoutineRepository(CoffeePiContext context)
    {
        _context = context;
    }

    public IEnumerable<WeeklyRoutineDto> FindAll() =>
        _context
            .Set<CoffeeRoutine>()
            .OfType<WeeklyRoutine>()
            .AsNoTracking()
            .Include(e => e.Executions)
            .Select(CoffeeRoutineMappings.ToDto);

    public WeeklyRoutineDto FindById(int id) =>
        _context
            .Set<CoffeeRoutine>()
            .OfType<WeeklyRoutine>()
            .AsNoTracking()
            .Include(e => e.Executions)
            .SingleOrDefault(e => e.Id == id)
            .ToDto();

    public async Task<WeeklyRoutineDto> CreateAsync(WeeklyRoutineDto dto)
    {
        WeeklyRoutine routine = dto.ToModel();

        routine.Enabled = true;
        
        await _context.AddAsync<CoffeeRoutine>(routine);

        await _context.SaveChangesAsync();

        return routine.ToDto();
    }

    public async Task UpdateAsync(WeeklyRoutineDto dto)
    {
        WeeklyRoutine routine =
            _context
                .Set<CoffeeRoutine>()
                .OfType<WeeklyRoutine>()
                .Include(e => e.Executions)
                .Single(e => e.Id == dto.Id);

        routine = dto.ToModel(routine);

        _context.Update<CoffeeRoutine>(routine);

        await _context.SaveChangesAsync();
    }
}
