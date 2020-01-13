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
    public partial class EcrlocaView : ContentPage
    {
        public EcrlocaView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            addSaveOrUpdate();
            DataTable dt = new DataTable();
            EDFHandler edf = new EDFHandler();
            var temp = (ecrloc)BindingContext;
            using (var stream = await FileSystem.OpenAppPackageFileAsync("ECRLOC.EDF"))
            {
                using (var reader = new StreamReader(stream))
                {
                    dt = edf.readEDF(reader);
                    reader.Close();
                }
                stream.Close();
            }
            var bc = (ecrloc)BindingContext;
            if (temp.ECRLOC > dt.Rows.Count && bc.ID != 0)
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
                var deletedItem = (ecrloc)BindingContext;
                var action = await DisplayActionSheet("Urcite vymazat?", null, null, "Ano", "Nie");
                if (action.Equals("Ano") && action != null)
                {
                    await App.Database.deleteECRLOC(deletedItem);
                    int num = deletedItem.ECRLOC - 1;
                    List<ecrloc> ecrlocs = await App.Database.getECRLOC();
                    foreach (ecrloc a in ecrlocs.Skip(num))
                    {
                        a.ECRLOC -= 1;
                        await App.Database.updateECRLOC(a);
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
            var bc = (ecrloc)BindingContext;
            List<ecrloc> at = await App.Database.getECRLOC();
            foreach (ecrloc a in at)
            {
                if (bc.ECRLOC == a.ECRLOC)
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
                await App.Database.saveECRLOC((ecrloc)BindingContext);
                action = await DisplayActionSheet("Ulozene", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
            else if (suButton.Text.Equals("Update"))
            {

                await App.Database.updateECRLOC((ecrloc)BindingContext);
                action = await DisplayActionSheet("Aktualizovane", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
        }
    }
}