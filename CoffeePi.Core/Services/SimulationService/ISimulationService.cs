using CoffeePi.Shared.Enums;

namespace CoffeePi.Core.Services;

public interface ISimulationService
{
    /// <summary>
    /// Simulates a Button press on the WPF project
    /// </summary>/param>
    public Task SendButtonPress(CoffeeButton button, int delay = 300, CancellationToken token = default);
}
