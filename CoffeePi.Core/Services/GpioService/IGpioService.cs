using CoffeePi.Shared.Enums;

namespace CoffeePi.Core.Services;

/// <summary>
/// Interface to interact with GPIO I/O
/// </summary>
public interface IGpioService : IDisposable
{
    /// <summary>
    /// Toggles the State of the given pin
    /// </summary>
    public void Toggle(CoffeeButton pin);

    /// <summary>
    /// Sets the State of the given pin to <see cref="PinValue.High"/>
    /// </summary>
    public void Enable(CoffeeButton pin);

    /// <summary>
    /// Sets the State of the given pin to <see cref="PinValue.Low"/>
    /// </summary>
    public void Disable(CoffeeButton pin);

    /// <summary>
    /// Enables the given pin for some (real-) time to simulate a button press
    /// </summary>
    /// <param name="delay">The delay between the ON/OFF state of the IO pin in milliseconds</param>
    public Task SimulatePress(CoffeeButton pin, int delay = 300);
}
