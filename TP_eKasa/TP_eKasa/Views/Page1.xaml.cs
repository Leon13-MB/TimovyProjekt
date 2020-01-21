using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TP_eKasa.Models;
using SQLite;
using System.Data;
using System.ComponentModel;
using TP_eKasa.Controllers;
using System.IO;
using System.Net;

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        Kasa kasa = new Kasa();
        public Page1()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //List<plu> plus = await App.Database.getPluWithName();
            //List<string> k = new List<string>();
            //foreach (plu p in plus)
            //{
            //    k.Add(p.KASA);
            //    //if(k.Count == 0)
            //    //{
            //    //    k.Add(p.KASA);
            //    //}
            //    //else
            //    //{
            //    //    foreach(string l in k)
            //    //    {
            //    //        if (!l.Equals(p.KASA))
            //    //        {
            //    //            k.Add(p.KASA);
            //    //        }
            //    //    }
            //    //}
            //}
            //pickerKasa.ItemsSource = k;
        }

        public async void test(object sender, EventArgs e)
        {
            if (pickerKasa.SelectedItem != null)
            {
                string kk = kasa.getTypeOfKasa();
                List<dpt> dpts = await App.Database.getDPT();
                string dptstring = "DPT\tDPT_NAME\r\n";
                foreach (dpt d in dpts)
                {
                    if (d.KASA.Equals(kasa.getTypeOfKasa()))
                    {
                        dptstring += d.DPT.ToString() + "\t" + d.DPT_NAME + "\r\n";
                    }
                }
                List<advertxt> advertxts = await App.Database.getADVERTXT();
                string advertxtstring = "ADVERTEXT\tADVERTISEMENT_TEXT\tADVERTISEMENT_TYPE\tADVERTISEMENT_LOOKUP\r\n";
                foreach (advertxt d in advertxts)
                {
                    if (d.KASA.Equals(kasa.getTypeOfKasa()))
                    {
                        advertxtstring += d.ADVERTEXT.ToString() + "\t" + d.ADVERTISEMENT_TEXT + "\t" + d.ADVERTISEMENT_TYPE + "\t" + d.ADVERTISEMENT_LOOKUP + "\r\n";
                    }
                }
                List<ecrloc> ecrlocs = await App.Database.getECRLOC();
                string ecrlocstring = "ECRLOC\tECRLOC_LOCTYPE\tECRLOC_LOCDESC\tECRLOC_MUNICIPALITY\tECRLOC_STREETNAME\tECRLOC_BUILDINGNUMBER\tECRLOC_PROPERTYREGNUMBER\tECRLOC_POSTALCODE\r\n";
                foreach (ecrloc d in ecrlocs)
                {
                    if (d.KASA.Equals(kasa.getTypeOfKasa()))
                    {
                        ecrlocstring += d.ECRLOC.ToString() + "\t" + d.ECRLOC_LOCTYPE + "\t" + d.ECRLOC_LOCDESC + "\t" +
                            d.ECRLOC_MUNICIPALITY + "\t" + d.ECRLOC_STREETNAME + "\t" + d.ECRLOC_BUILDINGNUMBER + "\t" +
                            d.ECRLOC_PROPERTYREGNUMBER + "\t" + d.ECRLOC_POSTALCODE + "\r\n";
                    }
                }
                List<functext> functexts = await App.Database.getFUNCTEXT();
                string functextstring = "FUNCTEXT\tFUNCTEXT_REFERENCE_NUMBER_TEXT\r\n";
                foreach (functext d in functexts)
                {
                    if (d.KASA.Equals(kasa.getTypeOfKasa()))
                    {
                        functextstring += d.FUNCTEXT.ToString() + "\t" + d.FUNCTEXT_REFERENCE_NUMBER_TEXT + "\r\n";
                    }
                }
                List<measunit> measunits = await App.Database.getMEASUNIT();
                string measunitstring = "MEASUREUNIT\tMEASUREUNIT_NAME\r\n";
                foreach (measunit d in measunits)
                {
                    if (d.KASA.Equals(kasa.getTypeOfKasa()))
                    {
                        measunitstring += d.MEASUREUNIT.ToString() + "\t" + d.MEASUREUNIT_NAME + "\r\n";
                    }
                }
                List<@operator> operators = await App.Database.getOPERATOR();
                string operatorstring = "OPERATOR\tOPERATOR_NAME\tOPERATOR_PSWD\tOPERATOR_XMODE\tOPERATOR_ZMODE\tOPERATOR_PMODE\tOPERATOR_TMODE\tOPERATOR_REFUND\tOPERATOR_VOID\tOPERATOR_SURDISC\r\n";
                foreach (@operator d in operators)
                {
                    if (d.KASA.Equals(kasa.getTypeOfKasa()))
                    {
                        operatorstring += d.OPERATOR.ToString() + "\t" + d.OPERATOR_NAME + "\t" + d.OPERATOR_PSWD + "\t" +
                            d.OPERATOR_XMODE + "\t" + d.OPERATOR_ZMODE + "\t" + d.OPERATOR_PMODE + "\t" + d.OPERATOR_TMODE + "\t" +
                            d.OPERATOR_REFUND + "\t" + d.OPERATOR_VOID + "\t" + d.OPERATOR_SURDISC + "\r\n";
                    }
                }
                List<surdisc> surdiscs = await App.Database.getSURDISC();
                string surdiscstring = "SURDISC\tPERCDISCRATE\tPERCDISCLIMIT\r\n";
                foreach (surdisc d in surdiscs)
                {
                    if (d.KASA.Equals(kasa.getTypeOfKasa()))
                    {
                        surdiscstring += d.SURDISC.ToString() + "\t" + d.PERCDISCRATE + "\t" + d.PERCDISCLIMIT + "\r\n";
                    }
                }
                List<textlogo> textlogos = await App.Database.getTEXTLOGO();
                string textlogostring = "TEXTLOGO\tLOGOLINE1\tLOGOLINE2\tLOGOLINE3\tLOGOLINE4\tLOGOLINE5\tLOGOLINE6\tLOGOLINE7\tLOGOLINE8\tLOGOLINE9\r\n";
                foreach (textlogo d in textlogos)
                {
                    if (d.KASA.Equals(kasa.getTypeOfKasa()))
                    {
                        textlogostring += d.TEXTLOGO.ToString() + "\t" + d.LOGOLINE1 + "\t" + d.LOGOLINE2 + "\t" + d.LOGOLINE3 +
                        "\t" + d.LOGOLINE4 + "\t" + d.LOGOLINE5 + "\t" + d.LOGOLINE6 + "\t" + d.LOGOLINE7 + "\t" + d.LOGOLINE8 + "\t" +
                        d.LOGOLINE9 + "\r\n";
                    }
                }
                List<traillog> traillogs = await App.Database.getTRAILLOG();
                string traillogstring = "TRAILERTEXTLOGO\tLOGOLINE1\tLOGOLINE2\tLOGOLINE3\tLOGOLINE4\tLOGOLINE5\tLOGOLINE6\tLOGOLINE7\tLOGOLINE8\tLOGOLINE9\r\n";
                foreach (traillog d in traillogs)
                {
                    if (d.KASA.Equals(kasa.getTypeOfKasa()))
                    {
                        traillogstring += d.TRAILERTEXTLOGO.ToString() + "\t" + d.LOGOLINE1 + "\t" + d.LOGOLINE2 + "\t" + d.LOGOLINE3 +
                        "\t" + d.LOGOLINE4 + "\t" + d.LOGOLINE5 + "\t" + d.LOGOLINE6 + "\t" + d.LOGOLINE7 + "\t" + d.LOGOLINE8 + "\t" +
                        d.LOGOLINE9 + "\r\n";
                    }
                }
                //List<updatedPlu> updatedPlus = await App.Database.getUpdatedPLU();
                //string uplustring = "PLU\tPLU_PRICE\tPLU_NAME\tPLU_BARCODE\tPLU_TAXRATE\tPLU_SPEC_REG\tPLU_DPT\tPLU_UNIT\tPLU_LINK\tPLU_OTHERPRICE\tPLU_DESCRIPTOR\tPLU_CONTAINER\n";
                //foreach (updatedPlu d in updatedPlus)
                //{
                //    if (d.KASA.Equals(kasa.getTypeOfKasa()) && d.selected)
                //    {
                //        uplustring += d.PLU.ToString() + "\t" + d.PLU_PRICE + "\t" + d.PLU_BARCODE + "\t" + d.PLU_TAXRATE + "\t" +
                //            d.PLU_SPEC_REG + "\t" + d.PLU_DPT + "\t" + d.PLU_UNIT + "\t" + d.PLU_LINK + "\t" + d.PLU_OTHERPRICE + "\t" + d.PLU_DESCRIPTOR + "\t" + d.PLU_CONTAINER + "\n";
                //    }
                //}
                List<plu> plus = await App.Database.getPluWithName();
                string plustring = "PLU\tPLU_PRICE\tPLU_NAME\tPLU_BARCODE\tPLU_TAXRATE\tPLU_SPEC_REG\tPLU_DPT\tPLU_UNIT\tPLU_LINK\tPLU_OTHERPRICE\tPLU_DESCRIPTOR\tPLU_CONTAINER\r\n";
                foreach (plu d in plus)
                {
                    if (d.KASA.Equals(pickerKasa.SelectedItem.ToString()))
                    {
                        plustring += d.PLU.ToString() + "\t" + d.PLU_PRICE+"\t" +d.PLU_NAME + "\t" + d.PLU_BARCODE + "\t" + d.PLU_TAXRATE + "\t" +
                            d.PLU_SPEC_REG + "\t" + d.PLU_DPT + "\t" + d.PLU_UNIT + "\t" + d.PLU_LINK + "\t" + d.PLU_OTHERPRICE + "\t" + d.PLU_DESCRIPTOR + "\t" + d.PLU_CONTAINER + "\r\n";
                    }
                }
                string stringToSend = "boundary=--------------------------asdlkjaskdjagsdkjl\r\n\r\n" + plustring + dptstring + advertxtstring + ecrlocstring + functextstring + measunitstring + operatorstring + surdiscstring + textlogostring + traillogstring +
                    "--------------------------asdlkjaskdjagsdkjl--";
                stringToSend = stringToSend.Replace("NaN", "");
                /*string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filename = Path.Combine(path, "backup.tsv");
                using (var streamWriter = new StreamWriter(filename))
                {
                    streamWriter.Write(stringToSend);
                }
                using (var streamReader = new StreamReader(filename))
                {
                    string content = streamReader.ReadToEnd();
                }*/

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.4.1/Services?Restore");
                request.Method = "POST";
                request.ContentType = "text/plain; boundary=--------------------------asdlkjaskdjagsdkjl";

                byte[] bytes = Encoding.UTF8.GetBytes(stringToSend);
                request.ContentLength = bytes.Length;
                
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);

                requestStream.Close();
            }

            //upload profil z druhej kasy

        }

        private async void btn1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdatedPLU());
        }
    }
}