using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using TP_eKasa.Models;
using System.Collections.ObjectModel;
using System;

namespace TP_eKasa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FTP : ContentPage
    {
        public FTP()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            dptView.ItemsSource = await App.Database.getDPT();
        }


        async void Save(object sender, EventArgs e)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create("ftp://192.168.2.9/tabs/DPT.EDF");
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            ftpWebRequest.Credentials = new NetworkCredential("Tu_ide_prihl_meno_do_FTP", "heslo");

            FtpWebResponse res = (FtpWebResponse)ftpWebRequest.GetResponse();

            Stream resStream = res.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            DataTable datatable = new DataTable();
            BackupHandler edf = new BackupHandler();
            datatable = edf.readEDF(reader);
            List<dpt> dpts = new List<dpt>();

            int dlzka = datatable.Rows.Count;

            for (int i = 0; i < dlzka; i++)
            {
                dpts.Add(new dpt
                {
                    DPT = Convert.ToInt32(datatable.Rows[i][0]),
                    DPT_NAME = datatable.Rows[i][1].ToString()
                });
            }
            
            foreach (dpt d in dpts)
            {
                await App.Database.saveDPT(d);
            }
            res.Close();
            resStream.Close();
        }
        
    }
}