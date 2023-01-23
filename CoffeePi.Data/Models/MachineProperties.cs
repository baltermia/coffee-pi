namespace CoffeePi.Data.Models;

public class MachineProperties
{
    public bool Running { get; set; } = false;

    public decimal BeanStatus { get; set; } = 1.0M;

    public decimal WaterLevel { get; set; } = 1.0M;
}
