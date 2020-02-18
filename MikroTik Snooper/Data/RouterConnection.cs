using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MikroTikSnooper
{
    public class RouterConnection
    {
        public static CommonMK Mk { get; set; }

        public RouterConnection(CommonMK common)
        {
            Mk = common;        
        }
        
        public static bool Connect()
        {
            {
                try
                {

                    if (!(string.IsNullOrWhiteSpace(RouterConnection.Mk.IP)))
                    {
                        if ((!(string.IsNullOrWhiteSpace(MikroTikSnooper.Mk.Password))) && (!(string.IsNullOrWhiteSpace(MikroTikSnooper.Mk.Login)))) MikroTikSnooper.Mk.MT.Login(MikroTikSnooper.Mk.Login, MikroTikSnooper.Mk.Password);
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
