using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2016_Qualification.Properties;

namespace _2016_Qualification
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = null;

            var runner1 = new Runner<ProblemInput, ProblemOutput>("2016_Qualification", new Parser(), new Solver(), new Printer(), calculator);
            runner1.Run(Resources.busy_day, "");

            var runner2 = new Runner<ProblemInput, ProblemOutput>("2016_Qualification", new Parser(), new Solver(), new Printer(), calculator);
            runner2.Run(Resources.mother_of_all_warehouses, "");

            var runner3 = new Runner<ProblemInput, ProblemOutput>("2016_Qualification", new Parser(), new Solver(), new Printer(), calculator);
            runner3.Run(Resources.redundancy, "");

        }
    }
}
