using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using TP_eKasa.Data;
using TP_eKasa.Droid.Data;
using Xamarin.Forms;

[assembly :Dependency(typeof(FileHelper))]

namespace TP_eKasa.Droid.Data
{
    public class FileHelper : ILocalFileHelper
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var dbName = "Config.db3";
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(docFolder,dbName);
            var conn = new SQLite.SQLiteAsyncConnection(path);
            return conn;
        }
    }
}