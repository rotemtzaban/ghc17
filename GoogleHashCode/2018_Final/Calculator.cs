using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using HashCodeCommon;
using HashCodeCommon.HelperClasses;

namespace _2018_Final
{
    public class Calculator : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            ValidateOutput(input, output);
            var utilityGrid = GetUtilityGrid(input, output);
            long score = 0;

            foreach (var building in output.Buildings)
            {
                var buildingProject = input.BuildingProjects[building.ProjectNumber];
                if (buildingProject.BuildingType == BuildingType.Residential)
                {
                    var capacity = buildingProject.Capacity;
                    var utilities = GetUtilities(input, utilityGrid, building, buildingProject);
                    score += capacity * utilities.Count;
                }
            }

            return score;
        }

        private static HashSet<int> GetUtilities(ProblemInput input, int[,] utilityGrid, OutputBuilding building, BuildingProject buildingProject)
        {
            HashSet<int> utilities = new HashSet<int>();
            bool[,] plan = buildingProject.Plan;
            for (int row = 0; row < plan.GetLength(0); row++)
            {
                for (int column = 0; column < plan.GetLength(1); column++)
                {
                    if (plan[row, column])
                    {
                        int gridRow = row + building.Coordinate.Row;
                        int gridCol = column + building.Coordinate.Column;
                        int maxDistance = input.MaxDistance;
                        for (int i = -maxDistance; i <= maxDistance; i++)
                        {
                            int maxColDistance = maxDistance - Math.Abs(i);
                            for (int j = -maxColDistance; j <= maxColDistance; j++)
                            {
                                var currGridRow = gridRow + i;
                                var currGridCol = gridCol + j;
                                if (IsWithinGrid(utilityGrid, currGridRow, currGridCol))
                                {
                                    if (utilityGrid[currGridRow, currGridCol] != 0)
                                    {
                                        utilities.Add(utilityGrid[currGridRow, currGridCol]);
                                    }
                                }
                            }
                        }
                    }
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