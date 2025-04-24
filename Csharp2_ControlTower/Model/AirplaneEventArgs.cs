using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp2_ControlTower.Model
{
    /// <summary>
    /// Represents event data for en airplane event.
    /// </summary>
    public class AirplaneEventArgs : EventArgs
    {
        //Properties
        public string Message { get; }
        public string Name { get; }
        public string FlightLevel { get; }

        //Constructor initializes properties.
        public AirplaneEventArgs(string nameIn, string messageIn, string flightLevelIn)
        {
            Message = messageIn;
            Name = nameIn;
            FlightLevel = flightLevelIn;
        }
    }
}
