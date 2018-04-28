using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_Final
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calcutaor());
            // runner1.Run(Properties.Resources.A, "A", 1, true);

            Runner<ProblemInput, ProblemOutput> runner2 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calcutaor());
            // runner2.Run(Properties.Resources.B, "B", 1, true);

            Runner<ProblemInput, ProblemOutput> runner3 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calcutaor());
            // runner3.Run(Properties.Resources.C, "C", 1, true);

            Runner<ProblemInput, ProblemOutput> runner4 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calcutaor());
            // runner4.Run(Properties.Resources.D, "D", 1, true);

            // DataAnalyze();

            runner1.CreateCodeZip();
            Console.Read();
        }

        private static void DataAnalyze()
        {
            string[] data = new string[]
            {
                    //Properties.Resources.charleston_road,
                    //Properties.Resources.lets_go_higher,
                    //Properties.Resources.opera,
                    //Properties.Resources.rue_de_londres,
            };

            for (int i = 0; i < data.Length; i++)
            {
                ProblemInput prob = new Parser().ParseFromData(data[i]);

                Console.WriteLine($"case {i}:");
                Console.WriteLine($"insert case data");
            }
        }
    }
}
