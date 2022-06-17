using CoffeePi.Shared.Enums;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeePi.Simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CancellationTokenSource source;
        private CancellationToken Token => source.Token;

        private readonly string IP = "127.0.0.1";
        private readonly int Port = 1302;

        public MainWindow()
        {
            InitializeComponent();

            source = new();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ListenAsync();
        }

        private async Task ListenAsync()
        {
            TcpListener listener = new(IPAddress.Parse(IP), Port);

            listener.Start();
            
            while (!Token.IsCancellationRequested)
            {
                using TcpClient client = await listener.AcceptTcpClientAsync(Token);
                using NetworkStream recieverStream = client.GetStream();

                CoffeeButton button = await JsonSerializer.DeserializeAsync<CoffeeButton>(recieverStream, cancellationToken: Token);

                HandleSimulation(button);                
            }
        }

        private void HandleSimulation(CoffeeButton button)
        {
            tbkResult.Text += $"New Result: {button}\n"; 
        }
    }
}
