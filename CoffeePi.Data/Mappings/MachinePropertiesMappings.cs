using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;

namespace CoffeePi.Data.Mappings;

public static class MachinePropertiesMappings
{
    public static MachinePropertiesDto ToDto(this MachineProperties properties) =>
        properties == default(MachineProperties) ?
            default :
            new MachinePropertiesDto
            {
                Running = properties.Running,
                BeanStatus = properties.BeanStatus,
                WaterLevel = properties.WaterLevel
            };
}
