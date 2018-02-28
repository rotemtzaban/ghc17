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
            Runner<DronesInput, DronesOutput>  runner = new Runner<DronesInput, DronesOutput>("Drones", new DronesParser(), new DronesSolver(), new DronesPrinter(), null);
            runner.Run(Properties.Resources.Example, "Example", 1, false);

			//runner = new Runner<DronesInput, DronesOutput>(new DronesParser(), new DronesSolver(), new DronesPrinter(), new DronesScoreCalculator());
			//int score = runner.Run(Properties.Resources.mother_of_all_warehouses, "mother_of_all_warehouses", 1, false);

			//runner = new Runner<DronesInput, DronesOutput>(new DronesParser(), new DronesSolver(), new DronesPrinter(), new DronesScoreCalculator());
			//score += runner.Run(Properties.Resources.busy_day, "busy_day",1, false);

			//runner = new Runner<DronesInput, DronesOutput>(new DronesParser(), new DronesSolver(), new DronesPrinter(), new DronesScoreCalculator());
			//score += runner.Run(Properties.Resources.redundancy, "redundancy", 1, false);

			//Console.WriteLine("Final Score - " + score);
			//runner.CreateCodeZip();
            Console.Read();
		}
	}
}
