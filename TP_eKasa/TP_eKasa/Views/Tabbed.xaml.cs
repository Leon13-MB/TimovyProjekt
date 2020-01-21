using Java.Nio.FileNio;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TP_eKasa.Controllers;
using TP_eKasa.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tabbed : TabbedPage
    {
        WifiController wifi = new WifiController();
        Kasa kasa = new Kasa();
        BackupHandler handler = new BackupHandler();
        bool connected = true;

        public Tabbed()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filename = Path.Combine(path, "KASA.txt");
                string content = "";
                using (var streamReader = new StreamReader(filename))
                {
                    content = streamReader.ReadToEnd();
                }
                if(content.Equals(""))
                {
                    kasa.setTypeOfKasa();
                }

            using (var streamReader = new StreamReader(filename))
            {
                content = streamReader.ReadToEnd();
            }
            pokladnaName.Text += content;
                Title = content;
                string temp = "";
                if (content.Contains("E50"))
                {
                    temp = "E50o";
                }
                else if (content.Contains("E150"))
                {
                    temp = "E150o";
                }
                image.Source = Device.RuntimePlatform == Device.Android
                    ? ImageSource.FromFile(temp + ".png")
                    : ImageSource.FromFile("Images/" + temp + ".png");
            
            checkConnectivity();
        }

        public void checkConnectivity()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.4.1/Services?EcrInfo");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response != null)
                {
                    connected = true;
                    string name = kasa.getTypeOfKasa();
                    loadBackup(name);
                }
            }
            catch (Exception)
            {
                connected = false;
                DisplayAlert("Pripojenie", "Nie ste pripojeny k pokladni", "OK");
            }
        }


        public async void loadBackup(string Kasa)
        {
            string action = "";
            var kasa = App.Database.getKasaPLU(Kasa).Result;
            if (connected && kasa == null)
            {
                action = await DisplayActionSheet("Aplikácia nemá profil pre danú pokladňu, prajete si stiahnúť profil z pokladne?", "Cancel", null, "Áno", "Nie");
                if (action != null)
                {
                    if (action.Equals("Áno"))
                    {
                        await Navigation.PushAsync(new LoadData());
                    }
                    else if (action.Equals("Nie"))
                    {
                        await DisplayAlert("Upozornenie", "Dáta neboli načítané", "OK");
                    }
                }
            }
        }

        [Obsolete]
        public async void LogoutProcedure(object sender, EventArgs e)
        {
            await DisplayAlert("Odhlásenie", "Budete odhlásení z aplikácie", "Okej");
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }


        private async void Advertxt(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Advertxt());
        }
        private async void Dpt(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Dpt());
        }
        private async void Ecrloc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ecrloc());
        }
        private async void Functext(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Functext());
        }
        private async void Measunit(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Measunit());
        }
        private async void Operator(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Operator());
        }
        private async void Plu(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Plu());
        }
        private async void Surdisc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Surdisc());
        }
        private async void Textlogo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Textlogo());
        }
        private async void Triallog(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Traillog());
        }
        private async void UploadTables(object sender, EventArgs e)
        {
            if (connected)
            {
                await Navigation.PushAsync(new Page1());
            }
        }
    }
}