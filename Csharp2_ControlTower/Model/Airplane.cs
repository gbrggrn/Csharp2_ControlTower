using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Csharp2_ControlTower.Model
{
    class Airplane
    {
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

        //Events
        public event EventHandler<AirplaneEventArgs>? TookOff;
        public event EventHandler<AirplaneEventArgs>? Landed;
        public event EventHandler<AirplaneEventArgs>? FlightLevelChanged;

        internal void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            CanLand = true;
            FlightTimeElapsed++;
            UpdateFlightLevel();

            if (FlightTimeElapsed == FlightTime)
            {
                OnLanding();
            }
        }

        internal void ManualUpdateFlightLevel(string flightLevelIn)
        {
            FlightLevel = flightLevelIn;
            FlightLevelChanged?.Invoke(this, new AirplaneEventArgs(Name, "manual flight level change", FlightLevel));
        }

        internal void UpdateFlightLevel()
        {
            double flightTimeRemaining = FlightTime - FlightTimeElapsed;
            string newLevel;

            if (flightTimeRemaining < FlightTime * 0.3)
            {
                newLevel = "FL090";
            }
            else if (flightTimeRemaining < FlightTime * 0.8)
            {
                newLevel = "FL330";
            }
            else
            {
                newLevel = "FL070";
            }

            if (FlightLevel != newLevel)
            {
                FlightLevel = newLevel;
                FlightLevelChanged?.Invoke(this, new AirplaneEventArgs(Name, "flight level changed", FlightLevel));
            }
        }

        internal void OnLanding()
        {
            FlightLevel = "FL000";
            LocalTime = TimeOnly.FromDateTime(DateTime.Now);
            string timeWithSeconds = LocalTime.ToString("HH:mm:ss");
            Landed?.Invoke(this, new AirplaneEventArgs(Name, $" has landed in {Destination}, {timeWithSeconds}!", FlightLevel));
            StopTimer();
            CanLand = false;
            Destination = "Home";
            FlightTimeElapsed = 0;
        }

        internal void OnTakeOff()
        {
            SetupTimer();

            LocalTime = TimeOnly.FromDateTime(DateTime.Now);
            string timeWithSeconds = LocalTime.ToString("HH:mm:ss");
            TookOff?.Invoke(this, new AirplaneEventArgs(Name, $" is taking off, destination {Destination}, {timeWithSeconds}!", FlightLevel));
        }

        internal void SetupTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick!);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        internal void StopTimer()
        {
            dispatcherTimer?.Stop();
        }
    }
}
