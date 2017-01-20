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
		public int ID { get; set; }
        public Coordinate Location { get; set; }

		public uint Weight { get; set; }

		//List<ICommand> Commands { get; set; }

		public uint ID { get; set; }

		public uint TurnsUntilAvailable { get; set; }

		public Drone(uint id, uint weight)
		{
			this.ID = id;
			this.Weight = weight;
			this.TurnsUntilAvailable = 0;
		}
    }
}
