using _2018_Final.Properties;
using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace _2018_Final
{
    class Program
    {
        static void Main(string[] args)
        {
            // DataAnalyze();
            Stopwatch watch = Stopwatch.StartNew();

            ZipCreator.CreateCodeZip("2018_Final");

            // RunAll(new Calculator());

            //Runner<ProblemInput, ProblemOutput> runner6 = new Runner<ProblemInput, ProblemOutput>(
            //    "2018_Final", new Parser(), new EScroer(), new Printer(), new Calculator());
            //runner6.Run(Properties.Resources.e_precise_fit, "e_precise_fit", 1, true);
            var runner = new Runner<ProblemInput, ProblemOutput>("2018_Final", new Parser(), new EfficintSolver(), new Printer(), new Calculator());

            runner.Run(Resources.c_going_green, "c_going_green", 1, true);
            Console.WriteLine("Time:" + watch.ElapsedMilliseconds);
            Console.Read();
        }

        private static void RunAll(Calculator calc)
        {
            Task t = Task.Run(() =>
            {
                Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>(
                              "2018_Final", new Parser(), new EfficintSolver(), new Printer(), calc);
                runner1.Run(Properties.Resources.a_example, "a_example", 1, true);
            });

            Task t2 = Task.Run(() =>
            {
                Runner<ProblemInput, ProblemOutput> runner2 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new EfficintSolver(), new Printer(), calc);
                runner2.Run(Properties.Resources.b_short_walk, "b_short_walk", 1, true);
            });

            Task t3 = Task.Run(() =>
            {
                Runner<ProblemInput, ProblemOutput> runner3 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new EfficintSolver(), new Printer(), calc);
                runner3.Run(Properties.Resources.c_going_green, "c_going_green", 1, true);
            });

            Task t4 = Task.Run(() =>
            {
                Runner<ProblemInput, ProblemOutput> runner4 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new EfficintSolver(), new Printer(), calc);
                runner4.Run(Properties.Resources.d_wide_selection, "d_wide_selection", 1, true);
            });

            //Task t5 = Task.Run(() =>
            //{
            //    Runner<ProblemInput, ProblemOutput> runner5 = new Runner<ProblemInput, ProblemOutput>(
            //    "2018_Final", new Parser(), new EScroer(), new Printer(), calc);
            //    runner5.Run(Properties.Resources.e_precise_fit, "e_precise_fit", 1, true);
            //});

            Task t6 = Task.Run(() =>
            {
                Runner<ProblemInput, ProblemOutput> runner6 = new Runner<ProblemInput, ProblemOutput>(
                "2018_Final", new Parser(), new EfficintSolver(), new Printer(), calc);
                runner6.Run(Properties.Resources.f_different_footprints, "f_different_footprints", 1, true);
            });

            Task.WaitAll(t, t2, t3, t4, t6);
        }

        private static void DataAnalyze()
        {
            string[] data = new string[]
            {
                    Properties.Resources.a_example,
                    Properties.Resources.b_short_walk,
                    Properties.Resources.c_going_green,
                    Properties.Resources.d_wide_selection,
                    Properties.Resources.e_precise_fit,
                    Properties.Resources.f_different_footprints,
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

                bool canPutInside = CanPutInside(prob);

                var bestRes = residtianls.Max(_ => 1.0 * _.Capacity / _.Plan.GetLength(0) / _.Plan.GetLength(1));
                BuildingProject bestBuilding = null;
                foreach (var item in residtianls)
                {
                    var _ = item;
                    if (1.0 * _.Capacity / _.Plan.GetLength(0) / _.Plan.GetLength(1) == bestRes)
                        bestBuilding = item;
                }

                Console.WriteLine($"case {i}:");
                Console.WriteLine($"insert case data Rows: {prob.Rows}, Columns:{prob.Columns}, MaxDistance: {prob.MaxDistance}");
                Console.WriteLine($"Are two util same type : { anyTwiceUtil}");
                Console.WriteLine($"Can put inside: {canPutInside}");
                Console.WriteLine($"Best res: {bestRes}, size: {bestBuilding.Plan.GetLength(0)}, {bestBuilding.Plan.GetLength(1)}");

                Console.WriteLine($"different utilities :{utility.Select(project => project.UtilityType).Distinct().Count()}");
                Console.WriteLine($"Num res :{residtianls.Count}");
                bool anySize1Res = prob.BuildingProjects.Any(_ => _.Plan.Length == 1 && _.BuildingType == BuildingType.Residential);
                Console.WriteLine("Size 1 res: " + anySize1Res);
                Console.WriteLine($"Min res: " + prob.BuildingProjects.Min(_ => _.Plan.Length));
                Console.WriteLine($"Any spcial: " + prob.BuildingProjects.Any(_ => _.Plan.Length != 1 && _.Plan.Cast<bool>().Count(__ => __) == 1));
                // Console.WriteLine($"Num res :{residtianls.Count}");
            }
        }

        private static bool CanPutInside(ProblemInput prob)
        {
            bool canPutInside = false;
            foreach (var currProject in prob.BuildingProjects)
            {
                foreach (var insideProjects in prob.BuildingProjects)
                {
                    for (int row = 0; row < currProject.Plan.GetLength(0); row++)
                    {
                        for (int col = 0; col < currProject.Plan.GetLength(1); col++)
                        {
                            bool isSuspect = true;
                            for (int i = 0; i <= insideProjects.Plan.GetLength(0) && isSuspect; i++)
                            {
                                for (int j = 0; j <= insideProjects.Plan.GetLength(1) && isSuspect; j++)
                                {
                                    int rowToCheck = row + i;
                                    int colToCheck = col + i;

                                    if (!InMatrix(rowToCheck, colToCheck, currProject.Plan) ||
                                        currProject.Plan[rowToCheck, colToCheck])
                                    {
                                        isSuspect = false;
                                        break;
                                    }
                                }
                            }

                            if (isSuspect)
                                canPutInside = true;
                        }
                    }
                }
            }

            return canPutInside;
        }

        private static bool InMatrix(int rowToCheck, int colToCheck, bool[,] matrix)
        {
            return rowToCheck >= 0 &&
                colToCheck >= 0 &&
                rowToCheck < matrix.GetLength(0) &&
                colToCheck < matrix.GetLength(1);
        }
    }
}
