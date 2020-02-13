using System.Collections.ObjectModel;

namespace MikroTikSnooper
{
    public class SnooperListViewModel : BaseViewModel
    {
        public string IP { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }
        public ObservableCollection<string[]> SnoopData { get; set; }
        public string Channel { get; }
        public string Wlan { get; set; }
    }
}
