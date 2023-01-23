using Microsoft.EntityFrameworkCore;

namespace CoffeePi.Data.Models;

[Keyless]
public class MachineProperties
{
    public static readonly decimal FullBeanStatus = 1.0M;

    public static readonly decimal FullWaterStatus = 1.0M;

    public bool Running { get; set; } = false;

    public decimal BeanStatus { get; set; } = FullBeanStatus;

    public decimal WaterLevel { get; set; } = FullWaterStatus;
}
