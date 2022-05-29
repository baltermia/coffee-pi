namespace CoffeePi.Shared.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

public class Coffee
{
    public int Id { get; init; }
    public DateTime TimeExecuted { get; set; }
    [Required]
    public string CoffeeType { get; set; }
    public int? PlannedRoutineId { get; set; }
}
