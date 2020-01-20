using Android.Content;
using Android.Net.Wifi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TP_eKasa.Models;
using TP_eKasa.Views;
using TP_eKasa.Controllers;
using Xamarin.Forms;
using System.Data;
using Xamarin.Essentials;

namespace TP_eKasa
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //public IList<ScanResult> list { get; private set; }
        WifiController wifi = new WifiController();
        Kasa kasa = new Kasa();
        BackupHandler handler = new BackupHandler();
        bool connected = false;

        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "KASA.txt"); 
            string content= "";
            using (var streamReader = new StreamReader(filename))
            {
                content = streamReader.ReadToEnd();
            }
            image.Source = Device.RuntimePlatform == Device.Android
                ? ImageSource.FromFile(content+".png")
                : ImageSource.FromFile("Images/"+content+".png");
            checkConnectivity();
        }

        public async void tables(object sender, EventArgs e)
        {
            //if (connected)
            //{
            //    await Navigation.PushAsync(new Tables());
            //}
            //else
            //{
            //    checkConnectivity();
            //}

            await Navigation.PushAsync(new Tables());
        }

        public async void upload(object sender, EventArgs e)
        {
            //if (connected)
            //{
            //    await Navigation.PushAsync(new Tables());
            //}
            //else
            //{
            //    checkConnectivity();
            //}

            await Navigation.PushAsync(new Upload());
        }


        // -----  Logout -----

        [Obsolete]
        public async void LogoutProcedure(object sender, EventArgs e)
        {
            await DisplayAlert("Odhlásenie", "Budete odhlásení z aplikácie", "Okej");
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }

        public void checkConnectivity()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://icm.local/Services?Ecrinfo");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response != null)
                {
                    connected = true;
                    loadBackup(kasa.getTypeOfKasa());
                }
            }
            catch (Exception)
            {
                connected = false;
                DisplayAlert("Pripojenie", "Nie ste pripojeny k pokladni", "OK");
            }
        }

        public async void loadBackup(string Kasa)
        {
            string action = "";
            var kasa = App.Database.getKasaPLU(Kasa);
            if (connected && kasa==null)
            {
                action = await DisplayActionSheet("Aplikácia nemá profil pre danú pokladňu, prajete si stiahnúť profil z pokladne?", "Cancel", null, "Áno", "Nie");
                if(action != null)
                {
                    if (action.Equals("Áno"))
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://icm.local/Services?Backup");
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream resStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(resStream, Encoding.Unicode);
                        string text = reader.ReadToEnd();
                        List<string> tables = handler.readToListofTables(text);
                        List<DataTable> dataTables = new List<DataTable>();
                        foreach(string t in tables)
                        {
                            dataTables.Add(handler.stringToDT(t));
                        }
                    }
                    else if (action.Equals("Nie"))
                    {
                        await DisplayAlert("Upozornenie","Dáta neboli načítané","OK");
                    }
                }
            }
        }

        public async void test(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "KASA.txt");
            string content = "";
            using (var streamReader = new StreamReader(filename))
            {
                content = streamReader.ReadToEnd();
            }
            string text = "";
            using (var stream = await FileSystem.OpenAppPackageFileAsync("backup.tsv"))
            {
                using (var reader = new StreamReader(stream))
                {
                    UTF8Encoding uTF8 = new UTF8Encoding();
                    byte[] output = uTF8.GetBytes(reader.ReadToEnd());
                    string tempString = Encoding.ASCII.GetString(output, 0, output.Length);
                    text = tempString;

                    reader.Close();
                }
                stream.Close();
            }
            List<string> tables = handler.readToListofTables(text);
            List<DataTable> dataTables = new List<DataTable>();
            foreach (string t in tables)
            {
                dataTables.Add(handler.stringToDT(t));
            }
            var kasa = await App.Database.getKasaPLU(content);
            if(kasa == null)
            {
                string action = await DisplayActionSheet("Aplikácia nemá profil pre danú pokladňu, prajete si stiahnúť profil z pokladne?", "Cancel", null, "Áno", "Nie");
                if(action.Equals("Áno"))
                {
                    loading.IsEnabled = true;
                    loading.IsVisible = true;
                    loading.IsRunning = true;
                    loadingLBL.IsVisible = true;
                    foreach (DataTable d in dataTables)
                    {
                        if (d.TableName.Equals("PLU"))
                        {
                            List<plu> plus = await App.Database.getPLU();
                            int dlzka = d.Rows.Count;
                            plus.Clear();
                            string[] columnNames = d.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
                            for (int i = 0; i < dlzka; i++)
                            {
                                plus.Add(new plu
                                {
                                    PLU = Convert.ToInt32(d.Rows[i][0]),
                                    PLU_PRICE = d.Rows[i][1].ToString(),
                                    PLU_NAME = d.Rows[i][2].ToString(),
                                    PLU_BARCODE = d.Rows[i][3].ToString(),
                                    PLU_TAXRATE = d.Rows[i][4].ToString(),
                                    PLU_SPEC_REG = d.Rows[i][5].ToString(),
                                    PLU_DPT = d.Rows[i][6].ToString(),
                                    PLU_UNIT = d.Rows[i][7].ToString(),
                                    PLU_LINK = d.Rows[i][8].ToString(),
                                    PLU_OTHERPRICE = d.Rows[i][9].ToString(),
                                    PLU_DESCRIPTOR = d.Rows[i][10].ToString(),
                                    PLU_CONTAINER = d.Rows[i][11].ToString(),
                                    KASA = content,
                                });
                            }
                            foreach (plu p in plus)
                            {
                                await App.Database.savePLU(p);
                            }
                        }
                        if (d.TableName.Equals("DPT"))
                        {
                            List<dpt> dpts = await App.Database.getDPT();
                            int dlzka = d.Rows.Count;
                            dpts.Clear();
                            for (int i = 0; i < dlzka; i++)
                            {
                                dpts.Add(new dpt
                                {
                                    DPT = Convert.ToInt32(d.Rows[i][0]),
                                    DPT_NAME = d.Rows[i][1].ToString(),
                                    KASA = content
                                });
                            }
                            foreach (dpt p in dpts)
                            {
                                await App.Database.saveDPT(p);
                            }
                        }
                        if (d.TableName.Equals("OPERATOR"))
                        {
                            List<@operator> operators = await App.Database.getOPERATOR();
                            int dlzka = d.Rows.Count;
                            operators.Clear();
                            for (int i = 0; i < dlzka; i++)
                            {
                                operators.Add(new @operator
                                {
                                    OPERATOR = Convert.ToInt32(d.Rows[i][0]),
                                    OPERATOR_NAME = d.Rows[i][1].ToString(),
                                    OPERATOR_PSWD = d.Rows[i][2].ToString(),
                                    OPERATOR_XMODE = d.Rows[i][3].ToString(),
                                    OPERATOR_ZMODE = d.Rows[i][4].ToString(),
                                    OPERATOR_PMODE = d.Rows[i][5].ToString(),
                                    OPERATOR_TMODE = d.Rows[i][6].ToString(),
                                    OPERATOR_REFUND = d.Rows[i][7].ToString(),
                                    OPERATOR_VOID = d.Rows[i][8].ToString(),
                                    OPERATOR_SURDISC = d.Rows[i][9].ToString(),
                                    KASA = content
                                });
                            }
                            foreach (@operator p in operators)
                            {
                                await App.Database.saveOPERATOR(p);
                            }
                        }
                        if (d.TableName.Equals("MEASUREUNIT"))
                        {
                            List<measunit> measunits = await App.Database.getMEASUNIT();
                            int dlzka = d.Rows.Count;
                            measunits.Clear();
                            for (int i = 0; i < dlzka; i++)
                            {
                                measunits.Add(new measunit
                                {
                                    MEASUREUNIT = Convert.ToInt32(d.Rows[i][0]),
                                    MEASUREUNIT_NAME = d.Rows[i][1].ToString(),
                                    KASA = content
                                });
                            }
                            foreach (measunit p in measunits)
                            {
                                await App.Database.saveMEASUNIT(p);
                            }
                        }
                        if (d.TableName.Equals("SURDISC"))
                        {
                            List<surdisc> surdiscs = await App.Database.getSURDISC();
                            int dlzka = d.Rows.Count;
                            surdiscs.Clear();
                            for (int i = 0; i < dlzka; i++)
                            {
                                surdiscs.Add(new surdisc
                                {
                                    SURDISC = Convert.ToInt32(d.Rows[i][0]),
                                    PERCDISCRATE = d.Rows[i][1].ToString(),
                                    PERCDISCLIMIT = d.Rows[i][2].ToString(),
                                    KASA = content
                                });
                            }
                            foreach (surdisc p in surdiscs)
                            {
                                await App.Database.saveSURDISC(p);
                            }
                        }
                        if (d.TableName.Equals("TEXTLOGO"))
                        {
                            List<textlogo> textlogos = await App.Database.getTEXTLOGO();
                            int dlzka = d.Rows.Count;
                            textlogos.Clear();
                            for (int i = 0; i < dlzka; i++)
                            {
                                textlogos.Add(new textlogo
                                {
                                    TEXTLOGO = Convert.ToInt32(d.Rows[i][0]),
                                    LOGOLINE1 = d.Rows[i][1].ToString(),
                                    LOGOLINE2 = d.Rows[i][2].ToString(),
                                    LOGOLINE3 = d.Rows[i][3].ToString(),
                                    LOGOLINE4 = d.Rows[i][4].ToString(),
                                    LOGOLINE5 = d.Rows[i][5].ToString(),
                                    LOGOLINE6 = d.Rows[i][6].ToString(),
                                    LOGOLINE7 = d.Rows[i][7].ToString(),
                                    LOGOLINE8 = d.Rows[i][8].ToString(),
                                    LOGOLINE9 = d.Rows[i][9].ToString(),
                                    KASA = content
                                });
                            }
                            foreach (textlogo p in textlogos)
                            {
                                await App.Database.saveTEXTLOGO(p);
                            }
                        }
                        if (d.TableName.Equals("FUNCTEXT"))
                        {
                            List<functext> functexts = await App.Database.getFUNCTEXT();
                            int dlzka = d.Rows.Count;
                            functexts.Clear();
                            for (int i = 0; i < dlzka; i++)
                            {
                                functexts.Add(new functext
                                {
                                    FUNCTEXT = Convert.ToInt32(d.Rows[i][0]),
                                    FUNCTEXT_REFERENCE_NUMBER_TEXT = d.Rows[i][1].ToString(),
                                    KASA = content
                                });
                            }
                            foreach (functext p in functexts)
                            {
                                await App.Database.saveFUNCTEXT(p);
                            }
                        }
                        if (d.TableName.Equals("ECRLOC"))
                        {
                            List<ecrloc> ecrlocs = await App.Database.getECRLOC();
                            int dlzka = d.Rows.Count;
                            ecrlocs.Clear();
                            for (int i = 0; i < dlzka; i++)
                            {
                                ecrlocs.Add(new ecrloc
                                {
                                    ECRLOC = Convert.ToInt32(d.Rows[i][0]),
                                    ECRLOC_LOCTYPE = d.Rows[i][1].ToString(),
                                    ECRLOC_LOCDESC = d.Rows[i][2].ToString(),
                                    ECRLOC_MUNICIPALITY = d.Rows[i][3].ToString(),
                                    ECRLOC_STREETNAME = d.Rows[i][4].ToString(),
                                    ECRLOC_BUILDINGNUMBER = d.Rows[i][5].ToString(),
                                    ECRLOC_PROPERTYREGNUMBER = d.Rows[i][6].ToString(),
                                    ECRLOC_POSTALCODE = d.Rows[i][7].ToString(),
                                    KASA = content
                                });
                            }
                            foreach (ecrloc p in ecrlocs)
                            {
                                await App.Database.saveECRLOC(p);
                            }
                        }
                        if (d.TableName.Equals("TRAILLOG"))
                        {
                            List<traillog> traillogs = await App.Database.getTRAILLOG();
                            int dlzka = d.Rows.Count;
                            traillogs.Clear();
                            for (int i = 0; i < dlzka; i++)
                            {
                                traillogs.Add(new traillog
                                {
                                    TRAILERTEXTLOGO = Convert.ToInt32(d.Rows[i][0]),
                                    LOGOLINE1 = d.Rows[i][1].ToString(),
                                    LOGOLINE2 = d.Rows[i][2].ToString(),
                                    LOGOLINE3 = d.Rows[i][3].ToString(),
                                    LOGOLINE4 = d.Rows[i][4].ToString(),
                                    LOGOLINE5 = d.Rows[i][5].ToString(),
                                    LOGOLINE6 = d.Rows[i][6].ToString(),
                                    LOGOLINE7 = d.Rows[i][7].ToString(),
                                    LOGOLINE8 = d.Rows[i][8].ToString(),
                                    LOGOLINE9 = d.Rows[i][9].ToString(),
                                    KASA = content
                                });
                            }
                            foreach (traillog p in traillogs)
                            {
                                await App.Database.saveTRAILLOG(p);
                            }
                        }
                        if (d.TableName.Equals("ADVERTXT"))
                        {
                            List<advertxt> advertxts = await App.Database.getADVERTXT();
                            int dlzka = d.Rows.Count;
                            advertxts.Clear();
                            for (int i = 0; i < dlzka; i++)
                            {
                                advertxts.Add(new advertxt
                                {
                                    ADVERTEXT = Convert.ToInt32(d.Rows[i][0]),
                                    ADVERTISEMENT_TEXT = d.Rows[i][1].ToString(),
                                    ADVERTISEMENT_TYPE = d.Rows[i][2].ToString(),
                                    ADVERTISEMENT_LOOKUP = d.Rows[i][3].ToString(),
                                    KASA = content
                                });
                            }
                            foreach (advertxt p in advertxts)
                            {
                                await App.Database.saveADVERTXT(p);
                            }
                        }
                    }
                }
                loading.IsEnabled = false;
                loading.IsVisible = false;
                loading.IsRunning = false;
                loadingLBL.IsVisible = false;
            }
        }
    }
}
