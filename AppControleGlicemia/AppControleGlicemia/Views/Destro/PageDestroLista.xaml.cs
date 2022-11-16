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

            AtualizaLista();
        }

        public void AtualizaLista()
        {
            ServicesDbDestro dbDestro = new ServicesDbDestro(App.DbPath);

            ListaDestro.ItemsSource = dbDestro.Listar();
        }
    }
}