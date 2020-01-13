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
    public partial class Measunit : ContentPage
    {
        public Measunit()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "ADD"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MeasunitView()
                {
                    BindingContext = new measunit
                    {
                        MEASUREUNIT = App.Database.getMEASUNIT().Result.Count + 1
                    }
                });
            };

            ToolbarItems.Add(toolbarItem);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            measunitView.ItemsSource = await App.Database.getMEASUNIT();
        }

        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new MeasunitView() { BindingContext = e.SelectedItem as measunit });
            }
        }
    }
}