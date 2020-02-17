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
            Calculator calculator = new Calculator();

            var runner1 = new Runner<ProblemInput, ProblemOutput>("2020_SecondPractice", new Parser(), new Solver(), new Printer(), calculator);
            // runner1.Run(Resources.Example, "example");
            List<double> runParams = new List<double>();
            for (int i = 0; i < 100; i++)
            {
                runParams.Add(0 + i / 100.0);
            }

            var runner2 = new Runner<ProblemInput, ProblemOutput>("2020_SecondPractice", new Parser(), new Solver(), new Printer(), calculator);
            runner2.Run(Resources.dc_in, "dc_in", 100, true, runParams);

            Console.ReadLine();
        }
    }
}
