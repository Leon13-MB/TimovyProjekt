using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP_eKasa.Models
{
    public class Profile
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string TableName { get; set; }
        

        public Profile() { }
        public Profile(string _tableName, int _ID) {
            this.TableName = _tableName;
            this.ID = _ID;
        }

        public bool CheckConf() {
            if (!this.TableName.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
