using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class Plu
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int PLU { get; set; }
        public float PLU_PRICE { get; set; }
        public string PLU_NAME { get; set; }
        public long PLU_BARCODE { get; set; }
        public int PLU_TAXRATE { get; set; }
        public int PLU_SPEC_REG { get; set; }
        public int PLU_DPT { get; set; }
        public int PLU_UNIT { get; set; }
        public int PLU_LINK { get; set; } 
        public int PLU_OTHERPRICE { get; set; }
        public int PLU_DESCRIPTOR { get; set; }
        public int PLU_CONTAINER { get; set; }
        public float PLU_STOCK { get; set; }
        public string IDKASA { get; set; }
    }
}
