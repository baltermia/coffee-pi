using CoffeePi.Data.Configuration;
using CoffeePi.Data.Mappings;
using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace CoffeePi.Core.Repositories;

public interface ICoffeeRoutineRepository : IRepositoryBase<CoffeeRoutineDto> { }

public class CoffeeRoutineRepository : ICoffeeRoutineRepository
{
    private readonly CoffeePiContext _context;
    private readonly ISingleRoutineRepository _singleRepo;
    private readonly IWeeklyRoutineRepository _weeklyRepo;
    private readonly IDailyRoutineRepository _dailyRepo;

    public CoffeeRoutineRepository(CoffeePiContext context, ISingleRoutineRepository singleRepo, IWeeklyRoutineRepository weeklyRepo, IDailyRoutineRepository dailyRepo)
    {
        _context = context;
        _singleRepo = singleRepo;
        _weeklyRepo = weeklyRepo;
        _dailyRepo = dailyRepo;
    }

    public IEnumerable<CoffeeRoutineDto> FindAll() =>
        _context
            .Set<CoffeeRoutine>()
            .AsNoTracking()
            .Include(e => (e as SingleRoutine).Execution)
            .Include(e => (e as DailyRoutine).Executions)
            .Include(e => (e as WeeklyRoutine).Executions)
            .Select(CoffeeRoutineMappings.ToDto);

    public CoffeeRoutineDto FindById(int id) =>
        _context
            .Set<CoffeeRoutine>()
            .AsNoTracking()
            .Include(e => (e as SingleRoutine).Execution)
            .Include(e => (e as DailyRoutine).Executions)
            .Include(e => (e as WeeklyRoutine).Executions)
            .SingleOrDefault(e => e.Id == id)
            .ToDto();

    public async Task<CoffeeRoutineDto> CreateAsync(CoffeeRoutineDto dto) => dto switch
    {
        SingleRoutineDto single => await _singleRepo.CreateAsync(single),
        WeeklyRoutineDto weekly => await _weeklyRepo.CreateAsync(weekly),
        DailyRoutineDto daily => await _dailyRepo.CreateAsync(daily),
        _ => throw CoffeeRoutineMappings.DtoMappingNotFound
    };

    public Task UpdateAsync(CoffeeRoutineDto dto) => dto switch
    {
        SingleRoutineDto single => _singleRepo.UpdateAsync(single),
        WeeklyRoutineDto weekly => _weeklyRepo.UpdateAsync(weekly),
        DailyRoutineDto daily => _dailyRepo.UpdateAsync(daily),
        _ => throw CoffeeRoutineMappings.DtoMappingNotFound
    };
}
