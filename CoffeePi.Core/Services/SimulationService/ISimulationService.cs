using CoffeePi.Shared.Enums;

namespace CoffeePi.Core.Services;

public interface ISimulationService
{
    public Task SendButtonPress(CoffeeButton button, CancellationToken token = default);
}
