using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_eKasa.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP_eKasa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Conf1Items : ContentPage
    {
        public Conf1Items()
        {
            InitializeComponent();
        }

        async void Save(object sender,EventArgs e)
        {
            var cf1 = (Configuration1)BindingContext;
            await App.Database.DeleteConf1ItemAsync((Configuration1)BindingContext);
            await App.Database.SaveConfiguration1Async(cf1);
            await Navigation.PopAsync();
        }
        async void Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async void Delete(object sender, EventArgs e)
        {
            await App.Database.DeleteConf1ItemAsync((Configuration1)BindingContext);
            await Navigation.PopAsync();
        }
    }
}