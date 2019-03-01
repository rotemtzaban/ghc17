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
            System.Diagnostics.Stopwatch t = new System.Diagnostics.Stopwatch();
            t.Start();
            var parserBase = new Parser();

            Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>(
                "2019", parserBase, new StupidDolver(), new Printer(), new Calcutaor());
            runner1.Run(Properties.Resources.a_example, "a_example", 10, false);
            runner1.Run(Properties.Resources.b_lovely_landscapes, "b_lovely_landscapes", 1, false);
            runner1.Run(Properties.Resources.c_memorable_moments, "c_memorable_moments", 100, false);
            runner1.Run(Properties.Resources.d_pet_pictures, "d_pet_pictures", 1, false);
            runner1.Run(Properties.Resources.e_shiny_selfies, "e_shiny_selfies", 1, false);
            t.Stop();
            Console.WriteLine("Finished in {0}", t.Elapsed);
            ZipCreator.CreateCodeZip(string.Empty);

            Console.Read();
        }
    }
}
