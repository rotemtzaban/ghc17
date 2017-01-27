using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodeCommon;

namespace DronesProblem
{
	public abstract class CommandBase
	{
		public Drone Drone { get; set; }
		public abstract string Tag { get; }

		public uint TurnsToComplete { get; set; } 

		public Coordinate ExpectedLocation { get; set; }

		public abstract string GetOutputLine();

		public override string ToString()
		{
			return GetOutputLine();
		}
	}
}
