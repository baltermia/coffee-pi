using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;

namespace CoffeePi.Data.Mappings;

/// <summary>
/// Extension methods to convert Database Models to DataTransferObjects and vice-versa
/// </summary>
public static class CoffeeRoutineMappings
{
    /// <summary>
    /// Exception stating the a mapping method to convert a given model to a dto could not be found.
    /// </summary>
    public static readonly ArgumentException DtoMappingNotFound = new($"CoffeeRoutine is not of any known type. Please add type to switch statement: {nameof(CoffeeRoutineMappings.ToDto)}");

    /// <summary>
    /// Exception stating the a mapping method to convert a given dto to a model could not be found.
    /// </summary>
    public static readonly ArgumentException ModelMappingNotFound = new($"CoffeeRoutineDto is not of any known type. Please add type to switch statement: {nameof(CoffeeRoutineMappings.ToModel)}");

    #region ToDto
    public static CoffeeRoutineDto ToDto(this CoffeeRoutine routine) => routine switch
    {
        SingleRoutine single => single.ToDto(),
        WeeklyRoutine weekly => weekly.ToDto(),
        DailyRoutine daily => daily.ToDto(),
        _ => throw DtoMappingNotFound
    };

    public static SingleRoutineDto ToDto(this SingleRoutine routine) =>
        routine == default(SingleRoutine) ?
            default :
            new SingleRoutineDto
            {
                Id = routine.Id,
                ButtonType = routine.ButtonType,
                Time = routine.Time,
                ExecutionId = routine.Execution?.Id ?? default
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
    #endregion

    #region ToModel
    public static CoffeeRoutine ToModel(this CoffeeRoutineDto dto, CoffeeRoutine routine = null) => dto switch
    {
        SingleRoutineDto single => single.ToModel(routine as SingleRoutine),
        WeeklyRoutineDto weekly => weekly.ToModel(routine as WeeklyRoutine),
        DailyRoutineDto daily => daily.ToModel(routine as DailyRoutine),
        _ => throw ModelMappingNotFound
    };

    public static SingleRoutine ToModel(this SingleRoutineDto dto, SingleRoutine routine = null)
    {
        routine ??= new();

        routine.ButtonType = dto.ButtonType;
        routine.Time = dto.Time;

        return routine;
    }

    public static WeeklyRoutine ToModel(this WeeklyRoutineDto dto, WeeklyRoutine routine = null)
    {
        routine ??= new();

        routine.ButtonType = dto.ButtonType;
        routine.TimeOfDay = dto.TimeOfDay;
        routine.DayOfWeek = dto.DayOfWeek;

        return routine;
    }

    public static DailyRoutine ToModel(this DailyRoutineDto dto, DailyRoutine routine = null)
    {
        routine ??= new();

        routine.ButtonType = dto.ButtonType;
        routine.TimeOfDay = dto.TimeOfDay;
        routine.DaysOfWeek = dto.DaysOfWeek;

        return routine;
    }
    #endregion
}
