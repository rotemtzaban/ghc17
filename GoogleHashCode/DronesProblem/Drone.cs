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
			// TODO: for commands, calculate expected location
			if (this.Commands.Count == 0) {
				return this.Location;
			}

			return this.Commands.Last().ExpectedLocation;
		}

		public Drone()
		{
			this.ID = s_ID++;
			this.WeightLoad = 0;
			this.TurnsUntilAvailable = 1; // Workaround for init case
			this.Commands = new List<CommandBase>();
		}

        public Drone(Drone other)
        {
            this.ID = other.ID;
            this.WeightLoad = other.WeightLoad;
            this.TurnsUntilAvailable = other.TurnsUntilAvailable;
            this.Commands = other.Commands;
        }

		public override bool Equals (object obj)
		{
			Drone d = obj as Drone;
			return d.ID.Equals (this.ID);
		}

		public override int GetHashCode ()
		{
			return this.ID.GetHashCode ();
		}
    }
}
