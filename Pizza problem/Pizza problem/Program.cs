using System;
using System.Collections.Generic;
using System.IO;
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
			RunOnExample(false);
			RunOnSmall(true);
			RunOnMedium(false);
			RunOnBig(false);

	        Console.WriteLine("Done!");
	        Console.ReadKey();
        }

		private static void RunOnExample(bool printResults = true)
		{
			RunOnInput(Resources.example, "example.new.out", printResults);
			if(!ReplaceIfBetter("example.out", "example.new.out"))
				Console.WriteLine("New example.out was worse than last and wasn't replaced");
		}

	    private static void RunOnSmall(bool printResults = true)
		{
			RunOnInput(Resources.small, "small.new.out", printResults);
			if(!ReplaceIfBetter("small.out", "small.new.out"))
				Console.WriteLine("New small.out was worse than last and wasn't replaced");
		}

		private static void RunOnMedium(bool printResults = true)
		{
			RunOnInput(Resources.medium, "medium.new.out", printResults);
			if(!ReplaceIfBetter("medium.out", "medium.new.out"))
				Console.WriteLine("New medium.out was worse than last and wasn't replaced");
		}

		private static void RunOnBig(bool printResults = true)
		{
			RunOnInput(Resources.big, "big.new.out", printResults);
			if(!ReplaceIfBetter("big.out", "big.new.out"))
				Console.WriteLine("New big.out was worse than last and wasn't replaced");
		}

		private static void RunOnInput(string data, string outputPath = null, bool printResults = true)
	    {
		    var parser = new Parser();
			var pizza = parser.ParseData(data);

		    var solver = new PizzaSolverBlat(pizza);
		    var results = solver.Solve();

		    var printer = new PizzaPrinter();
			printer.PrintToFile(results, outputPath);
			if (printResults)
				printer.PrintToConsole(pizza, results);
	    }

		private static bool ReplaceIfBetter(string finalPath, string newPath)
		{
			if (!File.Exists(newPath))
				return false;

			if (!File.Exists(finalPath))
			{
				File.Move(newPath, finalPath);
				return true;
			}

			var finalCalc = new ScoreCalc(finalPath);
			var newCalc = new ScoreCalc(newPath);
			if (newCalc.Score < finalCalc.Score) 
				return false;

			File.Delete(finalPath);
			File.Move(newPath, finalPath);
			return true;
		}
	}
}
