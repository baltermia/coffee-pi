using CoffeePi.Shared.Enums;
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

        foreach (CoffeeButton pin in Enum.GetValues<CoffeeButton>())
        {
            controller.OpenPin((int)pin, PinMode.Output, PinValue.Low);
        }
    }

    public void Toggle(CoffeeButton pin)
    {
        int pinNum = (int)pin;

        PinValue currentValue = controller.Read(pinNum);

        PinValue newValue =
            currentValue == PinValue.Low ?
                PinValue.High :
                PinValue.Low;

        controller.Write(pinNum, newValue);
    }

    public void Enable(CoffeeButton pin) =>
        controller.Write((int)pin, PinValue.High);

    public void Disable(CoffeeButton pin) =>
        controller.Write((int)pin, PinValue.Low);

    public async Task SimulatePress(CoffeeButton pin, int delay = 300)
    {
        Enable(pin);

        await Task.Delay(delay);

        Disable(pin);
    }

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
