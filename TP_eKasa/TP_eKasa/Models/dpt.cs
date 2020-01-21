using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class dpt
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int DPT { get; set; }
        public string DPT_NAME { get; set; }
        public string KASA { get; set; }
    }
}
