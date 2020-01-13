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
    public partial class Operator : ContentPage
    {
        public Operator()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "ADD"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new OperatorView()
                {
                    BindingContext = new @operator
                    {
                        OPERATOR = App.Database.getOPERATOR().Result.Count + 1
                    }
                });
            };

            ToolbarItems.Add(toolbarItem);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            operatorView.ItemsSource = await App.Database.getOPERATOR();
        }
        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new OperatorView() { BindingContext = e.SelectedItem as @operator });
            }
        }
    }
}