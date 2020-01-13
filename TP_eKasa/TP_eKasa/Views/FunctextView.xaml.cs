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
    public partial class FunctextView : ContentPage
    {
        public FunctextView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            addSaveOrUpdate();
            DataTable dt = new DataTable();
            EDFHandler edf = new EDFHandler();
            var temp = (functext)BindingContext;
            using (var stream = await FileSystem.OpenAppPackageFileAsync("FUNCTEXT.EDF"))
            {
                using (var reader = new StreamReader(stream))
                {
                    dt = edf.readEDF(reader);
                    reader.Close();
                }
                stream.Close();
            }
            var bc = (functext)BindingContext;
            if (temp.FUNCTEXT > dt.Rows.Count && bc.ID != 0)
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
                var deletedItem = (functext)BindingContext;
                var action = await DisplayActionSheet("Urcite vymazat?", null, null, "Ano", "Nie");
                if (action.Equals("Ano") && action != null)
                {
                    await App.Database.deleteFUNCTEXT(deletedItem);
                    int num = deletedItem.FUNCTEXT - 1;
                    List<functext> functexts = await App.Database.getFUNCTEXT();
                    foreach (functext a in functexts.Skip(num))
                    {
                        a.FUNCTEXT -= 1;
                        await App.Database.updateFUNCTEXT(a);
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
            var bc = (functext)BindingContext;
            List<functext> at = await App.Database.getFUNCTEXT();
            foreach (functext a in at)
            {
                if (bc.FUNCTEXT == a.FUNCTEXT)
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
                await App.Database.saveFUNCTEXT((functext)BindingContext);
                action = await DisplayActionSheet("Ulozene", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
            else if (suButton.Text.Equals("Update"))
            {

                await App.Database.updateFUNCTEXT((functext)BindingContext);
                action = await DisplayActionSheet("Aktualizovane", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
        }
    }
}