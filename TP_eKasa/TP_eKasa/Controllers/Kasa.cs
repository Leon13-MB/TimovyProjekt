using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TP_eKasa.Controllers
{
    public class Kasa
    {
        public Kasa()
        {

        }

        public string getTypeOfKasa()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "KASA.txt");
            string kasa = "";
            using (var streamReader = new StreamReader(filename))
            {
                kasa = streamReader.ReadToEnd();
                streamReader.Close();
            }
            return kasa;
        }

        public void setTypeOfKasa()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "KASA.txt");
            string kasa = getType();
            using (var streamWriter = new StreamWriter(filename))
            {
                streamWriter.Write(kasa);
                //streamWriter.Write("E150o");
                streamWriter.Close();
            }
        }


        public string getType()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.4.1/Services?EcrInfo");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            string txt = reader.ReadToEnd();
            reader.Close();
            string[] strings = txt.Split('\t');
            return strings[0];
        }
    }
}
