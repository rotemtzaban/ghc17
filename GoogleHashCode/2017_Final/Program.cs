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

            //DataAnalyze();

            ZipCreator.CreateCodeZip("2017_Final");
            Console.Read();
        }

        private static void DataAnalyze()
        {
            string[] data = new string[]
                            {
                    Properties.Resources.charleston_road,
                    Properties.Resources.lets_go_higher,
                    Properties.Resources.opera,
                    Properties.Resources.rue_de_londres,
                            };

            for (int i = 0; i < data.Length; i++)
            {
                ProblemInput prob = new Parser().ParseFromData(data[i]);
                int wallsCount = 0, EmptyCount = 0, TargetCount = 0;
                foreach (var item in prob.Cells)
                {
                    if (item == Cell.Empty)
                        EmptyCount++;
                    else if (item == Cell.Wall)
                        wallsCount++;
                    else TargetCount++;
                }

                Console.WriteLine($"{i}, total: {prob.Cells.Length}, walls: {wallsCount}, empty: {EmptyCount}, target: {TargetCount}");
            }
        }
    }
}
