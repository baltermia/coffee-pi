using CoffeePi.Mobile.Views;
using Xamarin.Forms;

namespace CoffeePi.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreatePage), typeof(CreatePage));
            Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
        }
    }
}
