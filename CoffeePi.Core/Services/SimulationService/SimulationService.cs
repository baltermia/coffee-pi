using CoffeePi.Shared.Enums;
using System.Net.Sockets;

namespace CoffeePi.Core.Services;

public class SimulationService : ISimulationService
{
    private readonly TcpClient _client;

    public SimulationService(TcpClient client)
    {
        _client = client;
    }

    public Task SendButtonPress(CoffeeButton button, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
