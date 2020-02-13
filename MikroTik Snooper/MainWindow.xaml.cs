using System.Windows;

namespace MikroTikSnooper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ComboBoxViewModel ComboBox { get; set; }
        public MainWindow()
        {           
            InitializeComponent();
            this.DataContext = new ViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Snoop_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
