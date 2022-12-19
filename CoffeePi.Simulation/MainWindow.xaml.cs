using CoffeePi.Shared.Enums;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CoffeePi.Simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string logPath = "Runs.log";

        private readonly CancellationTokenSource source;
        private CancellationToken Token => source.Token;

        private readonly string IP = "192.168.213.27";
        private readonly int Port = 1302;

        private readonly int delay = 7000;

        private static readonly Brush Red = new SolidColorBrush(Colors.Red);
        private static readonly Brush Green = new SolidColorBrush(Colors.Green);

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
            TcpListener listener = new(IPAddress.Any, Port);

            listener.Start();
            
            while (!Token.IsCancellationRequested)
            {
                using TcpClient client = await listener.AcceptTcpClientAsync(Token);
                using NetworkStream recieverStream = client.GetStream();

                CoffeeButton button = await JsonSerializer.DeserializeAsync<CoffeeButton>(recieverStream, cancellationToken: Token);

                HandleSimulation(button);

                await File.AppendAllTextAsync(logPath, $"{DateTime.Now}: {button}\n");
            }
        }

        private async void HandleSimulation(CoffeeButton button)
        {
            while (AnyButtonsRunning())
            {
                await Task.Delay(200);
            }

            Ellipse circle = button switch
            {
                CoffeeButton.Espresso => crlEspresso,
                CoffeeButton.SmallCup => crlCoffeeSmall,
                CoffeeButton.BigCup => crlCoffeeBig,
                CoffeeButton.HotWater => crlWarmWater,
                _ => default
            };

            if (circle == default)
            {
                return;
            }

            circle.Fill = Green;

            await Task.Delay(delay);

            circle.Fill = Red;
        }

        private bool AnyButtonsRunning() =>
            crlEspresso.Fill == Green ||
            crlCoffeeSmall.Fill == Green ||
            crlCoffeeBig.Fill == Green ||
            crlWarmWater.Fill == Green;
    }
}
