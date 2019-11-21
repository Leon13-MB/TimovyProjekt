using System;
using System.Collections.Generic;
using System.Text;
using Android.Content;
using Android.Net.Wifi;

namespace TP_eKasa
{
    public class WifiController
    {
        WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

    }
}
