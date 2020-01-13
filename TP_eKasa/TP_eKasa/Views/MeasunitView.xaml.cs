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
    public partial class MeasunitView : ContentPage
    {
        public MeasunitView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            addSaveOrUpdate();
            DataTable dt = new DataTable();
            EDFHandler edf = new EDFHandler();
            var temp = (measunit)BindingContext;
            using (var stream = await FileSystem.OpenAppPackageFileAsync("MEASUNIT.EDF"))
            {
                using (var reader = new StreamReader(stream))
                {
                    dt = edf.readEDF(reader);
                    reader.Close();
                }
                stream.Close();
            }
            var bc = (measunit)BindingContext;
            if (temp.MEASUREUNIT > dt.Rows.Count && bc.ID != 0)
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
                var deletedItem = (measunit)BindingContext;
                var action = await DisplayActionSheet("Urcite vymazat?", null, null, "Ano", "Nie");
                if (action.Equals("Ano") && action != null)
                {
                    await App.Database.deleteMEASUNIT(deletedItem);
                    int num = deletedItem.MEASUREUNIT - 1;
                    List<measunit> measunits = await App.Database.getMEASUNIT();
                    foreach (measunit a in measunits.Skip(num))
                    {
                        a.MEASUREUNIT -= 1;
                        await App.Database.updateMEASUNIT(a);
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
            var bc = (measunit)BindingContext;
            List<measunit> at = await App.Database.getMEASUNIT();
            foreach (measunit a in at)
            {
                if (bc.MEASUREUNIT == a.MEASUREUNIT)
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
                await App.Database.saveMEASUNIT((measunit)BindingContext);
                action = await DisplayActionSheet("Ulozene", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
            else if (suButton.Text.Equals("Update"))
            {

                await App.Database.updateMEASUNIT((measunit)BindingContext);
                action = await DisplayActionSheet("Aktualizovane", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
        }
    }
}