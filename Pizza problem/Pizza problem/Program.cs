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
			RunOnExample(5, false);
			RunOnSmall(5, true);
			RunOnMedium(5, false);
			RunOnBig(5, false);

			Console.WriteLine("Done!");
			Console.ReadKey();
		}

		private static void RunOnExample(int numberOfAttempts, bool printResults = true)
		{
			var data = Resources.example;
			var newOutPath = "example.new.out";
			var finalPath = "example.out";

			RunOnInput(data, numberOfAttempts, newOutPath, printResults);
			var improvement = ReplaceIfBetter(finalPath, newOutPath);
			if (improvement < 0)
				Console.WriteLine("New " + finalPath + " was worse than last and wasn't replaced");
			else if (improvement == 0)
				Console.WriteLine("New " + finalPath + " was the same as last");
		}

		private static void RunOnSmall(int numberOfAttempts, bool printResults = true)
		{
			var data = Resources.small;
			var newOutPath = "small.new.out";
			var finalPath = "small.out";

			RunOnInput(data, numberOfAttempts, newOutPath, printResults);
			var improvement = ReplaceIfBetter(finalPath, newOutPath);
			if (improvement < 0)
				Console.WriteLine("New " + finalPath + " was worse than last and wasn't replaced");
			else if (improvement == 0)
				Console.WriteLine("New " + finalPath + " was the same as last");
		}

		private static void RunOnMedium(int numberOfAttempts, bool printResults = true)
		{
			var data = Resources.medium;
			var newOutPath = "medium.new.out";
			var finalPath = "medium.out";

			RunOnInput(data, numberOfAttempts, newOutPath, printResults);
			var improvement = ReplaceIfBetter(finalPath, newOutPath);
			if (improvement < 0)
				Console.WriteLine("New " + finalPath + " was worse than last and wasn't replaced");
			else if (improvement == 0)
				Console.WriteLine("New " + finalPath + " was the same as last");
		}

		private static void RunOnBig(int numberOfAttempts, bool printResults = true)
		{
			var data = Resources.big;
			var newOutPath = "big.new.out";
			var finalPath = "big.out";

			RunOnInput(data, numberOfAttempts, newOutPath, printResults);
			var improvement = ReplaceIfBetter(finalPath, newOutPath);
			if (improvement < 0)
				Console.WriteLine("New " + finalPath + " was worse than last and wasn't replaced");
			else if (improvement == 0)
				Console.WriteLine("New " + finalPath + " was the same as last");
		}

		private static void RunOnInput(string data, int numberOfAttempts, string outputPath, bool printResults = true)
		{
			IEnumerable<PizzaSlice> bestResults = null;
			var parser = new Parser();
			var pizza = parser.ParseData(data);

			var bestScore = -2;
			for (int i = 0; i < numberOfAttempts; i++)
			{
				var solver = new PizzaSolverBlat(pizza);
				var results = solver.Solve();

				var calc = new ScoreCalc(results);
				if (calc.Score > bestScore)
				{
					bestResults = results;
					bestScore = calc.Score;
				}
			}

			var printer = new PizzaPrinter();
			printer.PrintToFile(bestResults, outputPath);
			if(printResults)
				printer.PrintToConsole(pizza, bestResults);
		}

		private static int ReplaceIfBetter(string finalPath, string newPath)
		{
			if (!File.Exists(newPath))
				return 0;

			if (!File.Exists(finalPath))
			{
				var calc = new ScoreCalc(newPath);
				File.Move(newPath, finalPath);
				return calc.Score;
			}

			var finalCalc = new ScoreCalc(finalPath);
			var newCalc = new ScoreCalc(newPath);
			if (newCalc.Score > finalCalc.Score)
			{
				File.Delete(finalPath);
				File.Move(newPath, finalPath);
			}
			return newCalc.Score - finalCalc.Score;
		}
	}
}
