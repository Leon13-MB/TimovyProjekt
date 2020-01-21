using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class @operator
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int OPERATOR { get; set; }
        public string OPERATOR_NAME { get; set; }
        public string OPERATOR_PSWD { get; set; }
        public string OPERATOR_XMODE { get; set; }
        public string OPERATOR_ZMODE { get; set; }
        public string OPERATOR_PMODE { get; set; }
        public string OPERATOR_TMODE { get; set; }
        public string OPERATOR_REFUND { get; set; }
        public string OPERATOR_VOID { get; set; }
        public string OPERATOR_SURDISC { get; set; }
        public string KASA { get; set; }

    }
}
