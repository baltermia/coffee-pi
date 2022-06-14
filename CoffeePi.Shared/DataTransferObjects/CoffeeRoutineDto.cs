using CoffeePi.Shared.Enums;

namespace CoffeePi.Shared.DataTransferObjects;

public abstract class CoffeeRoutineDto : IDataTransferObject
{
    public int Id { get; set; }
    public CoffeeButton ButtonType { get; set; }
}

public class WeeklyRoutineDto : CoffeeRoutineDto
{
    public TimeOnly TimeOfDay { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public ICollection<int> ExecutionIds { get; set; }
}

public class DailyRoutineDto : CoffeeRoutineDto
{
    public TimeOnly TimeOfDay { get; set; }
    public ICollection<DayOfWeek> DaysOfWeek { get; set; }
    public ICollection<int> ExecutionIds { get; set; }
}

public class SingleRoutineDto : CoffeeRoutineDto
{
    public DateTime Time { get; set; }
    public int ExecutionId { get; set; }
}
