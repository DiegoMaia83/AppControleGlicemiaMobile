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

            var mediaHoje = dbDestro.RetornarMediaDia(DateTime.Now);
            txtMediaHoje.Text = mediaHoje.Media.ToString();
            txtQuantidadeHoje.Text = mediaHoje.Quantidade.ToString();

            var mediaOntem = dbDestro.RetornarMediaDia(DateTime.Now.AddDays(-1));
            txtMediaOntem.Text = mediaOntem.Media.ToString();
            txtQuantidadeOntem.Text = mediaOntem.Quantidade.ToString();

            var ultimaMedicao = dbDestro.RetornarUltimaAfericao();
            if(ultimaMedicao != null)
            {
                txtUltimaData.Text = ultimaMedicao.DataAferido != null ? ultimaMedicao.DataAferido.ToString() : "";
                txtUltimaMedicao.Text = ultimaMedicao.ValorAferido.ToString();
            }
        }

        private void btInserirDestro_Clicked(object sender, EventArgs e)
        {
            FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
            page.Detail = new NavigationPage(new PageDestroCadastro());
        }
    }
}