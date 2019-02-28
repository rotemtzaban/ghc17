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
            var parserBase = new Parser();
            var problemInput = parserBase.ParseFromData(Properties.Resources.a_example);
            var fromData = parserBase.ParseFromData(Properties.Resources.b_lovely_landscapes);
            var input = parserBase.ParseFromData(Properties.Resources.c_memorable_moments);
            var data = parserBase.ParseFromData(Properties.Resources.d_pet_pictures);
            var problemInput1 = parserBase.ParseFromData(Properties.Resources.e_shiny_selfies);
            Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>(
                "2019", parserBase, new Solver(), new Printer(), new Calcutaor());
            runner1.Run(Properties.Resources.a_example, "a_example", 1, false);
            runner1.Run(Properties.Resources.b_lovely_landscapes, "b_lovely_landscapes", 1, false);
            runner1.Run(Properties.Resources.c_memorable_moments, "c_memorable_moments", 1, false);
            runner1.Run(Properties.Resources.d_pet_pictures, "d_pet_pictures", 1, false);
            runner1.Run(Properties.Resources.e_shiny_selfies, "e_shiny_selfies", 1, false);

            ZipCreator.CreateCodeZip(string.Empty);

            Console.Read();
        }
    }
}
