using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using TP_eKasa.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Advertxt : ContentPage
    {
        public Advertxt()
        {
            InitializeComponent();

            var toolbarItem = new ToolbarItem
            {
                Text = "ADD"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new AdvertxtView()
                {
                    BindingContext = new advertxt
                    {
                        ADVERTEXT = App.Database.getADVERTXT().Result.Count+1
                    }

                });
            };

            ToolbarItems.Add(toolbarItem);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            advertxtView.ItemsSource = await App.Database.getADVERTXT();
        }


        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AdvertxtView() { BindingContext = e.SelectedItem as advertxt });
            }
        }

    }
}