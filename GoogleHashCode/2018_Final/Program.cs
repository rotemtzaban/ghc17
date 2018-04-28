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
            DataAnalyze();

            Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calculator());
            runner1.Run(Properties.Resources.a_example , "a_example", 1, true);

            Runner<ProblemInput, ProblemOutput> runner2 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calculator());
            runner2.Run(Properties.Resources.b_short_walk, "b_short_walk", 1, true);

            Runner<ProblemInput, ProblemOutput> runner3 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calculator());
            runner3.Run(Properties.Resources.c_going_green, "c_going_green", 1, true);

            Runner<ProblemInput, ProblemOutput> runner4 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calculator());
            runner4.Run(Properties.Resources.d_wide_selection, "d_wide_selection", 1, true);

            Runner<ProblemInput, ProblemOutput> runner5 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calculator());
            runner5.Run(Properties.Resources.e_precise_fit, "e_precise_fit", 1, true);

            Runner<ProblemInput, ProblemOutput> runner6 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new Solver(), new Printer(), new Calculator());
            runner6.Run(Properties.Resources.f_different_footprints, "f_different_footprints", 1, true);

            runner1.CreateCodeZip();
            Console.Read();
        }

        private static void DataAnalyze()
        {
            string[] data = new string[]
            {
                    Properties.Resources.a_example,
                    Properties.Resources.b_short_walk,
                    Properties.Resources.c_going_green,
                    Properties.Resources.d_wide_selection,
                    Properties.Resources.f_different_footprints,
                    Properties.Resources.e_precise_fit,
            };

            for (int i = 0; i < data.Length; i++)
            {
                ProblemInput prob = new Parser().ParseFromData(data[i]);

                var residtianls = prob.BuildingProjects.Where(_ => _.BuildingType == BuildingType.Residential).ToList();
                var utility = prob.BuildingProjects.Where(_ => _.BuildingType == BuildingType.Utility).ToList();
                bool anyTwiceUtil = false;
                for (int j = 0; j < utility.Count; j++)
                {
                    for (int n = 0; n < utility.Count; n++)
                    {
                        if (j != n && utility[j].UtilityType == utility[n].UtilityType)
                        {
                            anyTwiceUtil = true;
                            break;
                        }
                    }
                }

                Console.WriteLine($"case {i}:");
                Console.WriteLine($"insert case data Rows: {prob.Rows}, Columns:{prob.Columns}, MaxDistance: {prob.MaxDistance}");
                Console.WriteLine($"Are two util same type : { anyTwiceUtil}");
            }
        }
    }
}
