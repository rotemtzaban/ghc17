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
			Runner<ProblemInput, ProblemOutput> runner = new Runner<ProblemInput, ProblemOutput> (new Parser (), new Solver (), new Printer (), new ScoreCalculator ());

            runner.Run(Properties.Resources.Input, "Example", 1, true);
            //runner.Run(Properties.Resources.busy_day, "busy_day", 1, false);
            //runner.Run(Properties.Resources.mother_of_all_warehouses, "mother_of_all_warehouses", 1, false);
            //runner.Run(Properties.Resources.redundancy, "redundancy", 1, false);	

            runner.CreateCodeZip();
				
            Console.Read();
        }
    }
}
