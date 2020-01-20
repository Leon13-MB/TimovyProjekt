using System;
using TP_eKasa.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TP_eKasa.Models;
using Xamarin.Essentials;
using System.IO;
using Java.IO;
using Android.Content.Res;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TP_eKasa.Controllers;
using TP_eKasa.Views;

namespace TP_eKasa
{
    public partial class App : Application
    {
        static Database database;

        public App()
        {
            try
            {
                InitializeComponent();
                MainPage = new NavigationPage(new MainPage());
            }
            catch (Exception e)
            {
                string ex = e.ToString();
            }
        }

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //TU vytvorí v databaze defaultne tabulky, ak existuju nerobi nič
            try
            {
                BackupHandler handler = new BackupHandler();
                Kasa kasa = new Kasa();
                kasa.setTypeOfKasa();
                //handler.createFiles();
            }
            catch (Exception e)
            {
                string excep = e.ToString();
            }
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
