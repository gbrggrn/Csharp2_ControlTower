using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Csharp2_ControlTower
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ControlTower controlTower;
        public MainWindow()
        {
            InitializeComponent();
            controlTower = new(airplaneListBox);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TakeOffBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}