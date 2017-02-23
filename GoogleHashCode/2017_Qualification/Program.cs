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
            Runner<ProblemInput, ProblemOutput> runner = new Runner<ProblemInput, ProblemOutput>(new Parser(), new Solver(), new Printer(), new ScoreCalculator());

			//runner.Run(Properties.Resources.ExampleInput, "ExampleInput", 1, true);
			runner.Run(Properties.Resources.Kittens, "Kittens", 1, true);
			//runner.Run(Properties.Resources.MeAtTheZoo, "MeAtTheZoo", 1, true);
			//runner.Run(Properties.Resources.TrendingToday, "TrendingToday", 1, true);
			//runner.Run(Properties.Resources.VideosWorthSpreading, "VideosWorthSpreading", 1, true);

            runner.CreateCodeZip();

            Console.Read();
        }
    }
}
