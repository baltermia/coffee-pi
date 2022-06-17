using CoffeePi.Shared.DataTransferObjects;
using CoffeePi.Shared.Enums;
using System.Net.Sockets;
using System.Xml.Serialization;

namespace CoffeePi.Core.Services;

public class SimulationService : ISimulationService, IDisposable
{
    private bool disposed = false;

    private readonly TcpClient _client;

    public SimulationService(TcpClient client)
    {
        _client = client;
    }

    public async Task SendButtonPress(CoffeeButton button, int delay = 300, CancellationToken token = default)
    {
        TcpSocketButtonDto dto = new()
        {
            Button = button
        };

        using NetworkStream stream = _client.GetStream();
        XmlSerializer serializer = new(typeof(TcpSocketButtonDto));

        dto.State = true;

        serializer.Serialize(stream, dto);

        await stream.FlushAsync(token);

        await Task.Delay(delay, token);

        dto.State = false;

        serializer.Serialize(stream, dto);

        await stream.FlushAsync(token);

        stream.Close();
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (disposed)
        {
            return;
        }
        if (isDisposing)
        {
            _client?.Dispose();
        }

        disposed = true;
    }
}
