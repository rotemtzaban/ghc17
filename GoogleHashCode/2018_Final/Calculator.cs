using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using HashCodeCommon;
using HashCodeCommon.HelperClasses;
using System.Diagnostics;

namespace _2018_Final
{
    public class Calculator : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public enum CellState
        {
            Empty,
            Res,
            Util
        }

        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            // PrintToFile(input, output);

            Stopwatch watch = Stopwatch.StartNew();
            ValidateOutput(input, output);
            var utilityGrid = GetUtilityGrid(input, output);
            long score = 0;
            var affected = CalcAffectedCoordinates(input);

            foreach (var building in output.Buildings)
            {
                var buildingProject = input.BuildingProjects[building.ProjectNumber];
                if (buildingProject.BuildingType == BuildingType.Residential)
                {
                    var capacity = buildingProject.Capacity;
                    var utilities = GetUtilities(input, utilityGrid, building, buildingProject, affected);
                    score += capacity * utilities.Count;
                }
            }

            Console.WriteLine("Calcu time: " + watch.ElapsedMilliseconds);

            return score;
        }

        private static void PrintToFile(ProblemInput input, ProblemOutput output)
        {
            try
            {
                File.Delete(@"C:\temp\f.txt");
            }
            catch { }
            using (var writer = new StreamWriter(@"C:\temp\f.txt"))
            {
                CellState[,] state = new CellState[input.Rows, input.Columns];
                foreach (var item in output.Buildings)
                {
                    BuildingProject buildingProject1 = input.BuildingProjects[item.ProjectNumber];
                    for (int i = 0; i < buildingProject1.Plan.GetLength(0); i++)
                    {
                        for (int j = 0; j < buildingProject1.Plan.GetLength(1); j++)
                        {
                            if (!buildingProject1.Plan[i, j])
                                continue;

                            if (buildingProject1.BuildingType == BuildingType.Residential)
                                state[item.Coordinate.Row + i, item.Coordinate.Column + j] = CellState.Res;
                            else
                                state[item.Coordinate.Row + i, item.Coordinate.Column + j] = CellState.Util;
                        }
                    }
                }

                for (int i = 0; i < state.GetLength(0); i++)
                {
                    for (int j = 0; j < state.GetLength(1); j++)
                    {
                        if (state[i, j] == CellState.Empty)
                            writer.Write("E");
                        else if (state[i, j] == CellState.Res)
                            writer.Write("R");
                        else if (state[i, j] == CellState.Util)
                            writer.Write("U");
                        else
                            throw new Exception();
                    }

                    writer.WriteLine();
                }
            }
        }

        private Dictionary<BuildingProject, MatrixCoordinate[]> CalcAffectedCoordinates(ProblemInput input)
        {
            Dictionary<BuildingProject, MatrixCoordinate[]> map = new Dictionary<BuildingProject, MatrixCoordinate[]>();
            foreach (var currProject in input.BuildingProjects)
            {
                HashSet<MatrixCoordinate> coordinates = new HashSet<MatrixCoordinate>();
                for (int row = 0; row < currProject.Plan.GetLength(0); row++)
                {
                    for (int col = 0; col < currProject.Plan.GetLength(1); col++)
                    {
                        if (!currProject.Plan[row, col])
                            continue;

                        for (int i = -input.MaxDistance; i <= input.MaxDistance; i++)
                        {
                            for (int j = -input.MaxDistance + Math.Abs(i); j <= input.MaxDistance - Math.Abs(i); j++)
                            {
                                if (row + i >= 0 && col + j >= 0 &&
                                    row + i < currProject.Plan.GetLength(0) &&
                                    col + j < currProject.Plan.GetLength(1) &&
                                    currProject.Plan[row + i, col + j])
                                    continue;

                                coordinates.Add(new MatrixCoordinate(row + i, col + j));
                            }
                        }
                    }
                }

                map.Add(currProject, coordinates.ToArray());
            }

            return map;
        }

        private static HashSet<int> GetUtilities(ProblemInput input, int[,] utilityGrid, OutputBuilding building, BuildingProject buildingProject, Dictionary<BuildingProject, MatrixCoordinate[]> dic)
        {
            HashSet<int> utilities = new HashSet<int>();
            bool[,] plan = buildingProject.Plan;
            var coordinates = dic[buildingProject];

            foreach (var item in coordinates)
            {
                int gridRow = item.Row + building.Coordinate.Row;
                int gridCol = item.Column + building.Coordinate.Column;
                if (IsWithinGrid(utilityGrid, gridRow, gridCol) && utilityGrid[gridRow, gridCol] != 0)
                {
                    utilities.Add(utilityGrid[gridRow, gridCol]);
                }
            }

            return utilities;
        }

        private static bool IsWithinGrid(int[,] utilityGrid, int currGridRow, int currGridCol)
        {
            return currGridRow < utilityGrid.GetLength(0) && currGridRow >= 0 &&
                   currGridCol < utilityGrid.GetLength(1) && currGridCol >= 0;
        }

        private static int[,] GetUtilityGrid(ProblemInput input, ProblemOutput output)
        {
            int[,] utilityGrid = new int[input.Rows, input.Columns];
            foreach (var building in output.Buildings)
            {
                var buildingProject = input.BuildingProjects[building.ProjectNumber];
                if (buildingProject.BuildingType == BuildingType.Utility)
                {
                    bool[,] plan = buildingProject.Plan;
                    for (int row = 0; row < plan.GetLength(0); row++)
                    {
                        for (int column = 0; column < plan.GetLength(1); column++)
                        {
                            if (plan[row, column])
                            {
                                int gridRow = row + building.Coordinate.Row;
                                int gridCol = column + building.Coordinate.Column;
                                utilityGrid[gridRow, gridCol] = buildingProject.UtilityType;
                            }
                        }
                    }
                }
            }

            return utilityGrid;
        }

        private void ValidateOutput(ProblemInput input, ProblemOutput output)
        {
            bool[,] grid = new bool[input.Rows, input.Columns];
            foreach (var building in output.Buildings)
            {
                var buildingProject = input.BuildingProjects[building.ProjectNumber];
                if (buildingProject.BuildingType == BuildingType.Utility)
                {
                    bool[,] plan = buildingProject.Plan;
                    for (int row = 0; row < plan.GetLength(0); row++)
                    {
                        for (int column = 0; column < plan.GetLength(1); column++)
                        {
                            if (plan[row, column])
                            {
                                int gridRow = row + building.Coordinate.Row;
                                int gridCol = column + building.Coordinate.Column;
                                if (grid[gridRow, gridCol])
                                {
                                    throw new Exception($"cell is filled with two building. cell: {gridRow}, {gridCol}");
                                }

                                grid[gridRow, gridCol] = true;
                            }
                        }
                    }
                }
            }
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            ProblemOutput output = new ProblemOutput();
            var buildings = reader.GetInt();
            output.Buildings = new OutputBuilding[buildings].ToList();
            for (int i = 0; i < buildings; i++)
            {
                var intList = reader.GetIntList();
                output.Buildings[i] = new OutputBuilding
                {
                    ProjectNumber = intList[0],
                    Coordinate = new MatrixCoordinate(intList[1], intList[2])
                };
            }

            return output;
        }
    }
}