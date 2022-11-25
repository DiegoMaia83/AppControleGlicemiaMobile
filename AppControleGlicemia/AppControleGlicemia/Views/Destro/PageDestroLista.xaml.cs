using AppControleGlicemia.Models;
using AppControleGlicemia.Services;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppControleGlicemia.Views.Destro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDestroLista : ContentPage
    {
        public PageDestroLista()
        {
            InitializeComponent();

            pckPeriodo.SelectedIndex = 2;

            AtualizaLista(2);
        }

        public void AtualizaLista(int idxPeriodo)
        {
            ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);

            var lista = dbDestro.Listar(idxPeriodo).OrderByDescending(x => x.DataAferido).ToList();

            ListaDestro.ItemsSource = lista;
        }

        private void pckPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idxPeriodo = pckPeriodo.SelectedIndex;

            AtualizaLista(idxPeriodo);
        }

        private void ListaDestro_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ModelDestro destro = (ModelDestro)ListaDestro.SelectedItem;

            FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
            page.Detail = new NavigationPage(new PageDestroCadastro(destro));
        }
    }
}