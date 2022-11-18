using AppControleGlicemia.Services;
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
    public partial class PageDestroLista : ContentPage
    {
        public PageDestroLista()
        {
            InitializeComponent();

            AtualizaLista(0);
        }

        public void AtualizaLista(int idxPeriodo)
        {
            ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);

            ListaDestro.ItemsSource = dbDestro.Listar(idxPeriodo);
        }

        private void pckPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idxPeriodo = pckPeriodo.SelectedIndex;

            AtualizaLista(idxPeriodo);
        }
    }
}