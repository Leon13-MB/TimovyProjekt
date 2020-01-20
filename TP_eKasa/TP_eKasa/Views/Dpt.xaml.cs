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
    public partial class Dpt : ContentPage
    {
        int polozky = 0;
        int count = 0;
        List<dpt> dpts = new List<dpt>();
        public Dpt()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "ADD"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new DptView()
                {
                    BindingContext = new dpt
                    {
                        DPT = App.Database.getDPT().Result.Count + 1
                    }
                });
            };

            ToolbarItems.Add(toolbarItem);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            dpts = await App.Database.getDPT();
            foreach (dpt d in dpts)
            {
                if (!d.DPT_NAME.Equals(""))
                {
                    if (count < 11)
                    {
                        count++;
                    }
                }
            }
            dptView.ItemsSource = dpts.Skip(polozky).Take(count);
        }
        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new DptView() { BindingContext = e.SelectedItem as dpt });
            }
        }
        private void Previous(object sender, EventArgs e)
        {
            if (polozky > 0 && count > 10)
            {
                polozky = polozky - 10;
                OnAppearing();
            }
        }

        private void Next(object sender, EventArgs e)
        {
            if (count > 10)
            {
                polozky = polozky + 10;
                OnAppearing();
            }
        }
        public void Find(object sender, EventArgs e)
        {
            if (picker != null && polozka != null && pickerPokladna != null)
            {
                if (picker.SelectedItem.ToString().Equals("DPT_ID"))
                {
                    foreach (dpt d in dpts)
                    {
                        if (d.DPT == Convert.ToInt32(polozka.Text))
                        {
                            dptView.ItemsSource = dpts.Skip(d.DPT - 1).Take(1);
                        }
                    }
                }
                if (picker.SelectedItem.ToString().Equals("DPT_NAME"))
                {
                    foreach (dpt d in dpts)
                    {
                        if (d.DPT_NAME.Equals(polozka.Text))
                        {
                            int idx = d.DPT;
                            dptView.ItemsSource = dpts.Skip(idx - 1).Take(1);
                        }
                    }
                }
            }
        }
    }
}