using System.Windows;
using WPFApp.Model;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            phonesList.ItemsSource = new PhonesRepository().AllPhones;
        }
    }
}
