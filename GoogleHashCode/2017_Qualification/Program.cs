using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
    class Program
    {
        static void Main(string[] args)
        {
			//runner0.Run(Properties.Resources.ExampleInput, "ExampleInput", 1, true);

			Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>(new Parser(), new Solver(), new Printer(), new ScoreCalculator());
			runner1.Run(Properties.Resources.MeAtTheZoo, "MeAtTheZoo", 1, true);

			Runner<ProblemInput, ProblemOutput> runner2 = new Runner<ProblemInput, ProblemOutput>(new Parser(), new Solver(), new Printer(), new ScoreCalculator());
			runner2.Run(Properties.Resources.TrendingToday, "TrendingToday", 1, true);

			Runner<ProblemInput, ProblemOutput> runner3 = new Runner<ProblemInput, ProblemOutput>(new Parser(), new Solver(), new Printer(), new ScoreCalculator());
			runner3.Run(Properties.Resources.VideosWorthSpreading, "VideosWorthSpreading", 1, true);

			Runner<ProblemInput, ProblemOutput> runner4 = new Runner<ProblemInput, ProblemOutput>(new Parser(), new Solver(), new Printer(), new ScoreCalculator());
			runner4.Run(Properties.Resources.Kittens, "Kittens", 1, true);

            runner1.CreateCodeZip();

            Console.Read();
        }
    }
}
