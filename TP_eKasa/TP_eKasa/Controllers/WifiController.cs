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
        public string TurnOnWifi()
        {
            string isTurned = string.Empty;
            if (wifiManager.IsWifiEnabled)
            {
                isTurned = "WIFI UŽ JE ZAPNUTÉ";
            }
            else
            {
                wifiManager.SetWifiEnabled(true);
                isTurned = "WIFI JE ZAPNUTÉ";
            }
            return isTurned;
        }

        public string TurnOffWifi()
        {
            string isOff = "WIFI JE VYPNUTÉ";
            wifiManager.SetWifiEnabled(false);
            return isOff;
        }

        public string getNetworkInfo()
        {
            string info = "";
            if (wifiManager.IsWifiEnabled)
            {
                info = wifiManager.ConnectionInfo.ToString();
            }
            return info;
        }

        public bool isOn()
        {
            return wifiManager.IsWifiEnabled;
        }

    }
}
