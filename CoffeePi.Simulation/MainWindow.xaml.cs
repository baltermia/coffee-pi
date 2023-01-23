using CoffeePi.Shared.DataTransferObjects;
using CoffeePi.Shared.Enums;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
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

        private readonly string IP = "127.0.0.1";
        private readonly int Port = 1302;

        private readonly int delay = 7000;

        private static readonly Brush Red = new SolidColorBrush(Colors.Red);
        private static readonly Brush Green = new SolidColorBrush(Colors.Green);

        private bool running = false;
        public string RunningText => running ? "Eingeschaltet" : "Ausgeschaltet";

        private readonly HttpClient client;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            source = new();

            client = new()
            {
                BaseAddress = new Uri("http://localhost:5000/api/")
            };
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await GetSettingsFromApi();

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

                await HandleSimulationAsync(button);

                await File.AppendAllTextAsync(logPath, $"{DateTime.Now}: {button}\n");
            }
        }

        private async Task HandleSimulationAsync(CoffeeButton button)
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

            await GetSettingsFromApi();
        }

        private async Task GetSettingsFromApi()
        {
            MachinePropertiesDto props = await client.GetFromJsonAsync<MachinePropertiesDto>("MachineProperties");

            running = props.Running;

            WaterLevel.Value = (double)props.WaterLevel;
            BeanStatus.Value = (double)props.BeanStatus;

            WaterLevelValue.Text = props.WaterLevel.ToString("P0");
            BeanStatusValue.Text = props.BeanStatus.ToString("P0");
        }

        private bool AnyButtonsRunning() =>
            crlEspresso.Fill == Green ||
            crlCoffeeSmall.Fill == Green ||
            crlCoffeeBig.Fill == Green ||
            crlWarmWater.Fill == Green;

        private async void RefillBeans_Click(object sender, RoutedEventArgs e)
        {
            await client.PatchAsync("MachineProperties/refill-beans", null);

            await GetSettingsFromApi();
        }

        private async void RefillWater_Click(object sender, RoutedEventArgs e)
        {
            await client.PatchAsync("MachineProperties/refill-water", null);

            await GetSettingsFromApi();
        }

        private async void Test_Click(object sender, RoutedEventArgs e)
        {
            await client.PostAsJsonAsync("Single", new SingleRoutineDto
            {
                ButtonType = CoffeeButton.SmallCup,
                Enabled = true,
                Time = DateTime.Now
            });
        }
    }
}
