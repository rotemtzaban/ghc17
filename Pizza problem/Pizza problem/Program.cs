using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_problem.Properties;

namespace Pizza_problem
{
    class Program
    {
        static void Main(string[] args)
        {
			// TODO: Always take the best solution ever (since inputs are static)
			RunOnExample(true);
			// RunOnSmall();
			// RunOnMedium(false);
			// RunOnBig(false);

	        Console.WriteLine("Done!");
	        Console.ReadKey();
        }

		private static void RunOnExample(bool printResults = true)
		{
			RunOnInput(Resources.example, "example.out", printResults);
		}

		private static void RunOnSmall(bool printResults = true)
		{
			RunOnInput(Resources.small, "small.out", printResults);
		}

		private static void RunOnMedium(bool printResults = true)
		{
			RunOnInput(Resources.medium, "medium.out", printResults);
		}

		private static void RunOnBig(bool printResults = true)
		{
			RunOnInput(Resources.big, "big.out", printResults);
		}

		private static void RunOnInput(string data, string outputPath = null, bool printResults = true)
	    {
		    var parser = new Parser();
			var pizza = parser.ParseData(data);

		    var solver = new PizzaSolverBasic(pizza);
		    var results = solver.Solve();

		    var printer = new PizzaPrinter();
			printer.PrintToFile(results, outputPath);
			if (printResults)
				printer.PrintToConsole(pizza, results);
	    }
    }
}
