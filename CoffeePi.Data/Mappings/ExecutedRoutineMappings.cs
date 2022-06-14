using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;

namespace CoffeePi.Data.Mappings;

/// <summary>
/// Extension methods to convert Database Models to DataTransferObjects and vice-versa
/// </summary>
public static class ExecutedRoutineMappings
{
    public static ExecutedRoutineDto ToDto(this ExecutedRoutine execution) =>
        execution == default(ExecutedRoutine) ?
            default :
            new ExecutedRoutineDto
            {
                Id = execution.Id,
                RoutineId = execution.Routine.Id,
                Time = execution.Time,
                Success = execution.Success
            };

    public static ExecutedRoutine ToModel(this ExecutedRoutineDto dto, ExecutedRoutine routine = null)
    {
        routine ??= new();

        routine.Success = dto.Success;
        routine.Time = dto.Time;

        return routine;
    }
}
