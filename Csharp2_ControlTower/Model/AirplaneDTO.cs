using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp2_ControlTower.Model
{
    class AirplaneDTO
    {
        public required string Name { get; set; }
        public required string FlightId { get; set; }
        public required string Destination { get; set; }
        public int FlightTime { get; set; }
    }
}
