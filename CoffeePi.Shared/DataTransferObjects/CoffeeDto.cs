using CoffeePi.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoffeePi.Shared.DataTransferObjects;

public class CoffeeDto
{
    public int Id { get; init; }
    public DateTime TimeExecuted { get; set; }
    [Required]
    public CoffeeButton Type { get; set; }
}
