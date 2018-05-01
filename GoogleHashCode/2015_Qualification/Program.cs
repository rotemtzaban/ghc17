using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodeCommon;

namespace _2015_Qualification
{
    class Program
    {
        static void Main(string[] args)
        {
			Runner<ProblemInput, ProblemOutput> runner = new Runner<ProblemInput, ProblemOutput> ("2015", new Parser (), new Solver (), new Printer (), new ScoreCalculator ());

			runner.Run(Properties.Resources.Input, "Example", 100, true);
			// runner.Run(Properties.Resources.TestInput, "Example", 1, true);

            ZipCreator.CreateCodeZip("Example");
				
            Console.Read();
        }
    }
}
