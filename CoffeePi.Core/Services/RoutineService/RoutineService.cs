using CoffeePi.Data.Configuration;
using CoffeePi.Data.Models;

namespace CoffeePi.Core.Services;

public class RoutineService : IRoutineService
{
    private readonly CoffeePiContext _context;
    private readonly IGpioService _gpio;

    public RoutineService(CoffeePiContext context, IGpioService gpio)
    {
        _context = context;
        _gpio = gpio;
    }

    public async Task CheckAllRoutinesAsync(CancellationToken token = default)
    {
        IEnumerable<CoffeeRoutine> openRoutines =
            _context
                .Set<CoffeeRoutine>()
                .Where(CoffeeRoutineCheckExtensions.ShouldRoutineBeExecuted);

        foreach (CoffeeRoutine routine in openRoutines)
        {
            if (token.IsCancellationRequested)
            {
                break;
            }

            await ExecuteRoutineAsync(routine, false, token);
        }

        await _context.SaveChangesAsync(token);
    }

    public async Task ExecuteRoutineAsync(CoffeeRoutine routine, bool saveDbChanges = true, CancellationToken token = default)
    {
        if (!routine.ShouldRoutineBeExecuted())
        {
            return;
        }

        bool success = true;

        try
        {
            await _gpio.SimulatePressAsync(routine.ButtonType, token: token);
        }
        catch
        {
            success = false;
        }


        ExecutedRoutine execution = new()
        {
            Routine = routine,
            Success = success,
            Time = DateTime.Now
        };

        await _context.AddAsync(execution, token);

        if (saveDbChanges)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}

/// <summary>
/// Methods to check wether a routine was executed already or not
/// </summary>
public static class CoffeeRoutineCheckExtensions
{
    public static bool ShouldRoutineBeExecuted(this CoffeeRoutine routine) => routine switch
    {
        SingleRoutine single => single.ShouldRoutineBeExecuted(),
        DailyRoutine daily => daily.ShouldRoutineBeExecuted(),
        WeeklyRoutine weekly => weekly.ShouldRoutineBeExecuted(),
        _ => throw new ArgumentException($"CoffeeRoutine is not of any known type. Please add type to switch statement: {nameof(CoffeeRoutineCheckExtensions.ShouldRoutineBeExecuted)}")
    };

    public static bool ShouldRoutineBeExecuted(this SingleRoutine single) =>
        single.Enabled &&
        single.Execution == default(ExecutedRoutine) &&
        single.Time < DateTime.Now;

    public static bool ShouldRoutineBeExecuted(this WeeklyRoutine weekly) =>
        weekly.Enabled &&
        weekly.DayOfWeek == DateTime.Now.DayOfWeek &&
        !weekly.Executions.Any(e => e.Time.Date == DateTime.Today) &&
        weekly.TimeOfDay.ToTimeSpan() < DateTime.Now.TimeOfDay;

    public static bool ShouldRoutineBeExecuted(this DailyRoutine daily) =>
        daily.Enabled &&
        daily.DaysOfWeek.Contains(DateTime.Now.DayOfWeek) &&
        !daily.Executions.Any(e => e.Time.Date == DateTime.Today) &&
        daily.TimeOfDay.ToTimeSpan() < DateTime.Now.TimeOfDay;
}
