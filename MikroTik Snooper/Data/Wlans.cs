using System.Collections.Generic;

namespace MikroTikSnooper
{
    class Wlans
    {
        public static MK Mk { get; set; }
        public static List<string> WirelessNets { get; set; }
        public static List<string> GetWlans()
        {
            Send("/interface list");
            return WirelessNets;
        }
    }
}
