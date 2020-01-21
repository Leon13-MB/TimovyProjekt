using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace TP_eKasa
{
    public class BackupHandler
    {
        public BackupHandler()
        {

        }
        public DataTable readEDF(StreamReader reader)
        {
            string txt = reader.ReadToEnd();

            string[] temp = txt.Split('\n');

            string header = temp[0];
            string[] rows = temp.Skip(1).Take(temp.Length - 2).ToArray();

            string[] columns = header.Split('\t');

            int numberofcolumns = columns.Count();
            int numberofrows = rows.Count();

            DataTable dt = new DataTable();
            DataColumn id = new DataColumn();
            id.DataType = Type.GetType("System.Int32");
            id.ColumnName = columns[0];
            dt.Columns.Add(id);
            //columns
            foreach (string c in columns.Skip(1))
            {
                DataColumn dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = c;
                dt.Columns.Add(dc);
            }
            //rows
            foreach (string r in rows)
            {
                string cn = string.Empty;
                DataRow dr = dt.NewRow();
                string[] t = r.Split('\t');
                for (int i = 0; i < numberofcolumns; i++)
                {
                    cn = columns[i];
                    if (i == 0)
                    {
                        dr[cn] = Convert.ToInt32(t[i]);
                    }
                    else
                    {
                        if (t[i] != "")
                        {
                            dr[cn] = t[i];
                        }
                        else
                        {
                            dr[cn] = "NaN";
                        }
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable stringToDT(string inputString)
        {
            string txt = inputString;

            string[] temp = txt.Split('\n');

            string header = temp[0];
            string[] rows = temp.Skip(1).Take(temp.Length - 2).ToArray();

            string[] columns = header.Split('\t');

            int numberofcolumns = columns.Count();
            foreach (string column in columns)
            {
                if (column.Equals(""))
                {
                    numberofcolumns--;
                }
            }

            int numberofrows = rows.Count();

            DataTable dt = new DataTable();
            dt.TableName = columns[0];
            DataColumn id = new DataColumn();
            id.DataType = Type.GetType("System.Int32");
            id.ColumnName = columns[0];
            dt.Columns.Add(id);
            //columns
            for (int i = 1; i < numberofcolumns; i++)
            {
                DataColumn dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = columns[i];
                dt.Columns.Add(dc);
            }
            //rows
            foreach (string r in rows)
            {
                if (!r.Equals(""))
                {
                    string cn = string.Empty;
                    DataRow dr = dt.NewRow();
                    string[] t = r.Split('\t');
                    for (int i = 0; i < numberofcolumns; i++)
                    {
                        cn = columns[i];
                        if (i == 0)
                        {
                            dr[cn] = Convert.ToInt32(t[i]);
                        }
                        else
                        {
                            if (t[i] != "")
                            {
                                dr[cn] = t[i];
                            }
                            else
                            {
                                dr[cn] = "NaN";
                            }
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        public string createEDF(DataTable dt)
        {
            //vyvorit string pre edf
            string text = string.Empty;
            var cols = dt.Columns;
            int o = 0;
            List<string> names = new List<string>();
            foreach (var cl in cols)
            {
                names.Add(cl.ToString());
            }
            foreach (string n in names)
            {
                if (o != names.Count() - 1)
                {
                    text += n + "\t";
                }
                else
                {
                    text += n + "\n";
                }
                o++;
            }

            var data = dt.Select();
            foreach (var d in data)
            {
                o = 0;
                var tmp = d.ItemArray;
                foreach (var i in tmp)
                {
                    if (o != tmp.Count() - 1)
                    {
                        text += i.ToString() + "\t";
                    }
                    else
                    {
                        text += i.ToString() + "\n";
                    }
                    o++;
                }
            }
            return text;
        }
        public string createTextToSend(DataTable dt)
        {
            //vyvorit string pre edf
            string text = string.Empty;
            var cols = dt.Columns;
            int o = 0;
            List<string> names = new List<string>();
            foreach (var cl in cols)
            {
                names.Add(cl.ToString());
            }
            foreach (string n in names)
            {
                if (o != names.Count() - 1)
                {
                    text += n + "\t";
                }
                else
                {
                    text += n + "\n";
                }
                o++;
            }

            var data = dt.Select();
            foreach (var d in data)
            {
                o = 0;
                var tmp = d.ItemArray;
                foreach (var i in tmp)
                {
                    if (o != tmp.Count() - 1)
                    {
                        text += i.ToString() + "\t";
                    }
                    else
                    {
                        text += i.ToString() + "\n";
                    }
                    o++;
                }
            }
            return text;
        }


        public List<string> readToListofTables(string txt)
        {
            List<string> tables = new List<string>();
            List<string> tabs = new List<string>();
            tables.Add("ADVERTXT");
            tables.Add("DPT");
            tables.Add("ECRLOC");
            tables.Add("FUNCTEXT");
            tables.Add("MEASUREUNIT");
            tables.Add("OPERATOR");
            tables.Add("PLU");
            tables.Add("SURDISC");
            tables.Add("TEXTLOGO");
            tables.Add("TRAILLOG");
            string[] rows = txt.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                if (!rows[i].Equals(""))
                {
                    rows[i] = rows[i].Replace("\r", string.Empty);
                }
            }
            string name = "";
            bool change = false;
            int index = -1;
            foreach (string r in rows)
            {
                string[] temp = r.Split('\t');

                foreach (string t in tables)
                {
                    if (t.Equals(temp[0]))
                    {
                        index++;
                        change = true;
                        tabs.Add("");
                        break;
                    }
                }
                if (change)
                {
                    name = temp[0];
                    change = false;
                }
                tabs[index] += r + "\n";
            }
            return tabs;
        }

        public void createFiles()
        {
            List<string> tables = new List<string>();
            tables.Add("ADVERTXT");
            tables.Add("DPT");
            tables.Add("ECRLOC");
            tables.Add("FUNCTEXT");
            tables.Add("MEASUNIT");
            tables.Add("OPERATOR");
            tables.Add("PLU");
            tables.Add("SURDISC");
            tables.Add("TEXTLOGO");
            tables.Add("TRAILLOG");
            string header = "";
            foreach (string t in tables)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filename = Path.Combine(path, t + ".EDF");
                switch (t)
                {
                    case "ADVERTXT":
                        header = "ADVERTXT\tADVERTISEMENT_TEXT\tADVERTISEMENT_TYPE\tADVERTISEMENT_LOOKUP\n";
                        break;
                    case "DPT":
                        header = "DPT\tDPT_NAME\n";
                        break;
                    case "ECRLOC":
                        header = "ECRLOC\tECRLOC_LOCTYPE\tECRLOC_LOCDESC\tECRLOC_MUNICIPALITY\tECRLOC_STREETNAME\tECRLOC_BUILDINGNUMBER\tECRLOC_PROPERTYREGNUMBER\tECRLOC_POSTALCODE\n";
                        break;
                    case "FUNCTEXT":
                        header = "FUNCTEXT\tFUNCTEXT_REFERENCE_NUMBER_TEXT\n";
                        break;
                    case "MEASUNIT":
                        header = "MEASUREUNIT\tMEASUREUNIT_NAME\n";
                        break;
                    case "OPERATOR":
                        header = "OPERATOR\tOPERATOR_NAME\tOPERATOR_PSWD\tOPERATOR_XMODE\tOPERATOR_ZMODE\tOPERATOR_PMODE\tOPERATOR_TMODE\tOPERATOR_REFUND\tOPERATOR_VOID\tOPERATOR_SURDISC\n";
                        break;
                    case "PLU":
                        header = "PLU\tPLU_PRICE\tPLU_NAME\tPLU_BARCODE\tPLU_TAXRATE\tPLU_SPEC_REG\tPLU_DPT\tPLU_UNIT\tPLU_LINK\tPLU_OTHERPRICE\tPLU_DESCRIPTOR\tPLU_CONTAINER\tPLU_STOCK\n";
                        break;
                    case "SURDISC":
                        header = "SURDISC\tPERCDISCRATE\tPERCDISCLIMIT\n";
                        break;
                    case "TEXTLOGO":
                        header = "TEXTLOGO\tLOGOLINE1\tLOGOLINE2\tLOGOLINE3\tLOGOLINE4\tLOGOLINE5\tLOGOLINE6\tLOGOLINE7\tLOGOLINE8\tLOGOLINE9\n";
                        break;
                    case "TRAILLOG":
                        header = "TRAILERTEXTLOGO\tLOGOLINE1\tLOGOLINE2\tLOGOLINE3\tLOGOLINE4\tLOGOLINE5\tLOGOLINE6\tLOGOLINE7\tLOGOLINE8\tLOGOLINE9\n";
                        break;
                }
                using (var streamWriter = new StreamWriter(filename))
                {
                    streamWriter.Write(header);
                }
                using (var streamReader = new StreamReader(filename))
                {
                    string content = streamReader.ReadToEnd();
                }
            }
        }
    }


}
