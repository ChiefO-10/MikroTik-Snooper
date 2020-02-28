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
        public List<string> WirelessNets { get; private set; }

        public RouterConnection() { }
        public RouterConnection (MK MicroTik, string login, string password)
        {
            Mk = MicroTik;
            Login = login;
            Password = password;
        }

        public void Connect(string IP, string Login, string Password, MK MK)
        {
            if (MK != null)
            {

                try
                {
                    this.Mk = MK;
                    MK.Setup(IP);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return;
                }

                try
                {
                    var state = false;
                    if ((!(string.IsNullOrWhiteSpace(Password))) && (!(string.IsNullOrWhiteSpace(Login))))
                    {
                        state = Mk.Login(Login, Password);
                    }
                    if (state)
                    {
                        this.Login = Login;
                        this.Password = Password;
                        Mk.Send("/interface list");
                        List<string> ConsoleRead = new List<string>(Mk.Read());
                        try
                        {
                            WirelessNets = ConsoleRead;
                        }
                        catch (NullReferenceException e)
                        {
                            Console.WriteLine(e.ToString());
                            MessageBox.Show(e.Message);
                            throw;
                        }
                    }
                
                    else
                    {
                        throw new ArgumentException("Incorrect Login or Password");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    MessageBox.Show(e.Message);
                    return;
                }
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
