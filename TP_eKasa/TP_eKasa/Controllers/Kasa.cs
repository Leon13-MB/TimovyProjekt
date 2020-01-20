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
            //string kasa = getType();
            using (var streamWriter = new StreamWriter(filename))
            {
                //streamWriter.WriteLine(kasa);
                streamWriter.Write("E50o");
                streamWriter.Close();
            }
        }


        public string getType()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://icm.local/Services?Ecrinfo");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream, Encoding.Unicode);
            string txt = reader.ReadToEnd();
            reader.Close();
            string[] strings = txt.Split('\t');
            return strings[0];
        }
    }
}
