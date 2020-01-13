using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TP_eKasa.Models;
using Xamarin.Essentials;
using System.IO;
using System.Data;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PluView : ContentPage
    {
        public PluView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            addSaveOrUpdate();
            DataTable dt = new DataTable();
            EDFHandler edf = new EDFHandler();
            var temp = (plu)BindingContext;
            using (var stream = await FileSystem.OpenAppPackageFileAsync("PLU.EDF"))
            {
                using (var reader = new StreamReader(stream))
                {
                    dt = edf.readEDF(reader);
                    reader.Close();
                }
                stream.Close();
            }
            var bc = (plu)BindingContext;
            if (temp.PLU > dt.Rows.Count && bc.ID != 0)
            {
                addDeleteButton();
            }
        }

        void addDeleteButton()
        {
            Button button = new Button
            {
                Text = "Delete"
            };

            button.Clicked += async (sender, args) =>
            {
                var deletedItem = (plu)BindingContext;
                var action = await DisplayActionSheet("Urcite vymazat?", null, null, "Ano", "Nie");
                if (action.Equals("Ano") && action != null)
                {
                    await App.Database.deletePLU(deletedItem);
                    int num = deletedItem.PLU - 1;
                    List<plu> plus = await App.Database.getPLU();
                    foreach (plu a in plus.Skip(num))
                    {
                        a.PLU -= 1;
                        await App.Database.updatePLU(a);
                    }
                    await Navigation.PopAsync();
                }
                else if (action.Equals("Nie") && action != null)
                {
                }
            };
            sl.Children.Add(button);
        }

        async void addSaveOrUpdate()
        {
            var bc = (plu)BindingContext;
            List<plu> at = await App.Database.getPLU();
            foreach (plu a in at)
            {
                if (bc.PLU == a.PLU)
                {
                    suButton.Text = "Update";
                    break;
                }
                else
                {
                    suButton.Text = "Save";
                }
            }
        }

        async void buttonClicked(object sender, EventArgs e)
        {
            var action = "";
            if (suButton.Text.Equals("Save"))
            {
                await App.Database.savePLU((plu)BindingContext);
                action = await DisplayActionSheet("Ulozene", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
            else if (suButton.Text.Equals("Update"))
            {

                await App.Database.updatePLU((plu)BindingContext);
                action = await DisplayActionSheet("Aktualizovane", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
        }
    }
}