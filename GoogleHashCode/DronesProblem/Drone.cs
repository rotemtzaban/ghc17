using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
	public class Drone : IndexedObject
    {
		public Coordinate Location { get; set; }

		public uint WeightLoad { get; set; }

		public List<CommandBase> Commands { get; set; }

		public uint TurnsUntilAvailable { get; set; }

		public Coordinate GetExpectedLocation ()
		{			
			// TODO: for commands, calculate expected location
			if (this.Commands.Count == 0) {
				return this.Location;
			}

			return this.Commands.Last().ExpectedLocation;
		}

		public Drone(int index)
            :base (index)
		{
			this.WeightLoad = 0;
			this.TurnsUntilAvailable = 1; // Workaround for init case
			this.Commands = new List<CommandBase>();
		}
    }
}
