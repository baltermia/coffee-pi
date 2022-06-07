using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;

namespace CoffeePi.Data.Mappings;

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
}
