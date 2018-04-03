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
            runner1.Run(Properties.Resources.charleston_road, "charleston_road", 1, true);

            Runner<ProblemInput, ProblemOutput> runner2 = new Runner<ProblemInput, ProblemOutput>(
                "2017_Final", new Parser(), new Solver(), new Printer(), new Calcutaor());
            runner2.Run(Properties.Resources.lets_go_higher, "lets_go_higher", 1, true);

            Runner<ProblemInput, ProblemOutput> runner3 = new Runner<ProblemInput, ProblemOutput>(
                "2017_Final", new Parser(), new Solver(), new Printer(), new Calcutaor());
            runner3.Run(Properties.Resources.opera, "opera", 1, true);

            Runner<ProblemInput, ProblemOutput> runner4 = new Runner<ProblemInput, ProblemOutput>(
                "2017_Final", new Parser(), new Solver(), new Printer(), new Calcutaor());
            runner4.Run(Properties.Resources.rue_de_londres, "rue_de_londres", 1, true);


            runner1.CreateCodeZip();

            Console.Read();
        }
    }
}
