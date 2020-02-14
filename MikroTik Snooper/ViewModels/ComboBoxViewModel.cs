using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;



namespace MikroTikSnooper
{
    public class ComboBoxViewModel : BaseViewModel
    {
        private string _login;
        private string _password;
        private ObservableCollection<string> _wlan = new ObservableCollection<string> { "wlan1", "wlan2" };

        public string Login
        {
            get { return _login; }
            set
            {
                if (_login == value) return;
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged();
            }
        }

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
