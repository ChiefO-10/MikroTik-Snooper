using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace MikroTikSnooper
{
    public static class WirelessNetwork
    {
        public static MK MT;
        public static string IP { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static ObservableCollection<string> WirelessNets { get; set; }
        public static ObservableCollection<string> ConnectionSetup()
        {

            return WirelessNets;
        }

        public static bool Connect()
        {
            {
                try
                {

                    if (!(String.IsNullOrWhiteSpace(WirelessNetwork.IP)))
                    {
                        WirelessNetwork.MT = new MK(IP);
                        if ((!(String.IsNullOrWhiteSpace(WirelessNetwork.Password))) && (!(String.IsNullOrWhiteSpace(WirelessNetwork.Login)))) WirelessNetwork.MT.Login(WirelessNetwork.Login, WirelessNetwork.Password);
                        else MessageBox.Show("Input Login and Password");
                    }

                    return true;

                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("{0}", e);
                    return false;
                }

            }
        }
    }
}
