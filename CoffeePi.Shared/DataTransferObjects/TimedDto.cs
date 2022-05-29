using System;

namespace CoffeePi.Shared.DataTransferObjects;

public class TimedDto
{
    public int Id { get; init; }
    public DateTime Time { get; set; }
    public bool Repeating  { get; set; }
}
