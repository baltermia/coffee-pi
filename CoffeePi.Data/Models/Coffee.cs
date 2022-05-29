using CoffeePi.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoffeePi.Data.Models;

public class Coffee
{
    public int Id { get; init; }
    public DateTime TimeExecuted { get; set; }
    [Required]
    public CoffeeButton Type { get; set; }
    public int? PlannedRoutineId { get; set; }
}
