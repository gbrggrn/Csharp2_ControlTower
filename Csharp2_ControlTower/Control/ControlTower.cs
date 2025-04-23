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
        public ObservableCollection<FlightMessages> InFlightMessaging { get; private set; }

        public ControlTower()
        {
            Airplanes = [];
            InFlightMessaging = [];
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
            FlightMessages flightEvent = new()
            {
                Name = e.Name,
                Message = e.Message,
            };

            InFlightMessaging.Add(flightEvent);
        }

        internal void OrderTakeOff(int index)
        {
            Airplanes[index].TookOff += OnDisplayInfo!;
            Airplanes[index].Landed += OnDisplayInfo!;

            Airplanes[index].OnTakeOff();
        }

        internal void OrderLanding(int index)
        {
            Airplanes[index].OnLanding();

            Airplanes[index].TookOff -= OnDisplayInfo!;
            Airplanes[index].Landed -= OnDisplayInfo!;
        }
    }
}
