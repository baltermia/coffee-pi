using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;

namespace CoffeePi.Data.Mappings;

public static class CoffeeRoutineMappings
{
    public static CoffeeRoutineDto ToDto(this CoffeeRoutine routine) => routine switch
    {
        SingleRoutine single => single.ToDto(),
        WeeklyRoutine weekly => weekly.ToDto(),
        DailyRoutine daily => daily.ToDto(),
        _ => throw new ArgumentException($"CoffeeRoutine is not of any known type. Please add type to switch statement: {nameof(CoffeeRoutineMappings.ToDto)}")
    };

    public static SingleRoutineDto ToDto(this SingleRoutine routine) =>
        routine == default(SingleRoutine) ?
            default :
            new SingleRoutineDto
            {
                Id = routine.Id,
                ButtonType = routine.ButtonType,
                Time = routine.Time,
                ExecutionId = routine.Execution.Id
            };

    public static WeeklyRoutineDto ToDto(this WeeklyRoutine routine) =>
        routine == default(WeeklyRoutine) ?
            default :
            new WeeklyRoutineDto
            {
                Id = routine.Id,
                ButtonType = routine.ButtonType,
                DayOfWeek = routine.DayOfWeek,
                TimeOfDay = routine.TimeOfDay,
                ExecutionIds = routine.Executions.Select(e => e.Id).ToList()
            };

    public static DailyRoutineDto ToDto(this DailyRoutine routine) =>
        routine == default(DailyRoutine) ?
            default :
            new DailyRoutineDto
            {
                Id = routine.Id,
                ButtonType = routine.ButtonType,
                DaysOfWeek = routine.DaysOfWeek,
                TimeOfDay = routine.TimeOfDay,
                ExecutionIds = routine.Executions.Select(e => e.Id).ToList()
            };
}
