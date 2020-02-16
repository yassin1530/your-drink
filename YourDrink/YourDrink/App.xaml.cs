using System;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YourDrink
{
    public partial class App : Application
    {
        public static string DatabasePath = String.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
        public App(string databasePath)
        {
            InitializeComponent();

            DatabasePath = databasePath;

            MainPage = new MainPage();

            
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
