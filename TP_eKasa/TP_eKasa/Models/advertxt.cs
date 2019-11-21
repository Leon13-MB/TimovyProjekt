using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TP_eKasa.Models
{
<<<<<<< HEAD
    public class advertxt
=======
    class advertxt
>>>>>>> 1b61a94cd0d601d28a5c5de4f001a9c6cec317d9
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string ADVERTEXT { get; set; }
        public string ADVERTISEMENT_TEXT { get; set; }
        public string ADVERTISEMENT_TYPE { get; set; }
        public string ADVERTISEMENT_LOOKUP { get; set; }
        public string IDKASA { get; set; }
    }
}
