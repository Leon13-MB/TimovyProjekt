using Android.Content;
using Android.Net.Wifi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_eKasa.Models;
using TP_eKasa.Views;
using Xamarin.Forms;

namespace TP_eKasa
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private WifiManager wifi = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

        //public IList<ScanResult> list { get; private set; }

        public MainPage()
        {
            InitializeComponent();
        }
        public async void ShowConfiguration(object sender, EventArgs e)
        {
            if (tableEntry.Text == "" || tableEntry.Text == null)
            {
                await DisplayAlert("Configuration", "Zadajte ", "OK");
            }
            else {
                    string id = tableEntry.Text;
                    //po vykonani connect operatora CONNECT/tREQ/n sa pouzije operator GETDEVINFO/tREQ/n
                    //a odpoved sa parsuje... GETDEVINFO/tRSP/t0/tE155TE/tSK/t1.026/t1.00/tN/tBIGL02866C/n
                    //na zaklade devType sa otvori prislusna tabulka konfiguracii 
                    //na konci prebehne DISCONNECT/tREQ/n
                    //zatial , ale ma byt \t, na mobile sa neda dat tab a ak surovo napisem \t da mi \\t
                    string separator = ",";
                    string[] result = id.Split(separator.ToCharArray());
                    switch (result[3])
                    {
                        case "1":
                            //testovacia konfiguracia, na ktorej sa testuju nase implementacie, resp funkcie
                            await Navigation.PushAsync(new Conf1());
                            break;
                        case "EURO50MINI":
                            await Navigation.PushAsync(new Conf1());
                            break;
                        case "EURO150FLEXY":
                            await Navigation.PushAsync(new Conf1());
                            break;
                    }
                
            }

        }

        [Obsolete]
        public void TurnOnWifi(object sender, EventArgs e)
        {
            if (wifi.IsWifiEnabled) {
                DisplayAlert("Wifi", "Wifi is already on", "ok");
            }
            else
            {
                wifi.SetWifiEnabled(true);
            }
        }

        public void TurnOffWifi(object sender, EventArgs e)
        {
            wifi.SetWifiEnabled(false);
        }

        public async void FTP(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FTP());
        }

        public async void tables(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Tables());
        }
    }
}
