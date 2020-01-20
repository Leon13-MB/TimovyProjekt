using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TP_eKasa.Controllers;
using Xamarin.Forms.Xaml;
using TP_eKasa.Models;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Plu : ContentPage
    {
        int polozky = 0;
        List<plu> plus2 = new List<plu>();
        bool find = false;

        Kasa kasa = new Kasa();
        public Plu()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "ADD"
            };

            toolbarItem.Clicked += async (sender, e) =>
            {
                string name = kasa.getTypeOfKasa();
                var p = await App.Database.getWithoutNamePlu("NaN", name);
                if (p != null)
                {
                    await Navigation.PushAsync(new PluView
                    {
                        BindingContext = p as plu
                    });
                }
                else
                {
                    await Navigation.PushAsync(new PluView());
                }
            };
            ToolbarItems.Add(toolbarItem);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            changeListView();
        }

        public async void changeListView()
        {
            List<plu> plus = new List<plu>();
            string name = kasa.getTypeOfKasa();
            for (int i = (1 + polozky); i < (11 + polozky); i++)
            {
                var pl = await App.Database.selectPlu(i, name);
                if (pl!=null && !pl.PLU_NAME.Equals("NaN"))
                {
                    plus.Add(pl);
                }
            }
            if (plus.Count > 0)
            {
                pluView.ItemsSource = plus;
            }
        }

        async void ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new PluView() { BindingContext = e.SelectedItem as plu });
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

        public async void Find(object sender, EventArgs e)
        {
            find = true;
            List<plu> plus = new List<plu>();
            plus2.Clear();
            string kasa = pickerPokladna.SelectedItem.ToString();
            if (picker != null && polozka != null && pickerPokladna != null)
            {
                if (picker.SelectedItem.ToString().Equals("PLU_ID"))
                {
                    plus.Add(await App.Database.selectPlu(Convert.ToInt32(polozka.Text), kasa));
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
                    plus2 = await App.Database.getPluByName(polozka.Text, kasa);
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
    }
}