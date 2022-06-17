using CoffeePi.Shared.Enums;

namespace CoffeePi.Shared.DataTransferObjects;

[Serializable]
public class TcpSocketButtonDto
{
    public CoffeeButton Button { get; set; }
    public bool State { get; set; }
}
