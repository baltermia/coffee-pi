using System.ComponentModel.DataAnnotations;

namespace CoffeePi.Data.Models;

public class ExecutedRoutine
{
    public int Id { get; set; }

    [Required]
    public CoffeeRoutine Routine { get; set; }

    [Required]
    public DateTime Time { get; set; }

    [Required]
    public bool Success { get; set; }
}
