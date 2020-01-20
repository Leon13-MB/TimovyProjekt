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
using System.Net;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PluView : ContentPage
    {
        public PluView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            addSaveOrUpdate();
        }

        public void addSaveOrUpdate()
        {
            var bc = (plu)BindingContext;
            if (bc.PLU_NAME.Equals("NaN"))
            {
                suButton.Text = "Save";
            }
            else
            {
                suButton.Text = "Update";
            }
        }

        async void buttonClicked(object sender, EventArgs e)
        {
            var bc = (plu)BindingContext;
            var bc1 = new updatedPlu();
            bc1.PLU = bc.PLU;
            bc1.PLU_BARCODE = bc.PLU_BARCODE;
            bc1.PLU_CONTAINER = bc.PLU_CONTAINER;
            bc1.PLU_DESCRIPTOR = bc.PLU_DESCRIPTOR;
            bc1.PLU_DPT = bc.PLU_DPT;
            bc1.PLU_LINK = bc.PLU_LINK;
            bc1.PLU_NAME = bc.PLU_NAME;
            bc1.PLU_OTHERPRICE = bc.PLU_OTHERPRICE;
            bc1.PLU_PRICE = bc.PLU_PRICE;
            bc1.PLU_SPEC_REG = bc.PLU_SPEC_REG;
            bc1.PLU_TAXRATE = bc.PLU_TAXRATE;
            bc1.PLU_UNIT = bc.PLU_UNIT;
            bc1.KASA = bc.KASA;
            bc1.selected = false;

            if (bc.PLU_NAME.Equals("NaN"))
            {
                await DisplayAlert("Warning", "Musíte zadať meno", "OK");
            }
            else
            {
                if (bc.PLU_PRICE.Equals("NaN"))
                {
                    bc.PLU_PRICE = "0.00";
                }
                else if (bc.PLU_PRICE.Contains(','))
                {
                    bc.PLU_PRICE.Replace(',', '.');
                }
                else if(!bc.PLU_PRICE.Contains('.'))
                {
                    bc.PLU_PRICE += ".00";
                }
                await App.Database.updatePLU(bc);
                await App.Database.saveUpdatedPLU(bc1);
                await Navigation.PopAsync();
            }
        }

    }
}