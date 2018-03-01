using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_Qualification
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>(
                "2018", new Parser(), new Solver(), new Printer(), new Calcutaor());

            // runner1.Run(Properties.Resources.MeAtTheZoo, "MeAtTheZoo", 1, true);

            runner1.CreateCodeZip();

            Console.Read();
        }
    }
}
