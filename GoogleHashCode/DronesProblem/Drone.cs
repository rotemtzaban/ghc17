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
		private static uint s_ID = 0;

		public uint ID { get; set; }
		public Coordinate Location { get; set; }

		public uint WeightLoad { get; set; }

		public List<CommandBase> Commands { get; set; }

		public uint TurnsUntilAvailable { get; set; }

		public Coordinate GetExpectedLocation ()
		{
			return Location;
			// TODO: for commands, calculate expected location
			throw new NotImplementedException();
		}

		public Drone()
		{
			this.ID = s_ID++;
			this.WeightLoad = 0;
			this.TurnsUntilAvailable = 0;
			this.Commands = new List<CommandBase>();
		}

        public Drone(Drone other)
        {
            this.ID = other.ID;
            this.WeightLoad = other.WeightLoad;
            this.TurnsUntilAvailable = other.TurnsUntilAvailable;
            this.Commands = other.Commands;
        }
    }
}
