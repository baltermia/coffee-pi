using System.Device.Gpio;

namespace CoffeePi.Core.Services;

/// <summary>
/// Basic implementation of <see cref="IGpioService"/> for the raspberry pi
/// </summary>
public sealed class GpioService : IGpioService // TODO: Test implementation on raspberry pi
{
    private readonly GpioController controller;

    public GpioService()
    {
        controller = new();

        foreach (CoffeeButtonPins pin in Enum.GetValues<CoffeeButtonPins>())
        {
            controller.OpenPin((int)pin, PinMode.Output);
        }
    }

    public void Toggle(CoffeeButtonPins pin)
    {
        int pinNum = (int)pin;

        PinValue currentValue = controller.Read(pinNum);

        PinValue newValue =
            currentValue == PinValue.Low ?
                PinValue.High :
                PinValue.Low;

        controller.Write(pinNum, newValue);
    }

    public void Enable(CoffeeButtonPins pin) =>
        controller.Write((int)pin, PinValue.High);

    public void Disable(CoffeeButtonPins pin) =>
        controller.Write((int)pin, PinValue.Low);


    public void Dispose()
    {
        foreach (CoffeeButtonPins pin in Enum.GetValues<CoffeeButtonPins>())
        {
            controller.ClosePin((int)pin);
        }

        controller.Dispose();
    }
}
