using System.Windows;

namespace MikroTikSnooper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {           
            InitializeComponent();
            this.DataContext = new ViewModels();
            Snooper x = new Snooper();
            x.FileName = @"C:\Users\astat\Desktop\myfile.txt";
            x.Snooping();
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
