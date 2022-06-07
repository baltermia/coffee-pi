using CoffeePi.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoffeePi.Data.Models;

public abstract class CoffeeRoutine
{
    public int Id { get; set; }

    [Required]
    public CoffeeButton ButtonType { get; set; }
}

public class WeeklyRoutine : CoffeeRoutine
{
    [Required]
    public TimeOnly TimeOfDay { get; set; }

    [Required]
    public DayOfWeek DayOfWeek { get; set; }

    public ICollection<ExecutedRoutine> Executions { get; set; }
}

public class DailyRoutine : CoffeeRoutine
{
    [Required]
    public TimeOnly TimeOfDay { get; set; }

    [Required]
    public ICollection<DayOfWeek> DaysOfWeek { get; set; }

    public ICollection<ExecutedRoutine> Executions { get; set; }
}

public class SingleRoutine : CoffeeRoutine
{
    [Required]
    public DateTime Time { get; set; }

    public ExecutedRoutine Execution { get; set; }
}
