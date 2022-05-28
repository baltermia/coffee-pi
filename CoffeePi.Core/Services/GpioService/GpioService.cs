using System.Device.Gpio;

namespace CoffeePi.Core.Services;

/// <summary>
/// Basic implementation of <see cref="IGpioService"/> for the raspberry pi
/// </summary>
public class GpioService : IGpioService // TODO: Test implementation on raspberry pi
{
    private readonly GpioController controller;

    public GpioService()
    {
        controller = new();
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
}
