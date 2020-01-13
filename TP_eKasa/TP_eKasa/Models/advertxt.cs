using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class advertxt
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ADVERTEXT { get; set; }
        public string ADVERTISEMENT_TEXT { get; set; }
        public string ADVERTISEMENT_TYPE { get; set; }
        public string ADVERTISEMENT_LOOKUP { get; set; }
    }
}
