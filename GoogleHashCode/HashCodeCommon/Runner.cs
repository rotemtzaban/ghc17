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
        private string m_OutputDirectory;

		public Runner(string outputDirectoryName, ParserBase<TInput> parser, SolverBase<TInput, TOutput> solver, PrinterBase<TOutput> printer, ScoreCalculatorBase<TInput, TOutput> calculator = null)
		{
			m_Parser = parser;
			m_Solver = solver;
			m_Printer = printer;
			m_Calculator = calculator;
            var solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))));
            m_OutputDirectory = Path.Combine(solutionPath, "Output", outputDirectoryName);

            Directory.CreateDirectory(m_OutputDirectory);
        }

        public long Run(string data, string caseName, int numberOfAttempts = 1, bool printResults = true)
		{
            TOutput bestResults = GetBestResult(numberOfAttempts, data, caseName);

			string newOutPath = Path.Combine(m_OutputDirectory, caseName + ".new.out");
			string finalPath = Path.Combine(m_OutputDirectory, caseName + ".out");

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

		private TOutput GetBestResult(int numberOfAttempts, string data, string caseName)
        {
            if (numberOfAttempts == 1)
            {
                return m_Solver.Solve(GetInput(data), new Random(), caseName);
            }

            TOutput bestResults = default(TOutput);
            long bestScore = -1;
            int bestSeed = -1;
            Random seedesGenerator = new Random();

            for (int i = 0; i < numberOfAttempts; i++)
            {
                Console.Write("Running attempt {0}/{1}...                            \r", i, numberOfAttempts);

                int seed = seedesGenerator.Next();
                Random random = new Random(seed);
                TOutput results = m_Solver.Solve(GetInput(data), random, caseName);

                long newScore = m_Calculator.Calculate(GetInput(data), results);
                if (newScore > bestScore)
                {
                    bestSeed = seed;
                    bestResults = results;
                    bestScore = newScore;
                }
            }
            Console.WriteLine();
            bestResults = CompareAndUpdateBestSeed(data, bestResults, bestScore, bestSeed, caseName);

            return bestResults;
        }

        private TOutput CompareAndUpdateBestSeed(string data, TOutput bestResults, long bestCurrentScore, int bestSeedFound, string caseName)
        {
            string seedsFile = "seeds.txt";

            int bestSeedOfAllTimes = bestSeedFound;
            TOutput bestResultOfAllTimes = bestResults;

            if (File.Exists(seedsFile))
            {
                int savedSeed = int.Parse(File.ReadLines(seedsFile).First());
                Random random = new Random(savedSeed);
                TOutput resultOfSavedSeed = m_Solver.Solve(GetInput(data), random, caseName);

                long scoreOfSavedSeed = m_Calculator.Calculate(GetInput(data), resultOfSavedSeed);
                if (scoreOfSavedSeed >= bestCurrentScore)
                {
                    bestSeedOfAllTimes = savedSeed;
                    bestResultOfAllTimes = resultOfSavedSeed;
                }
            }

            File.WriteAllText(seedsFile, bestSeedOfAllTimes.ToString()); 

            return bestResultOfAllTimes;
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

			long newCalc = m_Calculator.Calculate(GetInput(data), newPath);
            try
            {
                long finalCalc = m_Calculator.Calculate(GetInput(data), finalPath);
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

        public class ScoreChange
        {
            public long Improvment { get; set; }
            public long NewScore { get; set; }

            public ScoreChange(long newScore)
            {
                this.Improvment = newScore;
                this.NewScore = newScore;
            }

            public ScoreChange(long newScore, long improvment)
            {
                this.Improvment = improvment;
                this.NewScore = newScore;
            }
        }
    }
}
