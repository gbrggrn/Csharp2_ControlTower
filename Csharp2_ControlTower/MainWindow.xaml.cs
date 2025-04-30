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
        private readonly ControlTower controlTower;

        //Dictionary of textbox text-getters and input descriptions
        private Dictionary<Func<string>, string>? inputValues;

        //Limits
        private const int maxFlightTime = 50;
        private const int minFlightTime = 5;
        private const int maxChars = 20;

        /// <summary>
        /// Constructor initializes the inputValues Dictionary.
        /// Instantiates a new ControlTower.
        /// Sets the datacontext for this window.
        /// Sets itemsSource for ListViews.
        /// Calls AddTestValues in controlTower.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            controlTower = new();
            SetupInputValues();
            this.DataContext = controlTower;
            airplaneListView.ItemsSource = controlTower.Airplanes;
            flightDisplayLstView.ItemsSource = controlTower.InFlightMessaging;
            controlTower.AddTestValues();
        }

        /// <summary>
        /// Initializes the inputValues Dictionary with textbox-getters and a description string for each field.
        /// </summary>
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

        /// <summary>
        /// Validates the input entered and prompts error messages if invalid.
        /// If valid: creates an airplaneDTO and calls AddPlane from controlTower passing the DTO.
        /// Calls resetting of input fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> errorMessages = [];
            int flightTime = 0;
            
            //Iterates over inputs
            foreach (var pair in inputValues!)
            {
                string value = pair.Key();
                string label = pair.Value;

                //If textbox value is null/whitespace
                if (string.IsNullOrWhiteSpace(value))
                {
                    errorMessages.Add($"{label} can not be empty");
                }
                //If input is too long
                else if (value.Length > maxChars)
                {
                    errorMessages.Add($"{label} can not be longer than {maxChars} characters");
                }
                //When "Flight time" textbox is reached
                else if (label == "Flight time")
                {
                    //If flight time input can be parsed as int
                    if (int.TryParse(value, out int result))
                    {
                        //If flighttime input is within limits
                        if (result < minFlightTime || result > maxFlightTime)
                        {
                            errorMessages.Add($"{label} has to be an integer between {minFlightTime} and {maxFlightTime}");
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

            //Check if any errors occured
            if (errorMessages.Count > 0)
            {
                string result = string.Join("\n", errorMessages);

                MessageBoxes.DisplayErrorBox(result);
            }
            //Else continue with adding plane to collection
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

        /// <summary>
        /// Resets the input fields.
        /// </summary>
        private void ResetInputFields()
        {
            nameTxtBox.Text = string.Empty;
            flightIDTxtBox.Text = string.Empty;
            destinationTxtBox.Text = string.Empty;
            flightTimeTxtBox.Text = string.Empty;
        }

        /// <summary>
        /// If airplane is selected: calls take off on it.
        /// Else prompts error message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TakeOffBtn_Click(object sender, RoutedEventArgs e)
        {
            if (airplaneListView.SelectedIndex != -1)
            {
                int index = airplaneListView.SelectedIndex;

                //Check if already in flight
                if (!controlTower.Airplanes[index].CanLand)
                {
                    controlTower.OrderTakeOff(index);
                }
                else
                {
                    MessageBoxes.DisplayErrorBox("Airplane is in flight, it can take off again after landing");
                }
            }
            else
            {
                MessageBoxes.DisplayErrorBox("No airplane selected!");
            }
        }

        /// <summary>
        /// If there are airplanes flying: pauses all timers, opens flightlevelwindow.
        /// When flightlevelWindow is closed, resumes all timers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReqAltBtn_Click(object sender, RoutedEventArgs e)
        {
            bool anyCruisingFlights = false;

            foreach (Airplane airplane in controlTower.Airplanes)
            {
                if (airplane.FlightLevel == "FL330")
                {
                    anyCruisingFlights = true;
                }
            }

            if (!anyCruisingFlights)
            {
                MessageBoxes.DisplayErrorBox("No airplanes at cruising altitude!");
                return;
            }

            FlightLevelWindow flightLevelWindow = new(controlTower);

            controlTower.PauseAllTimers();
            flightLevelWindow.ShowDialog();
            controlTower.ResumeAllTimers();
        }
    }
}