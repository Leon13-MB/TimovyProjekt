using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class Surdisc
    {
        [PrimaryKey]
        public Nullable<int> ID { get; set; }
        public Nullable<float> SURDISC { get; set; }
        public Nullable<float> PERCDISCRATE { get; set; }
        public Nullable<float> PERCDISCLIMIT { get; set; }
        public string IDKASA { get; set; }
    }
}
