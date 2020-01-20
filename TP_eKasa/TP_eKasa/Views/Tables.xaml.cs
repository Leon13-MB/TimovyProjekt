using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tables : ContentPage
    {
        public Tables()
        {
            InitializeComponent();
        }

        private async void Advertxt(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Advertxt());
        }
        private async void Dpt(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Dpt());
        }
        private async void Ecrloc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ecrloc());
        }
        private async void Functext(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Functext());
        }
        private async void Measunit(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Measunit());
        }
        private async void Operator(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Operator());
        }
        private async void Plu(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Plu());
        }
        private async void Surdisc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Surdisc());
        }
        private async void Textlogo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Textlogo());
        }
        private async void Triallog(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Traillog());
        }
        private async void UpdatedPlu(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdatedPLU());
        }
    }
}