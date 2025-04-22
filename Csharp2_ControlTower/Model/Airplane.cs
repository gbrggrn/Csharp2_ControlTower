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
        public int FlightDuration { get; set; }

        //Timer
        private DispatcherTimer? dispatcherTimer;

        //Events
        public event EventHandler<AirplaneEventArgs>? TookOff;
        public event EventHandler<AirplaneEventArgs>? Landed;

        internal void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            FlightDuration--;

            if (FlightDuration <= 0)
            {
                OnLanding();
            }
        }

        internal void OnLanding()
        {
            LocalTime = TimeOnly.FromDateTime(DateTime.Now);
            Landed?.Invoke(this, new AirplaneEventArgs(Name, $" has landed in {Destination}, {LocalTime}!"));
        }

        internal void OnTakeOff()
        {
            FlightDuration = new Random().Next(5, 16);
            SetupTimer();

            LocalTime = TimeOnly.FromDateTime(DateTime.Now);
            TookOff?.Invoke(this, new AirplaneEventArgs(Name, $" is taking off, destination {Destination}, {LocalTime}!"));
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
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
