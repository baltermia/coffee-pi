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

    public static CoffeeRoutine ToModel(this CoffeeRoutineDto dto, CoffeeRoutine routine = null) => dto switch
    {
        SingleRoutineDto single => single.ToModel(routine as SingleRoutine),
        WeeklyRoutineDto weekly => weekly.ToModel(routine as WeeklyRoutine),
        DailyRoutineDto daily => daily.ToModel(routine as DailyRoutine),
        _ => throw new ArgumentException($"CoffeeRoutineDto is not of any known type. Please add type to switch statement: {nameof(CoffeeRoutineMappings.ToModel)}")
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

    public static SingleRoutine ToModel(this SingleRoutineDto dto, SingleRoutine routine = null)
    {
        routine ??= new();

        routine.ButtonType = dto.ButtonType;
        routine.Time = dto.Time;

        return routine;
    }

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

    public static WeeklyRoutine ToModel(this WeeklyRoutineDto dto, WeeklyRoutine routine = null)
    {
        routine ??= new();

        routine.ButtonType = dto.ButtonType;
        routine.TimeOfDay = dto.TimeOfDay;
        routine.DayOfWeek = dto.DayOfWeek;

        return routine;
    }

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

    public static DailyRoutine ToModel(this DailyRoutineDto dto, DailyRoutine routine = null)
    {
        routine ??= new();

        routine.ButtonType = dto.ButtonType;
        routine.TimeOfDay = dto.TimeOfDay;
        routine.DaysOfWeek = dto.DaysOfWeek;

        return routine;
    }
}
