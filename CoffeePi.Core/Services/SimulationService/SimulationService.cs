using CoffeePi.Shared.Enums;
using System.Net.Sockets;
using System.Text.Json;

namespace CoffeePi.Core.Services;

public class SimulationService : ISimulationService
{
    private readonly string IP;
    private readonly int Port;

    public SimulationService(string ip, int port)
    {
        IP = ip;
        Port = port;
    }

    public async Task SendButtonPress(CoffeeButton button, int delay = 300, CancellationToken token = default)
    {
        using TcpClient client = new(IP, Port);
        using NetworkStream senderStream = client.GetStream();

        JsonSerializer.Serialize(senderStream, button);

        await senderStream.FlushAsync(token);
    }
}
