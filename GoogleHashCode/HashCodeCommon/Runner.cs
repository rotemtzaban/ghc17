using HashCodeCommon.Interfaces;
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
        where TInput : IGoodCloneable<TInput>
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

		public int Run(string data, string caseName, int numberOfAttempts = 1, bool printResults = true)
		{
			TInput input = m_Parser.ParseFromData(data);
			TOutput bestResults = GetBestResult(numberOfAttempts, input.Clone());

			string newOutPath = caseName + ".new.out";
			string finalPath = caseName + ".out";

			m_Printer.PrintToFile(bestResults, newOutPath);
			if (printResults)
			{
				m_Printer.PrintToConsole(bestResults);
			}

			if (m_Calculator != null)
			{
                ScoreChange score = ReplaceIfBetter(input.Clone(), finalPath, newOutPath);
				PrintResults(caseName, score.Improvment);
                return score.NewScore;
			}

            return 0;
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
            if (numberOfAttempts == 1)
            {
                return m_Solver.Solve(input.Clone());
            }

			TOutput bestResults = default(TOutput);
			int bestScore = -1;

			for (int i = 0; i < numberOfAttempts; i++)
			{
				TOutput results = m_Solver.Solve(input.Clone());

				int newScore = m_Calculator.Calculate(input.Clone(), results);
				if (newScore > bestScore)
				{
					bestResults = results;
					bestScore = newScore;
				}
			}

			return bestResults;
		}

		private ScoreChange ReplaceIfBetter(TInput input, string finalPath, string newPath)
		{
			if (!File.Exists(newPath))
				throw new ArgumentException("output file wasn't created - " + newPath, "newPath");

			if (!File.Exists(finalPath))
			{
				File.Move(newPath, finalPath);
				return new ScoreChange(m_Calculator.Calculate(input.Clone(), finalPath));
			}

			int newCalc = m_Calculator.Calculate(input.Clone(), newPath);
            try
            {
                int finalCalc = m_Calculator.Calculate(input.Clone(), finalPath);
                if (newCalc > finalCalc)
                {
                    File.Delete(finalPath);
                    File.Move(newPath, finalPath);
                }
                return new ScoreChange(newCalc, newCalc - finalCalc);
            }
            catch
            {
                Console.WriteLine("Warning: old file wasn't valid");
                File.Delete(finalPath);
                File.Move(newPath, finalPath);
                return new ScoreChange(newCalc);
            }
		}

        public void CreateCodeZip()
        {
            var tmpDirectoryName = "tmp";

            var solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))));
            var tmpFolder = Path.Combine(solutionPath, tmpDirectoryName);
            if (Directory.Exists(tmpFolder))
                Directory.Delete(tmpFolder, true);
            Directory.CreateDirectory(tmpFolder);
            foreach (var codeFile in Directory.EnumerateFiles(solutionPath, "*", SearchOption.AllDirectories))
            {
                var relative = codeFile.Substring(solutionPath.Length + 1);
                if (relative.StartsWith("obj") || relative.StartsWith(tmpDirectoryName))
                    continue;
                var target = Path.Combine(tmpFolder, relative);
                var dir = Path.GetDirectoryName(target);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.Copy(codeFile, target);
            }

            var targetZip = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Code.zip");

            if (File.Exists(targetZip))
                File.Delete(targetZip);
            ZipFile.CreateFromDirectory(tmpFolder, targetZip);

            Directory.Delete(tmpFolder, true);

            Console.WriteLine("finish create zip");
        }

        public class ScoreChange
        {
            public int Improvment { get; set; }
            public int NewScore { get; set; }

            public ScoreChange(int newScore)
            {
                this.Improvment = newScore;
                this.NewScore = newScore;
            }

            public ScoreChange(int newScore, int improvment)
            {
                this.Improvment = improvment;
                this.NewScore = newScore;
            }
        }
    }
}
