namespace CoffeePi.Shared.DataTransferObjects;

public class ExecutedRoutineDto : IDataTransferObject
{
    public int Id { get; set; }
    public int RoutineId { get; set; }
    public DateTime Time { get; set; }
    public bool Success { get; set; }
}
