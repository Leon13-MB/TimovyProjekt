using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class measunit
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int MEASUREUNIT { get; set; }
        public string MEASUREUNIT_NAME { get; set; }
    }
}
