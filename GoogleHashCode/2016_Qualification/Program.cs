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
            var runner1 = new Runner<ProblemInput, ProblemOutput>("2016_Qualification", new Parser(), new Solver(), new Printer(), new Calculator());
            runner1.Run(Resources.busy_day, "");

            var runner2 = new Runner<ProblemInput, ProblemOutput>("2016_Qualification", new Parser(), new Solver(), new Printer(), new Calculator());
            runner2.Run(Resources.busy_day, "");

            var runner3 = new Runner<ProblemInput, ProblemOutput>("2016_Qualification", new Parser(), new Solver(), new Printer(), new Calculator());
            runner3.Run(Resources.busy_day, "");

        }
    }
}
