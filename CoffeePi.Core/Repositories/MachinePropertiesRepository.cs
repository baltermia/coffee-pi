using CoffeePi.Data.Configuration;
using CoffeePi.Data.Mappings;
using CoffeePi.Data.Models;
using CoffeePi.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace CoffeePi.Core.Repositories;

public interface IMachinePropertiesRepository
{
    public Task<MachinePropertiesDto> GetPropertiesAsync();

    public Task SetRunningStateAsync(bool state);

    public Task RefillWaterAsync();

    public Task RefillBeansAsync();
}

public class MachinePropertiesRepository : IMachinePropertiesRepository
{
    private readonly CoffeePiContext _context;

    public MachinePropertiesRepository(CoffeePiContext context) => _context = context;

    public Task<MachinePropertiesDto> GetPropertiesAsync() =>
        _context
            .Set<MachineProperties>()
            .AsNoTracking()
            .SingleAsync()
            .ToDtoAsync();

    public Task SetRunningStateAsync(bool state) =>
        ApplyChanges(props => props.Running = state);

    public Task RefillBeansAsync() =>
        ApplyChanges(props => props.BeanStatus = MachineProperties.FullBeanStatus);

    public Task RefillWaterAsync() =>
        ApplyChanges(props => props.WaterLevel = MachineProperties.FullWaterStatus);

    private Task ApplyChanges(Action<MachineProperties> changeAction)
    {
        MachineProperties props = _context.Set<MachineProperties>().Single();

        changeAction(props);

        _context.Update(props);

        return _context.SaveChangesAsync();
    }
}
