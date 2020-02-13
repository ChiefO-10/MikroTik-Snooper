using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;



namespace MikroTikSnooper
{
    public class ComboBoxViewModel : BaseViewModel
    {
      
        private ObservableCollection<string> _wlan;
        public ObservableCollection<string> Wlan
        {
            get { return _wlan; }
            set
            {
                if (_wlan == value) return;
                _wlan = WirelessNetwork.WirelessNets;
                OnPropertyChanged();
            }
        }

    }
}
