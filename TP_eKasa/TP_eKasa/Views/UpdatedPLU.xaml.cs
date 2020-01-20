using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_eKasa.Controllers;
using TP_eKasa.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatedPLU : ContentPage
    {
        int polozky = 0;
        List<updatedPlu> plus2 = new List<updatedPlu>();
        List<updatedPlu> plus = new List<updatedPlu>();
        bool find = false;

        Kasa kasa = new Kasa();
        public UpdatedPLU()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            changeListView();
        }

        public async void changeListView()
        {
            plus = new List<updatedPlu>();
            string name = kasa.getTypeOfKasa();
            plus = await App.Database.getUpdatedPLU();
            if (plus.Count > 0)
            {
                pluView.ItemsSource = plus;
            }
            else
            {
                await DisplayAlert("Warning", "V tejto tabuľke nie sú žiadné položky.", "OK");
            }
        }
        private void Previous(object sender, EventArgs e)
        {
            if (polozky > 0)
            {
                polozky -= 10;
                if (!find)
                {
                    changeListView();
                }
                else
                {
                    subTen();
                }
            }
        }

        private void Next(object sender, EventArgs e)
        {
            if (polozky < 2000)
            {
                polozky += 10;
                if (!find)
                {
                    changeListView();
                }
                else
                {
                    addTen();
                }
            }
        }

        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new UpdatedPLU() { BindingContext = e.SelectedItem as updatedPlu });
            }
        }

        public async void Find(object sender, EventArgs e)
        {
            find = true;
            List<updatedPlu> plus = new List<updatedPlu>();
            plus2.Clear();
            if (picker != null && polozka != null && pickerPokladna != null)
            {
                string kasa = pickerPokladna.SelectedItem.ToString();
                if (picker.SelectedItem.ToString().Equals("PLU_ID"))
                {
                    plus.Add(await App.Database.selectUpdatedPlu(Convert.ToInt32(polozka.Text), kasa));
                    if (plus[0].PLU_NAME.Equals("NaN"))
                    {
                        await DisplayAlert("Warning", "Tomuto ID nie je priradena polozka", "OK");
                    }
                    else
                    {
                        pluView.ItemsSource = plus;
                    }
                }
                if (picker.SelectedItem.ToString().Equals("PLU_NAME"))
                {
                    plus2 = await App.Database.getUpdatedPluByName(polozka.Text, kasa);
                    if (plus2.Count >= 10)
                    {
                        pluView.ItemsSource = plus2.Take(10);
                    }
                    else
                    {
                        pluView.ItemsSource = plus2;
                    }
                }
            }
            else
            {
                await DisplayAlert("Warning", "Chybne vyhladavanie", "OK");
            }
        }

        public void addTen()
        {
            int take = 0;
            if (plus2.Count != 0)
            {
                if (plus2.Count <= 10)
                {
                    pluView.ItemsSource = plus2;
                }
                else
                {
                    if (plus2.Count + polozky > polozky + 10)
                    {
                        pluView.ItemsSource = plus2.Skip(polozky).Take(10);
                    }
                    else
                    {
                        take = plus2.Count % 10;
                        if (take != 0)
                        {
                            pluView.ItemsSource = plus2.Skip(polozky).Take(take);
                        }
                    }
                }
            }
        }

        public void subTen()
        {
            if (plus2.Count != 0)
            {
                if (plus2.Count <= 10)
                {
                    pluView.ItemsSource = plus2;
                }
                else
                {
                    pluView.ItemsSource = plus2.Skip(polozky).Take(10);
                }
            }
        }
        public void cancelFind(object sender, EventArgs e)
        {
            find = false;
            polozky = 0;
            changeListView();
        }

        public void test(object sender, EventArgs e)
        {
            foreach(updatedPlu up in pluView.ItemsSource)
            {
                var p = up;
            }
        }

    }
}