using System;
using TP_eKasa.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TP_eKasa.Models;

namespace TP_eKasa
{
    public partial class App : Application
    {
        static Database database;
        public App()
        {            
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        public static Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new Database();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            var xx = new Profile("Default", 1);
            Database.SaveDefault(xx);
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            
        }

    }
}
