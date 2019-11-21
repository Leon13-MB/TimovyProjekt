using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP_eKasa.Models
{
    public class Configuration1
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ConfName { get; set; }
        public string A1 { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }

    }

}
