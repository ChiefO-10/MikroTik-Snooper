using System;
using System.Collections.Generic;
using System.Windows;

namespace MikroTikSnooper
{
    public class RouterConnection
    {
        public MK Mk { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<string> WirelessNets { get; set; }

        public RouterConnection() { }
        public RouterConnection (MK MicroTik, string login, string password)
        {
            Mk = MicroTik;
            Login = login;
            Password = password;
        }

        public void Connect()
        {
           try
           {
               if ((!(string.IsNullOrWhiteSpace(Password))) && (!(string.IsNullOrWhiteSpace(Login)))) Mk.Login(Login, Password);
               else MessageBox.Show("Input Login and Password");

           }
           catch (ArgumentException e)
           {
               Console.WriteLine("{0}", e);
           }
        }
        public bool GetWlans()
        {
            Mk.Send("/interface list");
            List<string>ConsoleRead = new List<string>(Mk.Read());
            if (ConsoleRead != null)
            {
                WirelessNets = ConsoleRead;
                return true;
            }
            else return false;
        }
    }
}
