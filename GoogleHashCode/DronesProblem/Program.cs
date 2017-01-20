using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
	class Program
	{
		static void Main(string[] args)
		{
			Runner<DronesInput, DronesOutput> runner = new Runner<DronesInput, DronesOutput>(new DronesParser(), new DronesSolver(), new DronesPrinter(), new DronesScoreCalculator());

			runner.Run(Properties.Resources.Example, "Example");
			// runner.Run(Properties.Resources.busy_day, "busy_day");
			// runner.Run(Properties.Resources.mother_of_all_warehouses, "mother_of_all_warehouses");
			// runner.Run(Properties.Resources.redundancy, "redundancy");

			runner.CreateCodeZip();
		}
	}
}
