using Csharp2_ControlTower.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xaml;

namespace Csharp2_ControlTower
{
    /// <summary>
    /// Responsible for adding, dispatching and relaying in flight messages of airplanes.
    /// </summary>
    public class ControlTower
    {
        //Registered airplanes
        public ObservableCollection<Airplane> Airplanes { get; private set; }

        //Registered in flight messages
        public ObservableCollection<FlightMessages> InFlightMessaging { get; private set; }

        /// <summary>
        /// Constructor initializes collections.
        /// </summary>
        public ControlTower()
        {
            Airplanes = [];
            InFlightMessaging = [];
        }

        /// <summary>
        /// Adds test-values to the application.
        /// </summary>
        internal void AddTestValues()
        {
            AirplaneDTO airplane1 = new()
            {
                Name = "Airbus A320",
                Destination = "New York",
                FlightId = "A320NEY",
                FlightTime = 20
            };

            AddPlane(airplane1);

            AirplaneDTO airplane2 = new()
            {
                Name = "Boeing 737",
                Destination = "Säffle",
                FlightId = "B737SFE",
                FlightTime = 15
            };

            AddPlane(airplane2);

            AirplaneDTO airplane3 = new()
            {
                Name = "Cessna 172",
                Destination = "Arlanda",
                FlightId = "C172ARN",
                FlightTime = 12
            };

            AddPlane(airplane3);
        }

        /// <summary>
        /// Unpacks the airplaneDTO and assigns its values to a new Airplane.
        /// Adds the airplane to the Airplanes collection.
        /// </summary>
        /// <param name="airplaneDTO">The airplane Data Transfer Object to be unpacked</param>
        internal void AddPlane(AirplaneDTO airplaneDTO)
        {
            Airplane airplane = new()
            {
                Name = airplaneDTO.Name,
                Destination = airplaneDTO.Destination,
                DestinationDisplay = airplaneDTO.Destination,
                FlightID = airplaneDTO.FlightId,
                FlightTime = airplaneDTO.FlightTime
            };

            airplane.TookOff += OnDisplayInfo!;
            airplane.Landed += OnDisplayInfo!;
            airplane.FlightLevelChanged += OnDisplayInfo!;

            Airplanes.Add(airplane);
        }

        /// <summary>
        /// Triggered upon AirplaneEventArgs event.
        /// Adds information to the InFlightMessaging collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisplayInfo(object sender, AirplaneEventArgs e)
        {
            FlightMessages flightEvent = new()
            {
                Name = e.Name,
                Message = e.Message,
                FlightLevel = e.FlightLevel
            };

            InFlightMessaging.Add(flightEvent);
        }

        /// <summary>
        /// Orders take off by calling the OnTakeOff method in the chosen airplane.
        /// </summary>
        /// <param name="index">Index of the airplane</param>
        internal void OrderTakeOff(int index)
        {
            Airplanes[index].OnTakeOff();
        }

        /// <summary>
        /// Orders a landing. Not used at this time.
        /// </summary>
        /// <param name="index">Index of the airplane</param>
        internal void OrderLanding(int index)
        {
            Airplanes[index].OnLanding();
        }

        /// <summary>
        /// Assigns the method ManualUpdateFlightLevel to the delegate ManualUpdateFlightLevelMethod.
        /// Then invokes the delegate to return a string.
        /// Return string is unused since UI update is done through events.
        /// TODO: I was thinking, however, that delegates would be a great tool to create a "black box" for
        /// this assignment, where controlTower can log actions in Airplane...
        /// </summary>
        /// <param name="index">Index of the airplane</param>
        /// <param name="flightLevelIn">The new flight level</param>
        internal void OrderFlightLevelChange(int index, string flightLevelIn)
        {
            //Assign delegate to method in airplane
            Airplanes[index].ManualUpdateFlightLevelMethod = Airplanes[index].ManualUpdateFlightLevel;

            //Unused string updatedFlightLevel just to show how it can be used with a delegate.
            string updatedFlightLevel = Airplanes[index].ManualUpdateFlightLevelMethod?.Invoke(flightLevelIn)!;
        }

        /// <summary>
        /// Iterates over registered airplanes and stops timers if they are in flight.
        /// </summary>
        internal void PauseAllTimers()
        {
            foreach (Airplane airplane in Airplanes)
            {
                if (airplane.CanLand)
                {
                    airplane.DispatcherTimer.Stop();
                }
            }
        }

        /// <summary>
        /// Iterates over registered airplanes and resumes stopped timers.
        /// </summary>
        internal void ResumeAllTimers()
        {
            foreach (Airplane airplane in Airplanes)
            {
                if (airplane.CanLand)
                {
                    airplane.DispatcherTimer.Start();
                }
            }
        }
    }
}
