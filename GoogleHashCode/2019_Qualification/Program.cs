using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_Qualification
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>(
                "2019", new Parser(), new Solver(), new Printer(), new Calcutaor());
            //runner1.Run(Properties.Resources.input_1, "input_1", 1, true);

            ZipCreator.CreateCodeZip(string.Empty);

            Console.Read();
        }
    }
}
