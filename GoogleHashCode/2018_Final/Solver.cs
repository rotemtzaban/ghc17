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
            output.Buildings = new List<OutputBuilding>();

            //var residtianls = input.BuildingProjects.Where(_ => _.BuildingType == BuildingType.Residential).ToList();
            //var utilities = input.BuildingProjects.Where(_ => _.BuildingType == BuildingType.Utility).ToList();
            //var orderResidntial = residtianls.OrderBy(OrderByResidintialMethod).ToList();
            //var orderutilities = utilities.OrderBy(OrderByUtilityMethod).ToList();

            CellType[,] filledCells = new CellType[input.Rows, input.Columns];
            for (int i = 0; i < filledCells.GetLength(0); i++)
            {
                for (int j = 0; j < filledCells.GetLength(1); j++)
                {
                    filledCells[i, j] = new CellType()
                    {
                        IsOccupied = false,
                        NearUtilities = new HashSet<int>(),
                        BuildingIndex = -1
                    };
                }
            }

            List<MatrixCoordinate> possiblePoints = new List<MatrixCoordinate>();
            possiblePoints.Add(new MatrixCoordinate(0, 0));
            while (true)
            {
                MatrixCoordinate first = possiblePoints[0];
                possiblePoints.RemoveAt(0);
                BuildingProject bestProject = GetBestFit(input.BuildingProjects, filledCells, first);
                if (bestProject == null)
                    break;

                first = AddBestProject(filledCells, first, bestProject);

                possiblePoints.Add(new MatrixCoordinate(first.Row + bestProject.Plan.GetLength(0), first.Column));
                possiblePoints.Add(new MatrixCoordinate(first.Row, first.Column + bestProject.Plan.GetLength(1)));
                possiblePoints.Add(new MatrixCoordinate(first.Row + bestProject.Plan.GetLength(0), first.Column + bestProject.Plan.GetLength(1)));
                output.Buildings.Add(new OutputBuilding() { Coordinate = first, ProjectNumber = bestProject.Index });
            }

            return output;
        }

        private MatrixCoordinate AddBestProject(CellType[,] filledCells, MatrixCoordinate first, BuildingProject bestProject)
        {
            for (int row = 0; row < bestProject.Plan.GetLength(0); row++)
            {
                for (int col = 0; col < bestProject.Plan.GetLength(1); col++)
                {
                    filledCells[row + first.Row, first.Column + col].IsOccupied = bestProject.Plan[row, col];
                    filledCells[row + first.Row, first.Column + col].BuildingType = bestProject.BuildingType;
                    filledCells[row + first.Row, first.Column + col].BuildingIndex = bestProject.Index;

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

                            if (cellToCheck.BuildingType == BuildingType.Utility)
                            {
                                filledCells[row + first.Row, first.Column + col].NearUtilities.Add(cellToCheck.UtilityIndex);
                            }
                            else
                            {
                                if (bestProject.BuildingType == BuildingType.Utility)
                                {
                                    cellToCheck.NearUtilities.Add(bestProject.UtilityType);
                                }
                            }
                        }
                    }
                }
            }

            return first;
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

            for (int row = 0; row < item.Plan.GetLength(0); row++)
            {
                for (int col = 0; col < item.Plan.GetLength(1); col++)
                {
                    int rowToCheck = inputCoordinate.Row + row;
                    int colToCheck = inputCoordinate.Column + col;

                    if (!InMatrix(rowToCheck, colToCheck))
                        return int.MinValue;

                    if (filledCells[rowToCheck, colToCheck].IsOccupied)
                        return int.MinValue;
                }
            }

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

            return nearUtilities.Count * item.Capacity;
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