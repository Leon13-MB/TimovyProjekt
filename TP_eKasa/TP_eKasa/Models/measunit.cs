using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class measunit
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string MEASUREUNIT { get; set; }
        public string MEASUREUNIT_NAME { get; set; }
        public string IDKASA { get; set; }
    }
}
