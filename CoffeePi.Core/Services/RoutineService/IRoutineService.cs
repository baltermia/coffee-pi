namespace CoffeePi.Core.Services;

public interface IRoutineService
{
    public Task DoRoutineWorkAsync(CancellationToken token);
}
