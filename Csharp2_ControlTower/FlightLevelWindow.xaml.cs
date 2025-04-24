using Csharp2_ControlTower.Model;
using CSharp2_ControlTower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Csharp2_ControlTower
{
    /// <summary>
    /// Interaction logic for FlightLevelWindow.xaml
    /// </summary>
    public partial class FlightLevelWindow : Window
    {
        private readonly ControlTower controlTower;

        /// <summary>
        /// Constructor assigns instance variable,
        /// sets datacontext for the window and assigns itemsSource of activeFlightsListView to Airplanes collection.
        /// </summary>
        /// <param name="controlTowerIn">The current instance of controlTower</param>
        public FlightLevelWindow(ControlTower controlTowerIn)
        {
            InitializeComponent();
            controlTower = controlTowerIn;
            this.DataContext = controlTower;
            activeFlightsListView.ItemsSource = controlTower.Airplanes;
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Initiates the calls for assigning a new Flight level
        /// or prompts an error message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFlBtn_Click(object sender, RoutedEventArgs e)
        {
            if (activeFlightsListView.SelectedIndex != -1)
            {
                string input = newFlTxtBox.Text;
                int index = activeFlightsListView.SelectedIndex;

                //If flightlevel is not cruising altitude of FL330
                if (controlTower.Airplanes[index].FlightLevel != "FL330")
                {
                    MessageBoxes.DisplayErrorBox("Flight level can only be changed at cruising altitude FL330");
                    return;
                }

                //If the chosen airplane is not flying
                if (controlTower.Airplanes[index].CanLand == false)
                {
                    MessageBoxes.DisplayErrorBox("The chosen airplane is not in flight");
                    return;
                }

                //If the input is empty
                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBoxes.DisplayErrorBox("Flight level cannot be empty");
                    return;
                }

                //Match to regex expression of "FL000-FL999"
                if (!Regex.IsMatch(input, @"^FL\d{3}$"))
                {
                    MessageBoxes.DisplayErrorBox("Flight level has to be in the format FL000-FL999");
                    return;
                }

                //If all above is correct - initialize change of altitude
                controlTower.OrderFlightLevelChange(index, input);
                this.Close();
            }
            //If not flight is selected
            else
            {
                MessageBoxes.DisplayErrorBox("No flight selected");
            }
        }
    }
}
