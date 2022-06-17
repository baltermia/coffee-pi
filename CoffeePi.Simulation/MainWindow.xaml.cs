using CoffeePi.Shared.DataTransferObjects;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace CoffeePi.Simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource source;
        private CancellationToken Token => source.Token;

        public MainWindow()
        {
            InitializeComponent();

            source = new();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Listener();
        }

        private async Task Listener()
        {
            TcpListener listener = new(IPAddress.Parse("127.0.0.1"), 1302);
            listener.Start();
            
            while (!Token.IsCancellationRequested)
            {
                using TcpClient client = await listener.AcceptTcpClientAsync();

                using NetworkStream stream = client.GetStream();
                XmlSerializer serializer = new(typeof(TcpSocketButtonDto));

                TcpSocketButtonDto dto = serializer.Deserialize(stream) as TcpSocketButtonDto;

                if (dto != default(TcpSocketButtonDto))
                {
                    HandleDto(dto);
                }
            }
        }

        private void HandleDto(TcpSocketButtonDto dto)
        {
            tbkResult.Text += $"New Result: {dto.Button}, {dto.State}\n"; 
        }
    }
}
