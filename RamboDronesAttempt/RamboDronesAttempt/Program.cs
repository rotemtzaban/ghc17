using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodeCommon;
using RamboDronesAttempt.IO;
using RamboDronesAttempt.Model;
using RamboDronesAttempt.Solver;

namespace RamboDronesAttempt
{
	public class Program
	{
		static void Main(string[] args)
		{
			var runner = new Runner<DronesInput, DronesOutput>(new DronesParser(), new DronesSolver(), new DronesPrinter(), null);
			//runner.Run(Properties.Resources.Example, "Example", 1, false);
		}
	}
}
