using System.Device.Gpio;

namespace CoffeePi.Core.Services;

/// <summary>
/// Basic implementation of <see cref="IGpioService"/> for the raspberry pi
/// </summary>
public class GpioService : IGpioService // TODO: Test implementation on raspberry pi
{
    private bool disposed = false;
    private readonly GpioController controller;

    public GpioService()
    {
        controller = new();

        foreach (CoffeeButtonPins pin in Enum.GetValues<CoffeeButtonPins>())
        {
            controller.OpenPin((int)pin, PinMode.Output, PinValue.Low);
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
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (disposed)
        {
            return;
        }

        if (isDisposing)
        {
            controller.Dispose();
        }

        disposed = true;
    }
}
