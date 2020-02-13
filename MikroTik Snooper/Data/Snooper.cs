using System;
using System.Collections.Generic;
using System.IO;


namespace MikroTikSnooper
{
    ///<summary> 
    /// Data acquisition for snooper
    ///</summary>
    public class Snooper
    {
        #region Properties Declaration
        public string IP { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }
        private List<string[]> SnoopData { get; set; }
        public string Channel { get; }
        public string Wlan { get; set; }
        #endregion

        #region Constructors
        public Snooper()
        {
            IP = null;
            Login = null;
            Password = null;
        }
        public Snooper(string myIP, string myLogin, string myPassword)
        {
            IP = myIP;
            Login = myLogin;
            Password = myPassword;
        }
        #endregion

        #region [Method] Connection Setup
        private bool SnoopCommandSend(string Network, MK link )
        {
            if (string.IsNullOrEmpty(Network))
            {
                throw new ArgumentException("Wlan not chosen", nameof(Network));
            }

            try
            {
                link.Send("/interface/wireless/snooper/snoop"); // uruchomienie snoopera
                link.Send("=interface=" + Network);
                link.Send(".tag=sss", true);

                link.Send("/interface wireless snooper{snoop interface=\"" + Network + "\" file=\"" + this.FileName + "\";}"); //zapisanie danych snoopera do pliku
                //ex .link.Send("/interface wireless snooper{snoop interface=\"wlan1\" file=\"myfile\";}");  
                /*  link.Send(""); // zatrzymanie snoopera
                    link.Send(""); // pobranie pliku z routera?
                */
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region [Method] Snooping 
        /// <summary>
        /// Establish connection
        /// </summary>
        /// <returns></returns>
        public string[][] Snooping()
        {
            // this.ConnectionSetup(this.Wlan);

            string myFile = @"C:\Users\Rafal\Desktop\" + this.FileName; // zródło
            int header = 4; // liczba linii nagłówkowych
            int snoopParams = 7; //liczba zbieranych info [Channel] [Width] [Use %] [BW [bps]] [ Net Count] [Noise floor] [STA-Count]
            var lines = File.ReadAllLines(myFile);

            string[][] data = new string[lines.Length][]; // [liczba linijek z txt (bez headera)] [parametry ze snoopera (7)]
            for (int i = header; i < lines.Length; i++)
            {
                string[] tempSplit = lines[i].Split(new string[] { " ","/","bps" },StringSplitOptions.RemoveEmptyEntries);
                data[i - header] = new string[snoopParams];
                int skip = 0;                                   //zmienna wskazujaca ominięcie pustego pola // mozna by TrimEnd/Start ale mniej kodu w ten sposob
                for (int j = 0; j < tempSplit.Length; j++)
                {
                    if (!(String.IsNullOrWhiteSpace(tempSplit[j])) && tempSplit[j] != "DP" && tempSplit[j] != "ac")
                    {

                        string raw = tempSplit[j].Trim('%');
                        data[i - header][j - skip] = raw;
                    }
                    else skip++;
                }
            }
            #endregion
/*
            List<string[]> snoopDataTemp = new List<string[]>();   tranformacja z jagged array na listę
            for (int i = 0; i < lines.Length; i++)
            {
                snoopDataTemp.Add(data[i]);
            }
            string[] a = new string[2];
            a = snoopDataTemp[0];
            a = snoopDataTemp[1];
            a = snoopDataTemp[2];

            SnoopData = snoopDataTemp;
            return SnoopData;
*/

/*
{
    //private List<String> _nets = new List<string>();
    public List<String> Nets { get; set; }
    public List<String> GetNetworks() 
    {
        var networks = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface adapter in networks)
        {
            if (adapter.OperationalStatus == OperationalStatus.Up)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && adapter.Name == "WiFi")
                {
                    //List<String> temp = new List<string>();
                    //temp.Add(adapter.Name);
                    this.Nets.Add(adapter.Name);
                }
            }
        }
        return this.Nets ;
    }
}
*/
            return data;
        }
    }
}
