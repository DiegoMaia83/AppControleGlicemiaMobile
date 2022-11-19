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

        public PageDestroCadastro(ModelDestro destro)
        {
            InitializeComponent();

            gridBtAlterar.IsVisible = true;
            gridBtInserir.IsVisible = false;

            //RetornarUltimaAfericao();

            txtDestroId.Text = destro.DestroId.ToString();
            txtValorAferido.Text = destro.ValorAferido.ToString();
            txtDataAferido.Text = destro.DataAferido.ToString();
            txtUltimaAfericao.Text = String.Format("Valor aferido em {0}", destro.DataAferido.ToString());

        }

        private void btInserir_Clicked(object sender, EventArgs e)
        {
            try
            {
                var destro = new ModelDestro()
                {
                    ValorAferido = Convert.ToInt32(txtValorAferido.Text),
                    DataAferido = DateTime.Now,
                    Observacoes = ""
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

            if (destro != null)
            {
                txtValorAferido.Text = destro.ValorAferido.ToString();
                txtUltimaAfericao.Text = String.Format("Última aferição {0} feita em {1}",
                    destro.ValorAferido.ToString(),
                    destro.DataAferido.ToString());
            }
        }

        private void btAlterar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var destro = new ModelDestro();
                destro.DestroId = Convert.ToInt32(txtDestroId.Text);
                destro.ValorAferido = Convert.ToInt32(txtValorAferido.Text);
                destro.DataAferido = Convert.ToDateTime(txtDataAferido.Text);
                destro.Observacoes = "";

                ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);

                dbDestro.Alterar(destro);

                DisplayAlert("Resultado da operação", dbDestro.StatusMessage, "OK");

                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageHome());
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "Ok");
            };
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir registro", "Deseja realmente excluir essa medição? Essa operação é irreversível!", "Sim", "Não");

            if(resp)
            {
                var id = Convert.ToInt32(txtDestroId.Text);
                ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);
                dbDestro.Excluir(id);
                await DisplayAlert("Resultado da operação", dbDestro.StatusMessage, "OK");

                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageHome());
            }
        }
    }
}