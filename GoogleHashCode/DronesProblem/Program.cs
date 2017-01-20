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

			runner.Run(DronesProblem.Properties.Resources.Example, "Example");

			runner.CreateCodeZip();
		}
	}
}
