﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
    public class textlogo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int TEXTLOGO { get; set; }
        public string LOGOLINE1 { get; set; }
        public string LOGOLINE2 { get; set; }
        public string LOGOLINE3 { get; set; }
        public string LOGOLINE4 { get; set; }
        public string LOGOLINE5 { get; set; }
        public string LOGOLINE6 { get; set; }
        public string LOGOLINE7 { get; set; }
        public string LOGOLINE8 { get; set; }
        public string LOGOLINE9 { get; set; }
        public string KASA { get; set; }

    }
}
