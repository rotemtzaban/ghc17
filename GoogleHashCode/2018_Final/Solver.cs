using System;
using System.Linq;
using HashCodeCommon;
using System.Collections.Generic;

namespace _2018_Final
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        private ProblemInput m_Input;

        protected override ProblemOutput Solve(ProblemInput input)
        {
            m_Input = input;
            ProblemOutput output = new ProblemOutput();

            //var residtianls = input.BuildingProjects.Where(_ => _.BuildingType == BuildingType.Residential).ToList();
            //var utilities = input.BuildingProjects.Where(_ => _.BuildingType == BuildingType.Utility).ToList();
            //var orderResidntial = residtianls.OrderBy(OrderByResidintialMethod).ToList();
            //var orderutilities = utilities.OrderBy(OrderByUtilityMethod).ToList();

            CellType[,] filledCells = new CellType[input.Rows, input.Columns];

            List<MatrixCoordinate> possiblePoints = new List<MatrixCoordinate>();
            possiblePoints.Add(new MatrixCoordinate(0, 0));
            while (true)
            {
                MatrixCoordinate first = possiblePoints[0];
                possiblePoints.RemoveAt(0);
                BuildingProject bestProject = GetBestFit(input.BuildingProjects, filledCells, first);
                if (bestProject == null)
                    break;

                // Add the best fit

                output.Buildings.Add(new OutputBuilding() { Coordinate = first, ProjectNumber = bestProject.Index });
            }

            return output;
        }

        private BuildingProject GetBestFit(IEnumerable<BuildingProject> orderResidntial, CellType[,] filledCells, MatrixCoordinate inputCoordinate)
        {
            BuildingProject bestResdintial = null;
            int bestResidntialScore = int.MinValue;
            foreach (var item in orderResidntial)
            {
                int currScore = GetScore(item, filledCells, inputCoordinate);
                if (bestResidntialScore < currScore)
                {
                    bestResdintial = item;
                    bestResidntialScore = currScore;
                }
            }

            return bestResdintial;
        }

        private int GetScore(BuildingProject item, CellType[,] filledCells, MatrixCoordinate inputCoordinate)
        {
            if (inputCoordinate.Row + item.Plan.GetLength(0) > filledCells.GetLength(0) ||
                inputCoordinate.Column + item.Plan.GetLength(1) > filledCells.GetLength(1))
                return int.MinValue;

            if (item.BuildingType == BuildingType.Residential)
                return GetResidntialScore(item, filledCells, inputCoordinate);
            return GetUtilityScore(item, filledCells, inputCoordinate);
        }

        private int GetUtilityScore(BuildingProject item, CellType[,] filledCells, MatrixCoordinate inputCoordinate)
        {
            HashSet<int> nearResidntials = new HashSet<int>();
            for (int row = 0; row < item.Plan.GetLength(0); row++)
            {
                for (int col = 0; col < item.Plan.GetLength(1); col++)
                {
                    if (!item.Plan[row, col])
                    {
                        continue;
                    }
                    for (int i = - m_Input.MaxDistance; i <= m_Input.MaxDistance; i++)
                    {
                        for (int j = - m_Input.MaxDistance + Math.Abs(i); j <= m_Input.MaxDistance - Math.Abs(i); j++)
                        {
                            int rowToCheck = row + i;
                            int colToCheck = col + i;

                            if (!InMatrix(rowToCheck, colToCheck))
                                continue;

                            var cellToCheck = filledCells[rowToCheck, colToCheck];
                            if (!cellToCheck.IsOccupied)
                                continue;

                            if (cellToCheck.BuildingType == BuildingType.Utility)
                                continue;

                            if (cellToCheck.NearUtilities.Contains(item.UtilityType))
                                continue;

                            nearResidntials.Add(cellToCheck.BuildingIndex);
                        }
                    }
                }
            }

            return nearResidntials.Sum(_ => m_Input.BuildingProjects[_].Capacity);
        }

        private bool InMatrix(int rowToCheck, int colToCheck)
        {
            return rowToCheck >= 0 &&
                colToCheck >= 0 &&
                rowToCheck < m_Input.Rows &&
                colToCheck < m_Input.Columns;
        }

        private int GetResidntialScore(BuildingProject item, CellType[,] filledCells, MatrixCoordinate inputCoordinate)
        {
            HashSet<int> nearUtilities = new HashSet<int>();
            for (int row = 0; row < item.Plan.GetLength(0); row++)
            {
                for (int col = 0; col < item.Plan.GetLength(1); col++)
                {
                    if (!item.Plan[row, col])
                    {
                        continue;
                    }
                    for (int i = -m_Input.MaxDistance; i <= m_Input.MaxDistance; i++)
                    {
                        for (int j = -m_Input.MaxDistance + Math.Abs(i); j <= m_Input.MaxDistance - Math.Abs(i); j++)
                        {
                            int rowToCheck = row + i;
                            int colToCheck = col + i;

                            if (!InMatrix(rowToCheck, colToCheck))
                                continue;

                            var cellToCheck = filledCells[rowToCheck, colToCheck];
                            if (!cellToCheck.IsOccupied)
                                continue;

                            if (cellToCheck.BuildingType == BuildingType.Residential)
                                continue;

                            nearUtilities.Add(cellToCheck.UtilityIndex);
                        }
                    }
                }
            }

            return nearUtilities.Sum(_ => m_Input.BuildingProjects[_].Capacity);
        }

        private object OrderByUtilityMethod(BuildingProject arg)
        {
            throw new NotImplementedException();
        }

        private object OrderByResidintialMethod(BuildingProject arg)
        {
            throw new NotImplementedException();
        }
    }
}