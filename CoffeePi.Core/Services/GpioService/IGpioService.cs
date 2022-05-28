namespace CoffeePi.Core.Services;

/// <summary>
/// Interface to interact with GPIO I/O
/// </summary>
public interface IGpioService
{
    /// <summary>
    /// Toggles the State of the given pin
    /// </summary>
    public void Toggle(CoffeeButtonPins pin);

    /// <summary>
    /// Sets the State of the given pin to <see cref="PinValue.High"/>
    /// </summary>
    public void Enable(CoffeeButtonPins pin);

    /// <summary>
    /// Sets the State of the given pin to <see cref="PinValue.Low"/>
    /// </summary>
    public void Disable(CoffeeButtonPins pin);
}
