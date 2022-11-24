using AppControleGlicemia.Services;
using AppControleGlicemia.Models;
using System;
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

            // Popula os campos data e hora com o horário atual
            DateTime now = DateTime.Now;
            txtData.Date = now.Date;
            txtHora.Time = new TimeSpan(now.Hour, now.Minute, now.Second);

            // Preenche o picker com os tipos de insulina
            PopularPickerInsulina();

            RetornarUltimaAfericao();
        }

        public PageDestroCadastro(ModelDestro destro)
        {
            InitializeComponent();

            // Popula os campos data e hora com o horário que retorna da tabela
            DateTime now = destro.DataAferido;
            txtData.Date = now.Date;
            txtHora.Time = new TimeSpan(now.Hour, now.Minute, now.Second);

            gridBtAlterar.IsVisible = true;
            gridBtInserir.IsVisible = false;

            txtDestroId.Text = destro.DestroId.ToString();
            txtValorAferido.Text = destro.ValorAferido.ToString();
            pckInsulina.SelectedItem = destro.InsulinaTipo;
            qtdInsulina.Text = destro.InsulinaQuantidade.ToString();

            // Preenche o picker com os tipos de insulina
            PopularPickerInsulina(destro.InsulinaTipo);
        }

        private void btInserir_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Agrupa a data e hora selecionada em um DateTime
                var date = txtData.Date;
                var hour = txtHora.Time;
                DateTime datetime = date + hour;

                var destro = new ModelDestro();
                destro.ValorAferido = Convert.ToInt32(txtValorAferido.Text);
                destro.DataAferido = datetime;
                destro.InsulinaTipo = pckInsulina.SelectedItem != null ? pckInsulina.SelectedItem.ToString() : "";
                destro.InsulinaQuantidade = !String.IsNullOrEmpty(qtdInsulina.Text) ? Convert.ToInt32(qtdInsulina.Text) : 0;

                ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);

                dbDestro.Inserir(destro);

                DisplayAlert("Resultado da operação", dbDestro.StatusMessage, "OK");

                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageDestroLista());
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
            }
        }

        private void btAlterar_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Agrupa a data e hora selecionada em um DateTime
                var date = txtData.Date;
                var hour = txtHora.Time;
                DateTime datetime = date + hour;

                var destro = new ModelDestro()
                {
                    DestroId = Convert.ToInt32(txtDestroId.Text),
                    ValorAferido = Convert.ToInt32(txtValorAferido.Text),
                    DataAferido = datetime,
                    InsulinaTipo = pckInsulina.SelectedItem != null ? pckInsulina.SelectedItem.ToString() : "",
                    InsulinaQuantidade = !String.IsNullOrEmpty(qtdInsulina.Text) ? Convert.ToInt32(qtdInsulina.Text) : 0
                };

                ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);

                dbDestro.Alterar(destro);

                DisplayAlert("Resultado da operação", dbDestro.StatusMessage, "OK");

                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageDestroLista());
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

        public void PopularPickerInsulina()
        {
            ServicesDbInsulina dbInsulina = new ServicesDbInsulina(App.DbPath);

            var lista = dbInsulina.Listar();

            foreach (var item in lista)
            {
                pckInsulina.Items.Add(item.Tipo);
            }

            pckInsulina.Items.Add("Adicionar novo");
        }

        public void PopularPickerInsulina(string selecionado = "")
        {
            ServicesDbInsulina dbInsulina = new ServicesDbInsulina(App.DbPath);

            var lista = dbInsulina.Listar();

            foreach (var item in lista)
            {
                pckInsulina.Items.Add(item.Tipo);
            }

            pckInsulina.SelectedItem = selecionado;

            pckInsulina.Items.Add("Adicionar novo");
        }

        private void pckInsulina_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pck = (Picker)sender;

            if (pck.SelectedItem.ToString() == "Adicionar novo")
            {
                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageInsulina());
            };
        }
    }
}