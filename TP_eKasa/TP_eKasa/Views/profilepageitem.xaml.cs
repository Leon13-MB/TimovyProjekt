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
    public partial class profilepageitem : ContentPage
    {
        public profilepageitem()
        {
            InitializeComponent();
        }

        async void Save(object sender, EventArgs e)
        {
            bool existuje = false;
            var xx = (Profile)BindingContext;
            if (xx.ID == 1)
            {
                await DisplayAlert("UPDATE", "NIE JE MOZNE VYKONAT UPDATE NAD DEFAULTNYM NASTAVENIM", "OK");
            }
            else
            {
                List<Profile> yy = await App.Database.GetProfiles();
                foreach (var pr in yy)
                {
                    if (pr.TableName == xx.TableName)
                    { 
                        existuje = true;
                        await DisplayAlert("Save", "Uz sa nachadza nazov", "OK");
                    }
                    else
                    {
                        existuje = false;
                    }
                }
                if (!existuje)
                {
                    xx.ID = App.Database.GetProfiles().Result.Count() + 1;
                    await App.Database.ProfileSave(xx);
                    await Navigation.PopAsync();
                }
            }
        }
        async void Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async void Delete(object sender, EventArgs e)
        {
            await App.Database.DeleteProfile((Profile)BindingContext);
            await Navigation.PopAsync();
        }
    }
}