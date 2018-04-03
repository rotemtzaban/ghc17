using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Final
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>(
                "2017_Final", new Parser(), new Solver(), new Printer(), new Calcutaor());
            // .Run(Properties.Resources.a_example, "a_example", 1, true);

            runner1.CreateCodeZip();

            Console.Read();
        }
    }
}
