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
            runner1.Run(Properties.Resources.a_example, "a_example", 1, true);

            Runner<ProblemInput, ProblemOutput> runner2 = new Runner<ProblemInput, ProblemOutput>(
                "2018", new Parser(), new Solver(), new Printer(), new Calcutaor());
            runner2.Run(Properties.Resources.b_should_be_easy, "b_should_be_easy", 1, true);

            Runner<ProblemInput, ProblemOutput> runner3 = new Runner<ProblemInput, ProblemOutput>(
                "2018", new Parser(), new Solver(), new Printer(), new Calcutaor());
            runner3.Run(Properties.Resources.c_no_hurry, "c_no_hurry", 1, true);

            Runner<ProblemInput, ProblemOutput> runner4 = new Runner<ProblemInput, ProblemOutput>(
                "2018", new Parser(), new Solver(), new Printer(), new Calcutaor());
            runner4.Run(Properties.Resources.d_metropolis, "d_metropolis", 1, true);

            Runner<ProblemInput, ProblemOutput> runner5 = new Runner<ProblemInput, ProblemOutput>(
                "2018", new Parser(), new Solver(), new Printer(), new Calcutaor());
            runner5.Run(Properties.Resources.e_high_bonus, "e_high_bonus", 1, true);

            ZipCreator.CreateCodeZip("e_high_bonus");

            Console.Read();
        }
    }
}
