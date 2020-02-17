using _2020_SecondPractice.Properties;
using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_SecondPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = null;

            var runner1 = new Runner<ProblemInput, ProblemOutput>("2020_SecondPractice", new Parser(), new Solver(), new Printer(), calculator);
            runner1.Run(Resources.Example, "example");

            var runner2 = new Runner<ProblemInput, ProblemOutput>("2020_SecondPractice", new Parser(), new Solver(), new Printer(), calculator);
            runner2.Run(Resources.dc_in, "dc_in");

            Console.ReadLine();
        }
    }
}
