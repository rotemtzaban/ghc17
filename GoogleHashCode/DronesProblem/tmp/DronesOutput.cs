using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class DronesOutput
    {
		public List<CommandBase> Commands { get; set; }

		public DronesOutput()
		{
			this.Commands = new List<CommandBase> ();
		}
    }
}
