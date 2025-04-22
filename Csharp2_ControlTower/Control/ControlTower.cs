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
        private List<Airplane> airplanes;
        private ListBox airplaneListBox;

        public ControlTower(ListBox airplaneListBoxIn)
        {
            airplanes = [];
            airplaneListBox = airplaneListBoxIn;
        }

        private void AddPlane()
        {
            throw new NotImplementedException();
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
