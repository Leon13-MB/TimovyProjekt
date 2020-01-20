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

namespace TP_eKasa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Upload : ContentPage
    {
        Kasa kasa = new Kasa();
        public Upload()
        {
            InitializeComponent();
        }

        public async void test(object sender, EventArgs e)
        {
            List<dpt> dpts = await App.Database.getDPT();
            string dptstring = "DPT\tDPT_NAME\n";
            foreach(dpt d in dpts)
            {
                if (d.KASA.Equals(kasa.getTypeOfKasa()))
                {
                    dptstring += d.DPT.ToString() + "\t" + d.DPT_NAME + "\n";
                }
            }
            List<advertxt> advertxts = await App.Database.getADVERTXT();
            string advertxtstring = "ADVERTEXT\tADVERTISEMENT_TEXT\tADVERTISEMENT_TYPE\tADVERTISEMENT_LOOKUP\n";
            foreach (advertxt d in advertxts)
            {
                if (d.KASA.Equals(kasa.getTypeOfKasa()))
                {
                    advertxtstring += d.ADVERTEXT.ToString() + "\t" + d.ADVERTISEMENT_TEXT + "\t" + d.ADVERTISEMENT_TYPE +"\t"+d.ADVERTISEMENT_LOOKUP+"\n";
                }
            }
            List<ecrloc> ecrlocs = await App.Database.getECRLOC();
            string ecrlocstring = "ECRLOC\tECRLOC_LOCTYPE\tECRLOC_LOCDESC\tECRLOC_MUNICIPALITY\tECRLOC_STREETNAME\tECRLOC_BUILDINGNUMBER\tECRLOC_PROPERTYREGNUMBER\tECRLOC_POSTALCODE\n";
            foreach (ecrloc d in ecrlocs)
            {
                if (d.KASA.Equals(kasa.getTypeOfKasa()))
                {
                    ecrlocstring += d.ECRLOC.ToString()+"\t"+d.ECRLOC_LOCTYPE+"\t"+d.ECRLOC_LOCDESC + "\t" +
                        d.ECRLOC_MUNICIPALITY + "\t" +d.ECRLOC_STREETNAME + "\t" +d.ECRLOC_BUILDINGNUMBER + "\t" +
                        d.ECRLOC_PROPERTYREGNUMBER + "\t" +d.ECRLOC_POSTALCODE+"\n";
                }
            }
            List<functext> functexts = await App.Database.getFUNCTEXT();
            string functextstring = "FUNCTEXT\tFUNCTEXT_REFERENCE_NUMBER_TEXT\n";
            foreach (functext d in functexts)
            {
                if (d.KASA.Equals(kasa.getTypeOfKasa()))
                {
                    functextstring += d.FUNCTEXT.ToString() + "\t" + d.FUNCTEXT_REFERENCE_NUMBER_TEXT + "\n";
                }
            }
            List<measunit> measunits = await App.Database.getMEASUNIT();
            string measunitstring = "MEASUREUNIT\tMEASUREUNIT_NAME\n";
            foreach (measunit d in measunits)
            {
                if (d.KASA.Equals(kasa.getTypeOfKasa()))
                {
                    measunitstring += d.MEASUREUNIT.ToString() + "\t" + d.MEASUREUNIT_NAME + "\n";
                }
            }
            List<@operator> operators = await App.Database.getOPERATOR();
            string operatorstring = "OPERATOR\tOPERATOR_NAME\tOPERATOR_PSWD\tOPERATOR_XMODE\tOPERATOR_ZMODE\tOPERATOR_PMODE\tOPERATOR_TMODE\tOPERATOR_REFUND\tOPERATOR_VOID\tOPERATOR_SURDISC\n";
            foreach (@operator d in operators)
            {
                if (d.KASA.Equals(kasa.getTypeOfKasa()))
                {
                    operatorstring += d.OPERATOR.ToString() + "\t" + d.OPERATOR_NAME + "\t"+d.OPERATOR_PSWD + "\t" +
                        d.OPERATOR_XMODE + "\t" +d.OPERATOR_ZMODE + "\t" +d.OPERATOR_PMODE + "\t" +d.OPERATOR_TMODE + "\t" +
                        d.OPERATOR_REFUND + "\t" +d.OPERATOR_VOID + "\t" +d.OPERATOR_SURDISC+"\n";
                }
            }
            List<surdisc> surdiscs = await App.Database.getSURDISC();
            string surdiscstring = "SURDISC\tPERCDISCRATE\tPERCDISCLIMIT\n";
            foreach (surdisc d in surdiscs)
            {
                if (d.KASA.Equals(kasa.getTypeOfKasa()))
                {
                    surdiscstring += d.SURDISC.ToString() + "\t" + d.PERCDISCRATE+"\t"+d.PERCDISCLIMIT + "\n";
                }
            }
            List<textlogo> textlogos = await App.Database.getTEXTLOGO();
            string textlogostring = "TEXTLOGO\tLOGOLINE1\tLOGOLINE2\tLOGOLINE3\tLOGOLINE4\tLOGOLINE5\tLOGOLINE6\tLOGOLINE7\tLOGOLINE8\tLOGOLINE9\n";
            foreach (textlogo d in textlogos)
            {
                if (d.KASA.Equals(kasa.getTypeOfKasa()))
                {
                    textlogostring += d.TEXTLOGO.ToString() + "\t" + d.LOGOLINE1+"\t" + d.LOGOLINE2 + "\t" + d.LOGOLINE3 +
                    "\t" + d.LOGOLINE4 + "\t" + d.LOGOLINE5 + "\t" + d.LOGOLINE6 + "\t" + d.LOGOLINE7 + "\t" + d.LOGOLINE8 + "\t" +
                    d.LOGOLINE9 + "\n";
                }
            }
            List<traillog> traillogs = await App.Database.getTRAILLOG();
            string traillogstring = "TRAILERTEXTLOGO\tLOGOLINE1\tLOGOLINE2\tLOGOLINE3\tLOGOLINE4\tLOGOLINE5\tLOGOLINE6\tLOGOLINE7\tLOGOLINE8\tLOGOLINE9\n";
            foreach (traillog d in traillogs)
            {
                if (d.KASA.Equals(kasa.getTypeOfKasa()))
                {
                    traillogstring += d.TRAILERTEXTLOGO.ToString() + "\t" + d.LOGOLINE1 + "\t" + d.LOGOLINE2 + "\t" + d.LOGOLINE3 +
                    "\t" + d.LOGOLINE4 + "\t" + d.LOGOLINE5 + "\t" + d.LOGOLINE6 + "\t" + d.LOGOLINE7 + "\t" + d.LOGOLINE8 + "\t" +
                    d.LOGOLINE9 + "\n";
                }
            }
            string stringToSend = dptstring + advertxtstring + ecrlocstring + functextstring + measunitstring + operatorstring + surdiscstring + textlogostring + traillogstring;
            stringToSend = stringToSend.Replace("NaN", "");
        }

    }
}