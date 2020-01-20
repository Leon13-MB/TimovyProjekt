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
    public partial class SurdiscView : ContentPage
    {
        public SurdiscView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            addSaveOrUpdate();
            DataTable dt = new DataTable();
            BackupHandler edf = new BackupHandler();
            var temp = (surdisc)BindingContext;
            using (var stream = await FileSystem.OpenAppPackageFileAsync("SURDISC.EDF"))
            {
                using (var reader = new StreamReader(stream))
                {
                    dt = edf.readEDF(reader);
                    reader.Close();
                }
                stream.Close();
            }
            var bc = (surdisc)BindingContext;
            if (temp.SURDISC > dt.Rows.Count && bc.ID != 0)
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
                var deletedItem = (surdisc)BindingContext;
                var action = await DisplayActionSheet("Urcite vymazat?", null, null, "Ano", "Nie");
                if (action.Equals("Ano") && action != null)
                {
                    await App.Database.deleteSURDISC(deletedItem);
                    int num = deletedItem.SURDISC - 1;
                    List<surdisc> surdiscs = await App.Database.getSURDISC();
                    foreach (surdisc a in surdiscs.Skip(num))
                    {
                        a.SURDISC -= 1;
                        await App.Database.updateSURDISC(a);
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
            var bc = (surdisc)BindingContext;
            List<surdisc> at = await App.Database.getSURDISC();
            foreach (surdisc a in at)
            {
                if (bc.SURDISC == a.SURDISC)
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
                await App.Database.saveSURDISC((surdisc)BindingContext);
                action = await DisplayActionSheet("Ulozene", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
            else if (suButton.Text.Equals("Update"))
            {

                await App.Database.updateSURDISC((surdisc)BindingContext);
                action = await DisplayActionSheet("Aktualizovane", "OK", null);
                if (action.Equals("OK"))
                {
                    saveEDF();
                    await Navigation.PopAsync();
                }
                else
                {
                    saveEDF();
                    await Navigation.PopAsync();
                }
            }
        }
        //odosle request POST s fileom PLU.EDF
        void sendRequest(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "SURDISC.EDF");
            string content = "";
            using (var streamReader = new StreamReader(filename))
            {
                content = streamReader.ReadToEnd();
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://icm.local/Services?Restore");
            request.Method = "POST";
            byte[] bytes = Encoding.UTF8.GetBytes(content);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
        }
        //backup vlozi do labelu pod tlacidlom
        void getBackup(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://icm.local/Services?Backup");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);

            string txt = reader.ReadToEnd();
            Backup.Text = txt;
            /*TU JE REQUEST PRE ZISKANIE ID KASY A POCTU POLOZIEK
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://icm.local/Services?Ecrinfo");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);

            string txt = reader.ReadToEnd();
            string[] temp = txt.Split('\t');
            string IDKASY = temp[0];
            string POCETPOLOZIEK = temp[1];
             
             */
        }

        public void saveEDF()
        {
            var savedItem = (surdisc)BindingContext;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "SURDISC.EDF");
            string data = savedItem.SURDISC.ToString() + "\t" + savedItem.PERCDISCRATE + "\t" + savedItem.PERCDISCLIMIT;
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