using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TP_eKasa.Models;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Surdisc : ContentPage
    {
        public Surdisc()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            surdiscView.ItemsSource = await App.Database.getSURDISC();
        }

        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new SurdiscView() { BindingContext = e.SelectedItem as surdisc });
            }
        }
    }
}