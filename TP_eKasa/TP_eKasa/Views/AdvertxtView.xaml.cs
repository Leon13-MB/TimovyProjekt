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
    public partial class AdvertxtView : ContentPage
    {
        public AdvertxtView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            addSaveOrUpdate();
            DataTable dt = new DataTable();
            EDFHandler edf = new EDFHandler();
            var temp = (advertxt)BindingContext;
            using (var stream = await FileSystem.OpenAppPackageFileAsync("ADVERTXT.EDF"))
            {
                using (var reader = new StreamReader(stream))
                {
                    dt = edf.readEDF(reader);
                    reader.Close();
                }
                stream.Close();
            }
            var bc = (advertxt)BindingContext;
            if (temp.ADVERTEXT > dt.Rows.Count && bc.ID != 0)
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
                var deletedItem = (advertxt)BindingContext;
                var action = await DisplayActionSheet("Urcite vymazat?", null, null,"Ano","Nie");
                if (action.Equals("Ano") && action != null)
                {
                    await App.Database.deleteADVERTXT(deletedItem);
                    int num = deletedItem.ADVERTEXT-1;
                    List<advertxt> advertxts = await App.Database.getADVERTXT();
                    foreach(advertxt a in advertxts.Skip(num))
                    {
                        a.ADVERTEXT -= 1;
                        await App.Database.updateADVERTXT(a);
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
            var bc = (advertxt)BindingContext;
            List<advertxt> at = await App.Database.getADVERTXT();
            foreach (advertxt a in at)
            {
                if (bc.ADVERTEXT == a.ADVERTEXT)
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
                await App.Database.saveADVERTXT((advertxt)BindingContext);

                action = await DisplayActionSheet("Ulozene", null, null, "OK");
                if (action.Equals("OK"))
                {
                    saveEDF();
                    await Navigation.PopAsync();
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
            else if (suButton.Text.Equals("Update"))
            {

                await App.Database.updateADVERTXT((advertxt)BindingContext);
                action = await DisplayActionSheet("Aktualizovane", null, null, "OK");
                if (action.Equals("OK"))
                {
                    saveEDF();
                    await Navigation.PopAsync();
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
        }

        public void saveEDF()
        {
            var savedItem = (advertxt)BindingContext;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "ADVERTXT.EDF");
            string data = savedItem.ADVERTEXT.ToString()+ "\t" + savedItem.ADVERTISEMENT_TEXT + "\t" + savedItem.ADVERTISEMENT_TYPE + "\t" + savedItem.ADVERTISEMENT_LOOKUP;
            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(data);
            }

            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
            }

            //string EDF = createEDF(dt);
            //FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(path);
            //ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

            //ftpWebRequest.Credentials = new NetworkCredential(textBox1.Text, textBox2.Text);

            //byte[] bytes = Encoding.UTF8.GetBytes(EDF);

            //Stream requestStream = ftpWebRequest.GetRequestStream();
            //requestStream.Write(bytes, 0, bytes.Length);
            //requestStream.Close();

        }
    }
}