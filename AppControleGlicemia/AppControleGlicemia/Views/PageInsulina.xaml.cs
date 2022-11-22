using System;
using AppControleGlicemia.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppControleGlicemia.Services;

namespace AppControleGlicemia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInsulina : ContentPage
    {
        public PageInsulina()
        {
            InitializeComponent();

            AtualizaLista();
        }

        private void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var insulina = new ModeInsulina()
                {
                    Tipo = txtTipoInsulina.Text
                };

                ServicesDbInsulina dbInsulina = new ServicesDbInsulina(App.DbPath);

                dbInsulina.Inserir(insulina);

                DisplayAlert("Resultado da operação", dbInsulina.StatusMessage, "OK");

                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageInsulina());
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "Ok");
            };
        }

        public void AtualizaLista()
        {
            ServicesDbInsulina dbInsulina = new ServicesDbInsulina(App.DbPath);

            var lista = dbInsulina.Listar();

            ListaInsulina.ItemsSource = lista;
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {            
            var resp = await DisplayAlert("Excluir registro", "Deseja realmente excluir esse registro? Essa operação é irreversível!", "Sim", "Não");

            if (resp)
            {
                var label = (Label)sender;
                var model = (ModeInsulina)label.BindingContext;

                ServicesDbInsulina dbInsulina = new ServicesDbInsulina(App.DbPath);
                dbInsulina.Excluir(model.InsulinaId);
                await DisplayAlert("Resultado da operação", dbInsulina.StatusMessage, "OK");

                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageInsulina());
            }
        }
    }
}