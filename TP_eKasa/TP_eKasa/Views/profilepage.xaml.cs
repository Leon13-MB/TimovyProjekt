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
    public partial class profilepage : ContentPage
    {
        public profilepage()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "ADD profile"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new profilepageitem() { BindingContext = new Profile() });
            };

            ToolbarItems.Add(toolbarItem);
            
            OnAppearing();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Conf1ListView.ItemsSource = await App.Database.GetProfiles();
        }

        async void ConfSelect(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new profilepageitem() { BindingContext = e.SelectedItem as Profile });
            }
        }
    }
}