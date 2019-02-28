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
            runner1.Run(Properties.Resources.a_example, "a_example", 1, true);
            runner1.Run(Properties.Resources.b_lovely_landscapes, "b_lovely_landscapes", 1, true);
            runner1.Run(Properties.Resources.c_memorable_moments, "c_memorable_moments", 1, true);
            runner1.Run(Properties.Resources.d_pet_pictures, "d_pet_pictures", 1, true);
            runner1.Run(Properties.Resources.e_shiny_selfies, "e_shiny_selfies", 1, true);

            ZipCreator.CreateCodeZip(string.Empty);

            Console.Read();
        }
    }
}
