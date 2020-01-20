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
        int polozky = 0;
        int count = 0;
        List<ecrloc> ecrlocs = new List<ecrloc>();
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
            ecrlocs = await App.Database.getECRLOC();
            foreach (ecrloc e in ecrlocs)
            {
                if (!e.ECRLOC_STREETNAME.Equals(""))
                {
                    if (count < 11)
                    {
                        count++;
                    }
                }
            }
            ecrlocView.ItemsSource = ecrlocs.Skip(polozky).Take(count);
        }

        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EcrlocaView() { BindingContext = e.SelectedItem as ecrloc });
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
                if (picker.SelectedItem.ToString().Equals("ECRLOC_ID"))
                {
                    foreach (ecrloc ecr in ecrlocs)
                    {
                        if (ecr.ECRLOC == Convert.ToInt32(polozka.Text))
                        {
                            ecrlocView.ItemsSource = ecrlocs.Skip(ecr.ECRLOC - 1).Take(1);
                        }
                    }
                }
                if (picker.SelectedItem.ToString().Equals("ECRLOC_STREETNAME"))
                {
                    foreach (ecrloc ecr in ecrlocs)
                    {
                        if (ecr.ECRLOC_STREETNAME.Equals(polozka.Text))
                        {
                            int idx = ecr.ECRLOC;
                            ecrlocView.ItemsSource = ecrlocs.Skip(idx - 1).Take(1);
                        }
                    }
                }
            }
        }
    }
}