using AppControleGlicemia.Services;
using AppControleGlicemia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppControleGlicemia.Views.Destro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDestroCadastro : ContentPage
    {
        public PageDestroCadastro()
        {
            InitializeComponent();

            RetornarUltimaAfericao();
        }

        private void btInserir_Clicked(object sender, EventArgs e)
        {
            try
            {
                var destro = new ModelDestro()
                {
                    ValorAferido = Convert.ToInt32(txtValorAferido.Text),
                    DataAferido = DateTime.Now,
                    Observacoes = "Teste"
                };

                ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);

                dbDestro.Inserir(destro);

                DisplayAlert("Resultado da operação", dbDestro.StatusMessage, "OK");

                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageHome());
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "Ok");
            };
        }

        private void BtAtualizaValor(object sender, EventArgs e)
        {
            Button bt = (Button)sender;

            var valor = Convert.ToInt32(txtValorAferido.Text);

            var valorAtual = valor;

            if (bt.Text == "+")
            {
                valorAtual = Adicao(valor);
            }
            else if (bt.Text == "-")
            {
                valorAtual = Subtracao(valor);
            }

            txtValorAferido.Text = valorAtual.ToString();
        }

        private int Subtracao(int valor)
        {
            return valor - 1;
        }

        private int Adicao(int valor)
        {
            return valor + 1;
        }

        private void RetornarUltimaAfericao()
        {
            ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);

            ModelDestro destro = dbDestro.RetornarUltimaAfericao();

            txtValorAferido.Text = destro.ValorAferido.ToString();
            txtUltimaAfericao.Text = String.Format("Última aferição {0} feita em {1}",
                destro.ValorAferido.ToString(),
                destro.DataAferido.ToString());
        }
    }
}