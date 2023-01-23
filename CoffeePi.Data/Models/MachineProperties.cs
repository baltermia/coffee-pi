using Microsoft.EntityFrameworkCore;

namespace CoffeePi.Data.Models;

public class MachineProperties
{
    public int Id { get; set; }

    public static readonly decimal FullBeanStatus = 1.0M;

    public static readonly decimal FullWaterStatus = 1.0M;

    public bool Running { get; set; } = false;

    public decimal BeanStatus { get; set; } = FullBeanStatus;

    public decimal WaterLevel { get; set; } = FullWaterStatus;
}
