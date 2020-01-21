using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_eKasa;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TP_eKasa.Models;
using TP_eKasa.Views;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        WifiController wifi = new WifiController();
        [Obsolete]
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        [Obsolete]
        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;

            txtUsername.Completed += (s, e) => txtPassword.Focus();
            txtPassword.Completed += (s, e) => SignInProcedure(s, e);
        }


        // ----- Login, bez pripojenej lokalnej databazy-----

        [Obsolete]
        async void SignInProcedure(object sender, EventArgs e)
        {
            if (wifi.isOn())
            {
                if (txtUsername.Text != null && txtPassword.Text != null)
                {
                    User user = new User(txtUsername.Text, txtPassword.Text);
                    if (user.CheckInformation())
                    {
                        string username = txtUsername.Text;
                        string password = txtPassword.Text;

                        if (username == "elcom" && password == "pokladna")
                        {
                            ActivitySpinner.IsVisible = false;
                            await DisplayAlert("Prihlásenie", "Prihlásenie úspešné", "OK");
                            // App.UserDatabase.SaveUser(user);
                            // App.TokenDatabase.SaveToken(result);
                            // await Navigation.PushAsync(new Dashboard());
                            if (Device.OS == TargetPlatform.Android)
                            {
                                Application.Current.MainPage = new NavigationPage(new Tabbed());
                            }
                            else if (Device.OS == TargetPlatform.iOS)
                            {
                                await Navigation.PushModalAsync(new NavigationPage(new Tabbed()));
                            }
                        }

                    }
                    else
                    {
                        await DisplayAlert("Prihlásenie", "Prihlásenie nesprávne, nesprávne použiveteľské meno alebo heslo", "OK");
                        ActivitySpinner.IsVisible = true;
                    }
                }
                else
                {
                    await DisplayAlert("Prihlásenie", "Zadajte prihlásovacie meno a heslo", "OK");
                }
            }
            else
            {
                string action = await DisplayActionSheet("Prihlásenie neúspnešné - WIFI je vypnuté", "Cancel",null, "Zapnúť WIFI");
                if(action.Equals("Zapnúť WIFI"))
                {
                    string status = wifi.TurnOnWifi();
                    await DisplayAlert("WIFI", status, "OK");
                }
            }
        }
    }
}