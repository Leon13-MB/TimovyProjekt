using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class ecrloc
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int ECRLOC { get; set; }
        public string ECRLOC_LOCTYPE { get; set; }
        public string ECRLOC_LOCDESC { get; set; }
        public string ECRLOC_MUNICIPALITY { get; set; }
        public string ECRLOC_STREETNAME { get; set; }
        public string ECRLOC_BUILDINGNUMBER { get; set; }
        public string ECRLOC_PROPERTYREGNUMBER { get; set; }
        public string ECRLOC_POSTALCODE { get; set; }
        public string IDKASA { get; set; }
    }
}
