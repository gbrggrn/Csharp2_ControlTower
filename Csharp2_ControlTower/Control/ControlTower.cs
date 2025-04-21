using Csharp2_ControlTower.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Csharp2_ControlTower
{
    class ControlTower
    {
        private List<Flight> flights;
        private ListBox airplaneListBox;

        public ControlTower(ListBox airplaneListBoxIn)
        {
            flights = [];
            airplaneListBox = airplaneListBoxIn;
        }

        private void AddFlight()
        {
            throw new NotImplementedException();
        }

        private void OnDisplayInfo(object sender, AirplaneEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OrderTakeOff(Flight flight)
        {
            throw new NotImplementedException();
        }

        private void OrderLanding(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
