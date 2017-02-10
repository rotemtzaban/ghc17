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
		}

		public int Run(string data, string caseName, int numberOfAttempts = 1, bool printResults = true)
		{
            TOutput bestResults = GetBestResult(numberOfAttempts, data);

			string newOutPath = caseName + ".new.out";
			string finalPath = caseName + ".out";

			m_Printer.PrintToFile(bestResults, newOutPath);
			if (printResults)
			{
				m_Printer.PrintToConsole(bestResults);
			}

			if (m_Calculator != null)
			{
                ScoreChange score = ReplaceIfBetter(data, finalPath, newOutPath);
				PrintResults(caseName, score);
                return score.NewScore;
			}
            else
            {
                Console.WriteLine(caseName + ": Calculator is null. No comparison was made");
            }

            return 0;
		}

        public TInput GetInput(string data)
        {
            return m_Parser.ParseFromData(data);
        }

		private void PrintResults(string caseName, ScoreChange scoreChange)
		{
			if (scoreChange.Improvment < 0)
			{
				WriteLineToConsoleInColor(caseName + ": new was worse: "+scoreChange.NewScore +". decrease by " + scoreChange.Improvment, ConsoleColor.Red);
			}
			else if (scoreChange.Improvment == 0)
			{
				WriteLineToConsoleInColor(caseName + ": new was the same as last: " + scoreChange.NewScore, ConsoleColor.Yellow);
			}
			else
			{
				WriteLineToConsoleInColor(caseName + " new was better: " + scoreChange.NewScore + ". improve by " + scoreChange.Improvment, ConsoleColor.Green);
			}
		}

		private void WriteLineToConsoleInColor(string line, ConsoleColor color)
		{
			ConsoleColor oldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine(line);
			Console.ForegroundColor = oldColor;
		}

		private TOutput GetBestResult(int numberOfAttempts, string data)
        {
            if (numberOfAttempts == 1)
            {
                return m_Solver.Solve(GetInput(data));
            }

            TOutput bestResults = default(TOutput);
            int bestScore = -1;
            int bestSeed = -1;
            Random seedesGenerator = new Random();

            for (int i = 0; i < numberOfAttempts; i++)
            {
                Console.Write("Running attempt {0}/{1}...                            \r", i, numberOfAttempts);

                int seed = seedesGenerator.Next();
                Random random = new Random(seed);
                TOutput results = m_Solver.Solve(GetInput(data), random);

                int newScore = m_Calculator.Calculate(GetInput(data), results);
                if (newScore > bestScore)
                {
                    bestSeed = seed;
                    bestResults = results;
                    bestScore = newScore;
                }
            }
            Console.WriteLine();
            bestResults = CompareAndUpdateBestSeed(data, bestResults, bestScore, bestSeed);

            return bestResults;
        }

        private TOutput CompareAndUpdateBestSeed(string data, TOutput bestResults, int bestScore, int bestSeed)
        {
            string seedsFile = "seeds.txt";

            if (!File.Exists(seedsFile))
            {
                File.WriteAllLines(seedsFile, new string[] { bestSeed.ToString() });
            }
            else
            {
                int seed = int.Parse(File.ReadAllLines(seedsFile)[0]);
                Random random = new Random(seed);
                TOutput results = m_Solver.Solve(GetInput(data), random);

                int newScore = m_Calculator.Calculate(GetInput(data), results);
                if (newScore > bestScore)
                {
                    bestSeed = seed;
                    bestResults = results;
                    File.WriteAllLines(seedsFile, new string[] { bestSeed.ToString() });
                }
            }

            return bestResults;
        }

        private ScoreChange ReplaceIfBetter(string data, string finalPath, string newPath)
		{
			if (!File.Exists(newPath))
				throw new ArgumentException("output file wasn't created - " + newPath, "newPath");

			if (!File.Exists(finalPath))
			{
				File.Move(newPath, finalPath);
				return new ScoreChange(m_Calculator.Calculate(GetInput(data), finalPath));
			}

			int newCalc = m_Calculator.Calculate(GetInput(data), newPath);
            try
            {
                int finalCalc = m_Calculator.Calculate(GetInput(data), finalPath);
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
