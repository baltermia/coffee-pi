namespace CoffeePi.Shared.DataTransferObjects;

public class MachinePropertiesDto : IDataTransferObject
{
    public int Id { get; set; }

    public bool Running { get; set; }

    public decimal BeanStatus { get; set; }

    public decimal WaterLevel { get; set; }
}
