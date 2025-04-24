using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Csharp2_ControlTower.Model
{
    public class Airplane
    {
        //Properties
        public bool CanLand { get; set; }
        public string Destination { get; set; } = "Home";
        public string FlightID { get; set; } = string.Empty;
        public double FlightTime { get; set; }
        public TimeOnly LocalTime { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FlightLevel { get; set; } = "FL000";
        public double FlightTimeElapsed { get; set; }

        //Timer
        private DispatcherTimer? dispatcherTimer;
        public DispatcherTimer DispatcherTimer => dispatcherTimer!;

        //Events
        public event EventHandler<AirplaneEventArgs>? TookOff;
        public event EventHandler<AirplaneEventArgs>? Landed;
        public event EventHandler<AirplaneEventArgs>? FlightLevelChanged;

        //Flags
        private bool ManualFlightLevelChanged { get; set; } = false;
        private int ManualFlightLevelOvveride { get; set; } = 0;

        //Delegates
        public delegate string ManualUpdateFlightLevelDelegate(string newLevel);
        public ManualUpdateFlightLevelDelegate? ManualUpdateFlightLevelMethod;

        /// <summary>
        /// Updates the state on each click until the airplane has landed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            CanLand = true;
            FlightTimeElapsed++;
            UpdateFlightLevel();

            //When landing is reached
            if (FlightTimeElapsed == FlightTime)
            {
                OnLanding();
            }
        }

        /// <summary>
        /// Manually updates the FlightLevel.
        /// Is called through a normal delegate to satisfy B-level of the assignment,
        /// however the actual GUI update is executed through an event to avoid injecting
        /// logic into Airplane.
        /// </summary>
        /// <param name="flightLevelIn">The new flightlevel to be assigned</param>
        /// <returns>The current flightlevel</returns>
        internal string ManualUpdateFlightLevel(string flightLevelIn)
        {
            FlightLevel = flightLevelIn;
            FlightLevelChanged?.Invoke(this, new AirplaneEventArgs(Name, "manual flight level override", FlightLevel));
            ManualFlightLevelChanged = true;
            ManualFlightLevelOvveride = 4;
            return FlightLevel;
        }

        /// <summary>
        /// Updates the flightlevel through an event depending on phase of flight: climbing, cruising or landing.
        /// </summary>
        internal void UpdateFlightLevel()
        {
            double flightProgression = FlightTimeElapsed / FlightTime;
            string newLevel;

            //If climbing
            if (flightProgression < 0.2)
            {
                newLevel = "FL070";
            }
            //If crusing
            else if (flightProgression < 0.8)
            {
                newLevel = "FL330";

                //If flight level has been changed manually during cruise
                if (ManualFlightLevelChanged && ManualFlightLevelOvveride > 0)
                {
                    ManualFlightLevelOvveride--;
                    return;
                }
            }
            //When landning
            else
            {
                newLevel = "FL090";
                ResetManualOverride();
            }

            if (FlightLevel != newLevel)
            {
                string message = "flight level changed";
                FlightLevel = newLevel;
                if (newLevel == "FL070")
                    message = "take off altitude assigned, manual override disabled";
                else if (newLevel == "FL330")
                {
                    if (ManualFlightLevelChanged && ManualFlightLevelOvveride == 0)
                    {
                        message = "manual override ended, reverting to cruising altitude";
                    }
                    else
                    {
                        message = "crusing altitude assigned, manual override enabled";
                    }
                }
                else if (newLevel == "FL090")
                    message = "landing altitude assigned, manual override disabled";
                FlightLevelChanged?.Invoke(this, new AirplaneEventArgs(Name, message, FlightLevel));
            }
        }

        /// <summary>
        /// Resets the manual flight level override properties.
        /// </summary>
        private void ResetManualOverride()
        {
            ManualFlightLevelChanged = false;
            ManualFlightLevelOvveride = 0;
        }

        /// <summary>
        /// Executes upon landing.
        /// Resets all mutable properties and triggers the Landed event.
        /// </summary>
        internal void OnLanding()
        {
            FlightLevel = "FL000";
            ManualFlightLevelChanged = false;
            LocalTime = TimeOnly.FromDateTime(DateTime.Now);
            string timeWithSeconds = LocalTime.ToString("HH:mm:ss");
            Landed?.Invoke(this, new AirplaneEventArgs(Name, $"landed in {Destination}, {timeWithSeconds}!", FlightLevel));
            StopTimer();
            CanLand = false;
            Destination = "Home";
            FlightTimeElapsed = 0;
        }

        /// <summary>
        /// Called on take off.
        /// Calls the timer setup, and triggers the TakeOff event.
        /// </summary>
        internal void OnTakeOff()
        {
            SetupTimer();

            LocalTime = TimeOnly.FromDateTime(DateTime.Now);
            string timeWithSeconds = LocalTime.ToString("HH:mm:ss");
            TookOff?.Invoke(this, new AirplaneEventArgs(Name, $"taking off, destination {Destination}, {timeWithSeconds}!", FlightLevel));
        }

        /// <summary>
        /// Instantiates and assigns a new timer.
        /// Sets up subscription to Tick-method, defines interval and starts the timer.
        /// </summary>
        internal void SetupTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick!);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        internal void StopTimer()
        {
            dispatcherTimer?.Stop();
        }
    }
}
