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
    public partial class Functext : ContentPage
    {
        public Functext()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "ADD"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new FunctextView()
                {
                    BindingContext = new functext
                    {
                        FUNCTEXT = App.Database.getFUNCTEXT().Result.Count + 1
                    }
                });
            };

            ToolbarItems.Add(toolbarItem);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            functextView.ItemsSource = await App.Database.getFUNCTEXT();
        }

        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new FunctextView() { BindingContext = e.SelectedItem as functext });
            }
        }
    }
}