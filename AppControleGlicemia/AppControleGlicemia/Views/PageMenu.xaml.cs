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
    public partial class PageMenu : FlyoutPage
    {
        public PageMenu()
        {
            InitializeComponent();

            this.Detail = new NavigationPage(new PageHome());
            IsPresented = false;
        }

        private void btHome_Clicked(object sender, EventArgs e)
        {
            this.Detail = new NavigationPage(new PageHome());
            IsPresented = false;
        }

        private void btDestroCadastro_Clicked(object sender, EventArgs e)
        {
            this.Detail = new NavigationPage(new PageDestroCadastro());
            IsPresented = false;
        }

        private void btDestroLista_Clicked(object sender, EventArgs e)
        {
            this.Detail = new NavigationPage(new PageDestroLista());
            IsPresented = false;
        }

        private void btSobre_Tapped(object sender, EventArgs e)
        {
            this.Detail = new NavigationPage(new PageSobre());
            IsPresented = false;
        }
    }
}