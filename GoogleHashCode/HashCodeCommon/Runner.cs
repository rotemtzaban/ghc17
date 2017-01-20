using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
	public class Runner<TInput, TOutput>
	{
		private IParser<TInput> m_Parser;
		private ISolver<TInput, TOutput> m_Solver;
		private IPrinter<TOutput> m_Printer;
		private IScoreCalculator<TInput, TOutput> m_Calculator;

		public Runner(ParserBase<TInput> parser, ISolver<TInput, TOutput> solver, PrinterBase<TOutput> printer, ScoreCalculatorBase<TInput, TOutput> calculator = null)
		{
			m_Parser = parser;
			m_Solver = solver;
			m_Printer = printer;
			m_Calculator = calculator;

			if (calculator == null)
			{
				Console.WriteLine("Calculator is null. compare to last result won't happened");
			}
		}

		public void Run(string data, string caseName, int numberOfAttempts = 1, bool printResults = true)
		{
			TInput input = m_Parser.ParseFromData(data);
			TOutput bestResults = GetBestResult(numberOfAttempts, input);

			string newOutPath = caseName + ".new.out";
			string finalPath = caseName + ".out";

			m_Printer.PrintToFile(bestResults, newOutPath);
			if (printResults)
			{
				m_Printer.PrintToConsole(bestResults);
			}

			if (m_Calculator != null)
			{
				int improvement = ReplaceIfBetter(input, finalPath, newOutPath);
				PrintResults(caseName, improvement);
			}
		}

		private void PrintResults(string caseName, int improvement)
		{
			if (improvement < 0)
			{
				WriteLineToConsoleInColor("New " + caseName + " was worse. decrease by " + improvement, ConsoleColor.Red);
			}
			else if (improvement == 0)
			{
				WriteLineToConsoleInColor("New " + caseName + " was the same as last", ConsoleColor.Yellow);
			}
			else
			{
				WriteLineToConsoleInColor("New " + caseName + " was better. improve by " + improvement, ConsoleColor.Green);
			}
		}

		private void WriteLineToConsoleInColor(string line, ConsoleColor color)
		{
			ConsoleColor oldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine(line);
			Console.ForegroundColor = oldColor;
		}

		private TOutput GetBestResult(int numberOfAttempts, TInput input)
		{
			TOutput bestResults = default(TOutput);
			int bestScore = -1;

			for (int i = 0; i < numberOfAttempts; i++)
			{
				TOutput results = m_Solver.Solve(input);

				int newScore = m_Calculator.Calculate(input, results);
				if (newScore > bestScore)
				{
					bestResults = results;
					bestScore = newScore;
				}
			}

			return bestResults;
		}

		private int ReplaceIfBetter(TInput input, string finalPath, string newPath)
		{
			if (!File.Exists(newPath))
				throw new ArgumentException("output file wasn't created - " + newPath, "newPath");

			if (!File.Exists(finalPath))
			{
				File.Move(newPath, finalPath);
				return m_Calculator.Calculate(input, newPath);
			}

			int finalCalc = m_Calculator.Calculate(input, finalPath);
			int newCalc = m_Calculator.Calculate(input, newPath);
			if (newCalc > finalCalc)
			{
				File.Delete(finalPath);
				File.Move(newPath, finalPath);
			}
			return newCalc - finalCalc;
		}

        public void CreateCodeZip()
        {
            var tmpDirectoryName = "tmp";

            var sourceDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            var tmpFolder = Path.Combine(sourceDir, tmpDirectoryName);
            if (Directory.Exists(tmpFolder))
                Directory.Delete(tmpFolder, true);
            Directory.CreateDirectory(tmpFolder);
            foreach (var codeFile in Directory.EnumerateFiles(sourceDir, "*.cs", SearchOption.AllDirectories))
            {
                var relative = codeFile.Substring(sourceDir.Length + 1);
                if (relative.StartsWith("obj") || relative.StartsWith(tmpDirectoryName))
                    continue;
                var target = Path.Combine(tmpFolder, relative);
                var dir = Path.GetDirectoryName(target);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.Copy(codeFile, target);
            }

            var targetZip = Path.Combine(sourceDir, "out", "Code.zip");

            if (File.Exists(targetZip))
                File.Delete(targetZip);
            ZipFile.CreateFromDirectory(tmpFolder, targetZip);

            Directory.Delete(tmpFolder, true);
        }
    }
}
