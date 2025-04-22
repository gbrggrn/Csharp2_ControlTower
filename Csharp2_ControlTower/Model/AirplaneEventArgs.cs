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

        //Properties
        public string Message => message;
        public string Name => name;

        public AirplaneEventArgs(string nameIn, string messageIn)
        {
            this.message = messageIn;
            this.name = nameIn;
        }
    }
}
