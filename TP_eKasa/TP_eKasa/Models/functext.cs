using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class functext
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int FUNCTEXT { get; set; }
        public string FUNCTEXT_REFERENCE_NUMBER_TEXT { get; set; }
    }
}
