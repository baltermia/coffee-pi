using CoffeePi.Data.Models;

namespace CoffeePi.Core.Services;

public interface IRoutineService
{
    public Task CheckAllRoutinesAsync(CancellationToken token = default);
    public Task ExecuteRoutineAsync(CoffeeRoutine routine, CancellationToken token = default);
}
