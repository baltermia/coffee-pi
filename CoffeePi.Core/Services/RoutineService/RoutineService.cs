namespace CoffeePi.Core.Services;

public class RoutineService : IRoutineService
{
    public async Task DoRoutineWorkAsync(CancellationToken token)
    {
         // TODO: Remove test code

        await Task.CompletedTask;

        Console.WriteLine("Running...");
    }
}
