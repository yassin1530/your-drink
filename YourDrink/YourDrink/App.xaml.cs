using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YourDrink
{
    public partial class App : Application
    {
        public string DatabasePath { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
        public App(string databasePath)
        {
            InitializeComponent();

            MainPage = new MainPage();

            DatabasePath = databasePath;
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
