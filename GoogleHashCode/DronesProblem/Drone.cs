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
		public uint ID { get; set; }
        public Coordinate Location { get; set; }

		public uint WeightLoad { get; set; }

		public List<CommandBase> Commands { get; set; }

		public uint TurnsUntilAvailable { get; set; }

		//public 

		public Drone(uint id)
		{
			this.ID = id;
			this.WeightLoad = 0;
			this.TurnsUntilAvailable = 0;
			this.Commands = new List<CommandBase> ();
		}
    }
}
