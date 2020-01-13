using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class surdisc
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int SURDISC { get; set; }
        public string PERCDISCRATE { get; set; }
        public string PERCDISCLIMIT { get; set; }

    }
}
