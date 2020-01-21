using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP_eKasa.Data
{
    public interface ILocalFileHelper
    {
        SQLiteAsyncConnection GetConnection();
    }
}
