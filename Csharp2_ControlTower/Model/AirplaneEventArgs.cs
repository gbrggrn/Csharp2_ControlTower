using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp2_ControlTower.Model
{
    class AirplaneEventArgs : EventArgs
    {
        private string message;
        private string name;
        private string flightLevel;

        //Properties
        public string Message => message;
        public string Name => name;
        public string FlightLevel => flightLevel;

        public AirplaneEventArgs(string nameIn, string messageIn, string flightLevelIn)
        {
            this.message = messageIn;
            this.name = nameIn;
            this.flightLevel = flightLevelIn;
        }
    }
}
