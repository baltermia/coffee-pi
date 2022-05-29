using CoffeePi.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoffeePi.Shared.Models;

/// <summary>
/// Model representing a planned routine for a button press
/// </summary>
public class PlannedRoutine
{
    public int Id { get; init; }

    [Required]
    public CoffeeButton Type { get; set; }

    [Required]
    public DateTime Time { get; set; }

    public bool? Successful = null;
}
