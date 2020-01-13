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
    public partial class Plu : ContentPage
    {
        public Plu()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "ADD"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new PluView()
                {
                    BindingContext = new plu
                    {
                        PLU = App.Database.getPLU().Result.Count + 1
                    }
                });
            };

            ToolbarItems.Add(toolbarItem);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            pluView.ItemsSource = await App.Database.getPLU();
        }

        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new PluView() { BindingContext = e.SelectedItem as plu });
            }
        }
    }
}