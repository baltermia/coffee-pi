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
                Id = properties.Id,
                Running = properties.Running,
                BeanStatus = properties.BeanStatus,
                WaterLevel = properties.WaterLevel
            };

    public static async Task<MachinePropertiesDto> ToDtoAsync(this Task<MachineProperties> task)
    {
        MachineProperties properties = await task;

        if (properties == default(MachineProperties))
            return default;
        
        return new MachinePropertiesDto
        {
            Id = properties.Id,
            Running = properties.Running,
            BeanStatus = properties.BeanStatus,
            WaterLevel = properties.WaterLevel
        };
    }
}
