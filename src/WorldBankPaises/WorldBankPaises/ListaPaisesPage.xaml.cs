using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldBankPaises.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorldBankPaises
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPaisesPage : ContentPage
    {
        private bool paisesObtidos = false;
        private ObservableCollection<Pais> paises = new ObservableCollection<Pais>();
        private API.WorldBank api = new API.WorldBank();

        public ListaPaisesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            if (!paisesObtidos)
            {
                PaisesListView.ItemsSource = await this.api.ObtemListaPaises();
                paisesObtidos = true;
            }
        }

        private async void PaisesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var p = (e.SelectedItem as Pais);
                await this.api.ComplementaDetalhePais(p);
                await Navigation.PushAsync(new PaisPage(p));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}