using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem.Commands
{
	public class WaitCommand : CommandBase
	{
		public WaitCommand(Drone drone, uint turnCount)
		{
			Drone = drone;
			TurnCount = turnCount;
			this.TurnsToComplete = turnCount;
		}

		public uint TurnCount { get; set; }
		public override string Tag
		{
			get { return "W"; }
		}

		public override string GetOutputLine()
		{
			return string.Format("{0} {1} {2}", Drone.ID, Tag, TurnCount);
		}
	}
}
