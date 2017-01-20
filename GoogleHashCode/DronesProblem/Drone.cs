using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Drone
    {
        public Coordinate Location { get; set; }

		public uint WeightLoad { get; set; }

		//List<ICommand> Commands { get; set; }

		public uint ID { get; set; }

		public uint TurnsUntilAvailable { get; set; }

		//public 

		public Drone(uint id)
		{
			this.ID = id;
			this.WeightLoad = 0;
			this.TurnsUntilAvailable = 0;
		}
    }
}
