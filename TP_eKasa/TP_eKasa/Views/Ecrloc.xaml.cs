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
    public partial class Ecrloc : ContentPage
    {
        public Ecrloc()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "ADD"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new EcrlocaView()
                {
                    BindingContext = new ecrloc
                    {
                        ECRLOC = App.Database.getECRLOC().Result.Count + 1
                    }
                });
            };

            ToolbarItems.Add(toolbarItem);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ecrlocView.ItemsSource = await App.Database.getECRLOC();
        }

        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EcrlocaView() { BindingContext = e.SelectedItem as ecrloc });
            }
        }
    }
}