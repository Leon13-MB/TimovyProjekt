﻿using System;
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
    public partial class TriallogView : ContentPage
    {
        public TriallogView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            addSaveOrUpdate();
            DataTable dt = new DataTable();
            BackupHandler edf = new BackupHandler();
            var temp = (traillog)BindingContext;
            using (var stream = await FileSystem.OpenAppPackageFileAsync("TRAILLOG.EDF"))
            {
                using (var reader = new StreamReader(stream))
                {
                    dt = edf.readEDF(reader);
                    reader.Close();
                }
                stream.Close();
            }
            var bc = (traillog)BindingContext;
            if (temp.TRAILERTEXTLOGO > dt.Rows.Count && bc.ID != 0)
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
                var deletedItem = (traillog)BindingContext;
                var action = await DisplayActionSheet("Urcite vymazat?", null, null, "Ano", "Nie");
                if (action.Equals("Ano") && action != null)
                {
                    await App.Database.deleteTRAILLOG(deletedItem);
                    int num = deletedItem.TRAILERTEXTLOGO - 1;
                    List<traillog> traillogs = await App.Database.getTRAILLOG();
                    foreach (traillog a in traillogs.Skip(num))
                    {
                        a.TRAILERTEXTLOGO -= 1;
                        await App.Database.updateTRAILLOG(a);
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
            var bc = (traillog)BindingContext;
            List<traillog> at = await App.Database.getTRAILLOG();
            foreach (traillog a in at)
            {
                if (bc.TRAILERTEXTLOGO == a.TRAILERTEXTLOGO)
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
                await App.Database.saveTRAILLOG((traillog)BindingContext);
                action = await DisplayActionSheet("Ulozene", "OK", null);
                if (action.Equals("OK") && action != null)
                {
                    await Navigation.PopAsync();
                }
            }
            else if (suButton.Text.Equals("Update"))
            {

                await App.Database.updateTRAILLOG((traillog)BindingContext);
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
            string filename = Path.Combine(path, "TRAILLOG.EDF");
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
            var savedItem = (traillog)BindingContext;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "TRAILLOG.EDF");
            string data = savedItem.TRAILERTEXTLOGO.ToString() + "\t" + savedItem.LOGOLINE1 + "\t" + savedItem.LOGOLINE2 + "\t" +
                savedItem.LOGOLINE3 + "\t" + savedItem.LOGOLINE4 + "\t" + savedItem.LOGOLINE5 + "\t" + savedItem.LOGOLINE6 + "\t" +
                savedItem.LOGOLINE7 + "\t" + savedItem.LOGOLINE8 + "\t" + savedItem.LOGOLINE9;
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