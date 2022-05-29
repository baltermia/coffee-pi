namespace CoffeePi.Shared.Enums;

/// <summary>
/// Enum representing all buttons that can be simulated from the raspberry pi
/// 
/// The int Values represent the GPIO pin on the raspberry pi
/// </summary>
public enum CoffeeButton
{
    SmallCup = 11,
    BigCup = 17,
    HotWater = 20,
    Espresso = 26
}
