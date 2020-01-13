using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_eKasa.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP_eKasa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Conf1 : ContentPage
    {
        public Conf1()
        {
            InitializeComponent();

            var toolbarItem = new ToolbarItem
            {
                Text = "ADD Configuration"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new Conf1Items() { BindingContext = new Configuration1() });
            };

            ToolbarItems.Add(toolbarItem);
            OnAppearing();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Conf1ListView.ItemsSource = await App.Database.GetConf1ItemsAsync();
        }

        async void ConfSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Conf1Items() { BindingContext = e.SelectedItem as Configuration1 });
            }
        }

    }
}