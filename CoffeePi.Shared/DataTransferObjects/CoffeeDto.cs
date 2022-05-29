namespace CoffeePi.Shared.DataTransferObjects;

public class CoffeeDto
{
    public int Id { get; init; }
    public DateTime Time { get; set; }
    public int RepeatValue { get; set; } // ToDo: set values for Daily, weelky repeats
    public string CoffeeType { get; set; }
}
