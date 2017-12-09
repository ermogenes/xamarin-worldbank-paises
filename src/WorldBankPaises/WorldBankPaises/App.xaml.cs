using Xamarin.Forms;

namespace WorldBankPaises
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListaPaisesPage());
        }

    }
}
