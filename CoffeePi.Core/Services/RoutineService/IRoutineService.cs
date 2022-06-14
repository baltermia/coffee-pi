namespace CoffeePi.Core.Services;

public interface IRoutineService
{
    public Task CheckAllRoutinesAsync(CancellationToken token = default);
}
