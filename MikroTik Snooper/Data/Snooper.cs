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
        //Constant file name
        private const string FileName = "SnoopData.txt";
        //Storage for file path 
        public string FilePath { get; }

        public static MK Mk { get; set; }
        public string Wlan { get; set; }
        public List<Channel> ChannelsList { get; set; }
        #endregion

        #region Constructors
        public Snooper()
        {
            string folderLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string myPath = Path.Combine(folderLocation, FileName);
            this.FilePath = myPath;
            ChannelsList = new List<Channel>();
        }

        #endregion

        #region [Method] SnoopCommandSend
        /// <summary>
        /// Creates snoop file txt in given directory
        /// </summary>
        /// <param name="Network"></param>
        /// <param name="MT"></param>
        /// <returns></returns>
        private bool SnoopCommandSend()
        {
     
            try
            {
                MT.Send("/interface/wireless/snooper/snoop"); // uruchomienie snoopera
                MT.Send("=interface=" + this.Wlan);
                MT.Send(".tag=sss", true);

                MT.Send("/interface wireless snooper{snoop interface=\"" + this.Wlan + "\" file=\"" + this.FilePath + "\";}"); //zapisanie danych snoopera do pliku
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
        /// Data aquisition
        /// </summary>
        /// <returns></returns>
        public void Snooping()
        {
            // this.ConnectionSetup(this.Wlan);
            
            int header = 4; // liczba linii nagłówkowych
            int snoopParams = 7; //liczba zbieranych info [Channel] [Width] [Use %] [BW [bps]] [ Net Count] [Noise floor] [STA-Count]
            var lines = System.IO.File.ReadAllLines(this.FilePath);

            string[][] data = new string[lines.Length-header][]; // [liczba linijek z txt (bez headera)] [parametry ze snoopera (7)]
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
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != null)
                { 
                Channel Channel = new Channel();
                Channel.Fequency = data[i][0] ?? "0";
                Channel.FeqWidth = data[i][1] ?? "0";
                Channel.UsePercentage = data[i][2] ?? "0";
                Channel.BandWidth = data[i][3] ?? "0";
                Channel.NetCount = data[i][4] ?? "0";
                Channel.NoiseFloor = data[i][5] ?? "0";
                Channel.StationCount = data[i][6] ?? "0";
                ChannelsList.Add(Channel);
                }
            }




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
        }
        #endregion

    }
}
