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
    public partial class OperatorView : ContentPage
    {
        public OperatorView()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            DataTable dt = new DataTable();
            BackupHandler edf = new BackupHandler();
            var temp = (@operator)BindingContext;
            using (var stream = await FileSystem.OpenAppPackageFileAsync("OPERATOR.EDF"))
            {
                using (var reader = new StreamReader(stream))
                {
                    dt = edf.readEDF(reader);
                    reader.Close();
                }
                stream.Close();
            }
        }
    }
}