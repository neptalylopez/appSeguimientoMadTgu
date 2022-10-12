using MadTguSeguimientoApp.Views.Persona;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadTguSeguimientoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PersonaListPage());
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
