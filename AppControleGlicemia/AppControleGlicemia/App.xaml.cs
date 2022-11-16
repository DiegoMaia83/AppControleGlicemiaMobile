using AppControleGlicemia.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppControleGlicemia
{
    public partial class App : Application
    {
        public static string DbPath;
        public static string DbName;        

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PageMenu())
            {
                BarBackgroundColor = Color.Gray
            };
        }

        public App(string dbPath, string dbName)
        {
            InitializeComponent();

            App.DbPath = dbPath;
            App.DbName = dbPath;

            MainPage = new PageMenu();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
