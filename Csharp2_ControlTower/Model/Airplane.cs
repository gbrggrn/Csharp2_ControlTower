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
        public string Destination { get; set; }
        public string FlightID { get; set; }
        public double FlightTime { get; set; }
        public TimeOnly LocalTime { get; set; }
        public string Name { get; set; }

        //Timer
        private DispatcherTimer dispatcherTimer;

        public Airplane()
        {
            SetupTimer();
        }

        internal void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void OnLanding()
        {
            throw new NotImplementedException();
        }

        internal void OnTakeOff()
        {
            throw new NotImplementedException();
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
