using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class Operator
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int OPERATOR { get; set; }
        public string OPERATOR_NAME { get; set; }
        public int OPERATOR_PSWD { get; set; }
        public int OPERATOR_XMODE { get; set; }
        public int OPERATOR_ZMODE { get; set; }
        public int OPERATOR_PMODE { get; set; }
        public int OPERATOR_TMODE { get; set; }
        public int OPERATOR_REFUND { get; set; }
        public int OPERATOR_VOID { get; set; }
        public int OPERATOR_SURDISC { get; set; }
        public string IDKASA { get; set; }

    }
}
