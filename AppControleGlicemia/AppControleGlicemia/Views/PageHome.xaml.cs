using AppControleGlicemia.Services;
using AppControleGlicemia.Views.Destro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppControleGlicemia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHome : ContentPage
    {
        ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);

        public PageHome()
        {
            InitializeComponent();

            txtMediaDia.Text = dbDestro.RetornarMediaDia().ToString();
        }

        private void btInserirDestro_Clicked(object sender, EventArgs e)
        {
            FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
            page.Detail = new NavigationPage(new PageDestroCadastro());
        }
    }
}