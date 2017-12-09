using WorldBankPaises.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorldBankPaises
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaisPage : ContentPage
    {
        private Pais pais;
        public PaisPage(Pais pais)
        {
            InitializeComponent();
            this.pais = pais;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = this.pais;
        }
    }
}