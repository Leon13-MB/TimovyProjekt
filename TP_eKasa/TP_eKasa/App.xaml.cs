using System;
using TP_eKasa.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TP_eKasa.Models;
using Xamarin.Essentials;
using System.IO;
using Java.IO;
using Android.Content.Res;
using System.Data;
using System.Collections.Generic;

namespace TP_eKasa
{
    public partial class App : Application
    {
        static Database database;
        public App()
        {
            try
            {
                InitializeComponent();
                MainPage = new NavigationPage(new MainPage());
            }
            catch (Exception e)
            {
                string ex = e.ToString();
            }
        }

        public static Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new Database();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //TU vytvorí v databaze defaultne tabulky, ak existuju nerobi nič
            EDFHandler edf = new EDFHandler();
            createDefaultData();
            //pripravy subory, ktore odosle na FTP pre zmenu nastaveni
            edf.createFiles();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            
        }

        

        public async void createDefaultData()
        {
            EDFHandler edf = new EDFHandler();
            DataTable dt = new DataTable();
            int dlzka = 0;
            //dpt
            List<dpt> dpts = new List<dpt>();
            dpts = await Database.getDPT();
            if (dpts.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("DPT.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                dpts.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    dpts.Add(new dpt
                    {
                        DPT = Convert.ToInt32(dt.Rows[i][0]),
                        DPT_NAME = dt.Rows[i][1].ToString()
                    });
                }
                foreach (dpt d in dpts)
                {
                    await Database.saveDPT(d);
                }
            }
            //advertxt
            List<advertxt> advertxts = new List<advertxt>();
            advertxts = await Database.getADVERTXT();
            if (advertxts.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("ADVERTXT.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                advertxts.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    advertxts.Add(new advertxt
                    {
                        ADVERTEXT = Convert.ToInt32(dt.Rows[i][0]),
                        ADVERTISEMENT_TEXT = dt.Rows[i][1].ToString(),
                        ADVERTISEMENT_TYPE = dt.Rows[i][2].ToString(),
                        ADVERTISEMENT_LOOKUP = dt.Rows[i][3].ToString()
                    });
                }
                foreach (advertxt d in advertxts)
                {
                    await Database.saveADVERTXT(d);
                }
            }
            //ecrloc
            List<ecrloc> ecrlocs = new List<ecrloc>();
            ecrlocs = await Database.getECRLOC();
            if (ecrlocs.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("ECRLOC.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                ecrlocs.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    ecrlocs.Add(new ecrloc
                    {
                        ECRLOC = Convert.ToInt32(dt.Rows[i][0]),
                        ECRLOC_LOCTYPE = dt.Rows[i][1].ToString(),
                        ECRLOC_LOCDESC = dt.Rows[i][2].ToString(),
                        ECRLOC_MUNICIPALITY = dt.Rows[i][3].ToString(),
                        ECRLOC_STREETNAME = dt.Rows[i][4].ToString(),
                        ECRLOC_BUILDINGNUMBER = dt.Rows[i][5].ToString(),
                        ECRLOC_PROPERTYREGNUMBER = dt.Rows[i][6].ToString(),
                        ECRLOC_POSTALCODE = dt.Rows[i][7].ToString()
                    });
                }
                foreach (ecrloc d in ecrlocs)
                {
                    await Database.saveECRLOC(d);
                }
            }
            //functext
            List<functext> functexts = new List<functext>();
            functexts = await Database.getFUNCTEXT();
            if (functexts.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("FUNCTEXT.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                functexts.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    functexts.Add(new functext
                    {
                        FUNCTEXT = Convert.ToInt32(dt.Rows[i][0]),
                        FUNCTEXT_REFERENCE_NUMBER_TEXT = dt.Rows[i][1].ToString()
                    });
                }
                foreach (functext d in functexts)
                {
                    await App.Database.saveFUNCTEXT(d);
                }
            }
            //measunit
            

            List<measunit> measunits = new List<measunit>();
            measunits = await Database.getMEASUNIT();
            
            if (measunits.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("MEASUNIT.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                measunits.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    measunits.Add(new measunit
                    {
                        MEASUREUNIT = Convert.ToInt32(dt.Rows[i][0]),
                        MEASUREUNIT_NAME = dt.Rows[i][1].ToString()
                    });
                }
                foreach (measunit d in measunits)
                {
                    await Database.saveMEASUNIT(d);
                }
            }
            //operator
            

            List<@operator> operators = new List<@operator>();
            operators = await Database.getOPERATOR();
            if (operators.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("OPERATOR.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                operators.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    operators.Add(new @operator
                    {
                        OPERATOR = Convert.ToInt32(dt.Rows[i][0]),
                        OPERATOR_NAME = dt.Rows[i][1].ToString(),
                        OPERATOR_PSWD = dt.Rows[i][2].ToString(),
                        OPERATOR_XMODE = dt.Rows[i][3].ToString(),
                        OPERATOR_ZMODE = dt.Rows[i][4].ToString(),
                        OPERATOR_PMODE = dt.Rows[i][5].ToString(),
                        OPERATOR_TMODE = dt.Rows[i][6].ToString(),
                        OPERATOR_REFUND = dt.Rows[i][7].ToString(),
                        OPERATOR_VOID = dt.Rows[i][8].ToString(),
                        OPERATOR_SURDISC = dt.Rows[i][9].ToString()
                    });
                }
                foreach (@operator d in operators)
                {
                    await Database.saveOPERATOR(d);
                }
            }
            //PLU
            List<plu> plus = new List<plu>();
            plus = await Database.getPLU();
            if (plus.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("PLU.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                plus.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    plus.Add(new plu
                    {
                        PLU = Convert.ToInt32(dt.Rows[i][0]),
                        PLU_PRICE = dt.Rows[i][1].ToString(),
                        PLU_NAME = dt.Rows[i][2].ToString(),
                        PLU_BARCODE = dt.Rows[i][3].ToString(),
                        PLU_TAXRATE = dt.Rows[i][4].ToString(),
                        PLU_SPEC_REG = dt.Rows[i][5].ToString(),
                        PLU_DPT = dt.Rows[i][6].ToString(),
                        PLU_UNIT = dt.Rows[i][7].ToString(),
                        PLU_LINK = dt.Rows[i][8].ToString(),
                        PLU_OTHERPRICE = dt.Rows[i][9].ToString(),
                        PLU_DESCRIPTOR = dt.Rows[i][10].ToString(),
                        PLU_CONTAINER = dt.Rows[i][11].ToString(),
                        PLU_STOCK = dt.Rows[i][12].ToString()
                    });
                }
                foreach (plu d in plus)
                {
                    await Database.savePLU(d);
                }
            }
            //SURDISC
            List<surdisc> surdiscs = new List<surdisc>();
            surdiscs = await Database.getSURDISC();
            if (surdiscs.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("SURDISC.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                surdiscs.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    surdiscs.Add(new surdisc
                    {
                        SURDISC = Convert.ToInt32(dt.Rows[i][0]),
                        PERCDISCRATE = dt.Rows[i][1].ToString(),
                        PERCDISCLIMIT = dt.Rows[i][2].ToString()
                    });
                }
                foreach (surdisc d in surdiscs)
                {
                    await Database.saveSURDISC(d);
                }
            }
            //TEXTLOGO
            List<textlogo> textlogos = new List<textlogo>();
            textlogos = await Database.getTEXTLOGO();
            if (textlogos.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("TEXTLOGO.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                textlogos.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    textlogos.Add(new textlogo
                    {
                        TEXTLOGO = Convert.ToInt32(dt.Rows[i][0]),
                        LOGOLINE1 = dt.Rows[i][1].ToString(),
                        LOGOLINE2 = dt.Rows[i][2].ToString(),
                        LOGOLINE3 = dt.Rows[i][3].ToString(),
                        LOGOLINE4 = dt.Rows[i][4].ToString(),
                        LOGOLINE5 = dt.Rows[i][5].ToString(),
                        LOGOLINE6 = dt.Rows[i][6].ToString(),
                        LOGOLINE7 = dt.Rows[i][7].ToString(),
                        LOGOLINE8 = dt.Rows[i][8].ToString(),
                        LOGOLINE9 = dt.Rows[i][9].ToString()
                    });
                }
                foreach (textlogo d in textlogos)
                {
                    await Database.saveTEXTLOGO(d);
                }
            }
            //traillog
            List<traillog> traillogs = new List<traillog>();
            traillogs = await Database.getTRAILLOG();
            if (traillogs.Count == 0)
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("TRAILLOG.EDF"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        dt = edf.readEDF(reader);
                        reader.Close();
                    }
                    stream.Close();
                }
                dlzka = dt.Rows.Count;
                traillogs.Clear();
                for (int i = 0; i < dlzka; i++)
                {
                    traillogs.Add(new traillog
                    {
                        TRAILERTEXTLOGO = Convert.ToInt32(dt.Rows[i][0]),
                        LOGOLINE1 = dt.Rows[i][1].ToString(),
                        LOGOLINE2 = dt.Rows[i][2].ToString(),
                        LOGOLINE3 = dt.Rows[i][3].ToString(),
                        LOGOLINE4 = dt.Rows[i][4].ToString(),
                        LOGOLINE5 = dt.Rows[i][5].ToString(),
                        LOGOLINE6 = dt.Rows[i][6].ToString(),
                        LOGOLINE7 = dt.Rows[i][7].ToString(),
                        LOGOLINE8 = dt.Rows[i][8].ToString(),
                        LOGOLINE9 = dt.Rows[i][9].ToString()
                    });
                }
                foreach (traillog d in traillogs)
                {
                    await Database.saveTRAILLOG(d);
                }
            }
        }
    }

 
}
