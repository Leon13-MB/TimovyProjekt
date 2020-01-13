using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class plu
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public int PLU { get; set; }
        public string PLU_PRICE { get; set; }
        public string PLU_NAME { get; set; }
        public string PLU_BARCODE { get; set; }
        public string PLU_TAXRATE { get; set; }
        public string PLU_SPEC_REG { get; set; }
        public string PLU_DPT { get; set; }
        public string PLU_UNIT { get; set; }
        public string PLU_LINK { get; set; }
        public string PLU_OTHERPRICE { get; set; }
        public string PLU_DESCRIPTOR { get; set; }
        public string PLU_CONTAINER { get; set; }
        public string PLU_STOCK { get; set; }

    }
}
