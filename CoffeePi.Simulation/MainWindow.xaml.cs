using System.Net.Sockets;
using System.Windows;

namespace CoffeePi.Simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TcpClient client = new("127.0.0.1", 1302);
        }
    }
}
