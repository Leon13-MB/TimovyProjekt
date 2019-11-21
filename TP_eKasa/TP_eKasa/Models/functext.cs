using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class functext
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string FUNCTEXT { get; set; }
        public string FUNCTEXT_REFERENCE_NUMBER_TEXT { get; set; }
        public string IDKASA { get; set; }
    }
}
