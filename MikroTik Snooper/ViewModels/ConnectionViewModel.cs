using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MikroTikSnooper
{
    public class ConnectionViewModel : BaseViewModel
    {
        #region Private Fields

        private string _ip;
        private string _login;
        private string _password;
        private ObservableCollection<string> _wlan = new ObservableCollection<string> { "wlan1", "wlan2" };

        #endregion

        #region Public Properties

        public RouterConnection Connection { get; set; }
        public string IP
        {
            get { return _ip; }
            set
            {
                if (_ip == value) return;
                _ip = value;
                OnPropertyChanged();
            }
        }
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
 //             _wlan = Mk.WirelessNets;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public ConnectionViewModel()
        {

            Connection = new RouterConnection();
            ConnectCommand = new RelayCommand<object> ( param => this.Connection.Connect(), 
                                                        param => this.CanExecuteConnection());

            GetWlansCommand = new RelayCommand<object>(param => this.Connection.GetWlans(),
                                                       param => this.CanExecuteGetWlans());


        }

        #endregion

        #region Commands

        public ICommand ConnectCommand { get; set; }
        public ICommand GetWlansCommand { get; set; }
        #endregion

        #region Can Execute Methods
        private bool CanExecuteGetWlans()
        {
            return RouterConnection.Mk != null;
        }
        private bool CanExecuteConnection()
        {
            return RouterConnection.Mk != null && (!string.IsNullOrWhiteSpace(Password)) && (!string.IsNullOrWhiteSpace(Login));
        }

        #endregion
    }
}
