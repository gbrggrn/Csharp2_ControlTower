using Csharp2_ControlTower.Model;
using CSharp2_ControlTower;
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
        private Dictionary<Func<string>, string>? inputValues;

        public MainWindow()
        {
            InitializeComponent();
            controlTower = new();
            SetupInputValues();
            this.DataContext = controlTower;
            airplaneListView.ItemsSource = controlTower.Airplanes;
            flightDisplayLstView.ItemsSource = controlTower.InFlightMessaging;
        }

        internal void SetupInputValues()
        {
            inputValues = new Dictionary<Func<string>, string>()
            {
                { () => nameTxtBox.Text, "Name" },
                { () => flightIDTxtBox.Text, "Flight ID" },
                { () => destinationTxtBox.Text, "Description" },
                { () => flightTimeTxtBox.Text, "Flight time" },
            };
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> errorMessages = [];
            int flightTime = 0;
            
            foreach (var pair in inputValues!)
            {
                string value = pair.Key();
                string label = pair.Value;

                if (string.IsNullOrWhiteSpace(value))
                {
                    errorMessages.Add($"{label} can not be empty");
                }
                else if (value.Length > 20)
                {
                    errorMessages.Add($"{label} can not be longer than 20 characters");
                }
                else if (label == "Flight time")
                {
                    if (int.TryParse(value, out int result))
                    {
                        if (result < 5 || result > 15)
                        {
                            errorMessages.Add($"{label} has to be an integer between 5-15");
                        }
                        else
                        {
                            flightTime = result;
                        }
                    }
                    else
                    {
                        errorMessages.Add($"{label} has to be an integer");
                    }
                }
            }

            if (errorMessages.Count > 0)
            {
                string result = string.Join("\n", errorMessages);

                MessageBoxes.DisplayErrorBox(result);
            }
            else
            {
                var inputs = new AirplaneDTO
                {
                    Name = nameTxtBox.Text,
                    FlightId = flightIDTxtBox.Text,
                    Destination = destinationTxtBox.Text,
                    FlightTime = flightTime
                };

                controlTower.AddPlane(inputs);
                ResetInputFields();
            }
        }

        private void ResetInputFields()
        {
            nameTxtBox.Text = string.Empty;
            flightIDTxtBox.Text = string.Empty;
            destinationTxtBox.Text = string.Empty;
            flightTimeTxtBox.Text = string.Empty;
        }

        private void TakeOffBtn_Click(object sender, RoutedEventArgs e)
        {
            if (airplaneListView.SelectedIndex != -1)
            {
                int index = airplaneListView.SelectedIndex;
                controlTower.OrderTakeOff(index);
            }
        }
    }
}