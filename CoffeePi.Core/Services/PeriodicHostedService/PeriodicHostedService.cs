namespace CoffeePi.Core.Services;

public class PeriodicHostedService : BackgroundService, IPeriodicHostedService
{

    private readonly IServiceScopeFactory _factory;

    private readonly TimeSpan period = TimeSpan.FromSeconds(5);

    public bool Enabled { get; set; } = true;

    public PeriodicHostedService(IServiceScopeFactory factory)
    {
        _factory = factory;
    }

    protected async override Task ExecuteAsync(CancellationToken token)
    {
        using PeriodicTimer timer = new(period);

        while (!token.IsCancellationRequested && await timer.WaitForNextTickAsync(token))
        {
            if (!Enabled)
            {
                continue;
            }

            try
            {
                await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();

                IRoutineService routineService = asyncScope.ServiceProvider.GetRequiredService<IRoutineService>();

                await routineService.DoRoutineWorkAsync(token);
            }
            catch { } // TODO: Add exception logging (perhaps?)
        }
    }
}
