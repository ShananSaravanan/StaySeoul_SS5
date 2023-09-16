using StaySeoul_SS5.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaySeoul_SS5
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new LoginPage();
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
