namespace CoffeePi.Core.Services;

public interface IPeriodicHostedService : IHostedService
{
    public bool Enabled { get; set; }
}
