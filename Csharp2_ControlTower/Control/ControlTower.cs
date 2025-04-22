using Csharp2_ControlTower.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Csharp2_ControlTower
{
    class ControlTower
    {
        public ObservableCollection<Airplane> Airplanes { get; private set; }

        public ControlTower()
        {
            Airplanes = [];
        }

        internal void AddPlane(AirplaneDTO airplaneDTO)
        {
            Airplane airplane = new Airplane();
            airplane.Name = airplaneDTO.Name;
            airplane.Destination = airplaneDTO.Destination;
            airplane.FlightID = airplaneDTO.FlightId;
            airplane.FlightTime = airplaneDTO.FlightTime;

            Airplanes.Add(airplane);
        }

        private void OnDisplayInfo(object sender, AirplaneEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OrderTakeOff(int index)
        {
            throw new NotImplementedException();
        }

        private void OrderLanding(int index)
        {
            throw new NotImplementedException();
        }
    }
}
