using System;
using System.Linq;
using HashCodeCommon;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace _2018_Final
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        private Dictionary<BuildingProject, MatrixCoordinate[]> m_AffectedCoordinates;
        private ProblemInput m_Input;

        protected override ProblemOutput Solve(ProblemInput input)
        {
            m_Input = input;
            ProblemOutput output = new ProblemOutput();
            output.Buildings = new List<OutputBuilding>();
            m_AffectedCoordinates = CalcAffectedCoordinates(input);
            SortBuildingPrjects();

            CellType[,] filledCells = InitMatrixCells(input);
            // TourmenetSolution(input, output, filledCells);

            TurkishAirportSolution(input, output, filledCells);

            FillEmptyCells(input, output, filledCells);

            // PrintToFile(filledCells);
            return output;
        }

        private void TourmenetSolution(ProblemInput input, ProblemOutput output, CellType[,] filledCells)
        {
            List<MatrixCoordinate> possiblePoints = new List<MatrixCoordinate>
            {
                new MatrixCoordinate(0, 0)
            };
            while (possiblePoints.Count != 0)
            {
                MatrixCoordinate first = possiblePoints[0];
                if (first.Row == 99)
                {
                    int n = 0;
                }
                possiblePoints.RemoveAt(0);
                BuildingProject bestProject = GetBestFit(input.BuildingProjects, filledCells, first);
                if (bestProject == null)
                    continue;

                if (output.Buildings.Count == 1700)
                {
                    int r = 0;
                }
                first = AddBestProject(filledCells, first, bestProject);

                possiblePoints.Add(new MatrixCoordinate(first.Row + bestProject.Plan.GetLength(0), first.Column));
                possiblePoints.Add(new MatrixCoordinate(first.Row, first.Column + bestProject.Plan.GetLength(1)));
                possiblePoints.Add(new MatrixCoordinate(first.Row + bestProject.Plan.GetLength(0), first.Column + bestProject.Plan.GetLength(1)));
                output.Buildings.Add(new OutputBuilding() { Coordinate = first, ProjectNumber = bestProject.Index });
            }
        }

        private static CellType[,] InitMatrixCells(ProblemInput input)
        {
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

            return filledCells;
        }

        private void SortBuildingPrjects()
        {
            Array.Sort(m_Input.BuildingProjects, (project1, project2) =>
            {
                if (project1.BuildingType != project2.BuildingType)
                {
                    return project1.BuildingType.CompareTo(project2.BuildingType);
                }

                if (project1.BuildingType == BuildingType.Residential)
                {
                    return GetResidntialHeuristic(project1).CompareTo(GetResidntialHeuristic(project2));
                }

                return GetUtilityHeuristic(project1).CompareTo(GetUtilityHeuristic(project2));
            });
        }

        private void PrintToFile(CellType[,] state)
        {
            using (var writer = new StreamWriter(@"C:\temp\c.txt"))
            {
                for (int i = 0; i < state.GetLength(0); i++)
                {
                    for (int j = 0; j < state.GetLength(1); j++)
                    {
                        if (!state[i, j].IsOccupied)
                            writer.Write("E" + "-1" + "E");
                        else if (state[i, j].BuildingType == BuildingType.Residential)
                            writer.Write("R" + state[i, j].BuildingIndex.ToString("00") + "R");
                        else if (state[i, j].BuildingType == BuildingType.Utility)
                            writer.Write("U" + state[i, j].BuildingIndex.ToString("00") + "U");
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
            foreach (var currProject in m_Input.BuildingProjects)
            {
                HashSet<MatrixCoordinate> coordinates = new HashSet<MatrixCoordinate>();
                for (int row = 0; row < currProject.Plan.GetLength(0); row++)
                {
                    for (int col = 0; col < currProject.Plan.GetLength(1); col++)
                    {
                        if (!currProject.Plan[row, col])
                            continue;

                        for (int i = -m_Input.MaxDistance; i <= m_Input.MaxDistance; i++)
                        {
                            for (int j = -m_Input.MaxDistance + Math.Abs(i); j <= m_Input.MaxDistance - Math.Abs(i); j++)
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

        private double GetResidntialHeuristic(BuildingProject project1)
        {
            // return project1.Capacity / (double)project1.Plan.Cast<bool>().Count(b => b);
            return project1.Capacity / (double)project1.Plan.Length;
        }

        private double GetUtilityHeuristic(BuildingProject project1)
        {
            // return 1.0 / project1.Plan.Cast<bool>().Count(b => b);
            return 1.0 / project1.Plan.Length;
        }

        private static void FillEmptyCells(ProblemInput input, ProblemOutput output, CellType[,] filledCells)
        {
            var Size1Building = input.BuildingProjects.FirstOrDefault(_ => _.Plan.Length == 1 && _.BuildingType == BuildingType.Residential);
            if (Size1Building != null)
            {
                for (int i = 0; i < filledCells.GetLength(0); i++)
                {
                    for (int j = 0; j < filledCells.GetLength(1); j++)
                    {
                        if (filledCells[i, j].IsOccupied)
                            continue;

                        if (output.Buildings.Count == 0)
                        {
                            int r = 0;
                        }
                        output.Buildings.Add(new OutputBuilding()
                        {
                            Coordinate = new MatrixCoordinate(i, j),
                            ProjectNumber = Size1Building.Index
                        });
                    }
                }
            }
        }

        private void TurkishAirportSolution(ProblemInput input, ProblemOutput output, CellType[,] filledCells)
        {
            var Size1Building = input.BuildingProjects.FirstOrDefault(_ => _.Plan.Length == 1 && _.BuildingType == BuildingType.Residential);
            for (int i = 0; i < filledCells.GetLength(0); i++)
            {
                for (int j = 0; j < filledCells.GetLength(1); j++)
                {
                    if (filledCells[i, j].IsOccupied)
                        continue;

                    BuildingProject bestProject = GetBestFit(input.BuildingProjects, filledCells, new MatrixCoordinate(i, j));

                    if (bestProject == null)
                        continue;

                    AddBestProject(filledCells, new MatrixCoordinate(i, j), bestProject);

                    output.Buildings.Add(new OutputBuilding()
                    {
                        Coordinate = new MatrixCoordinate(i, j),
                        ProjectNumber = bestProject.Index
                    });
                }
            }
        }
            
        int resId = 1000000;
        private MatrixCoordinate AddBestProject(CellType[,] filledCells, MatrixCoordinate first, BuildingProject bestProject)
        {
            resId++;
            for (int row = 0; row < bestProject.Plan.GetLength(0); row++)
            {
                for (int col = 0; col < bestProject.Plan.GetLength(1); col++)
                {
                    if (filledCells[row + first.Row, first.Column + col].IsOccupied)
                    {
                        int n = 0;
                    }
                    filledCells[row + first.Row, first.Column + col].IsOccupied = bestProject.Plan[row, col];
                    filledCells[row + first.Row, first.Column + col].BuildingType = bestProject.BuildingType;

                    filledCells[row + first.Row, first.Column + col].BuildingIndex = bestProject.Index;
                    filledCells[row + first.Row, first.Column + col].BuildingUniqueIndex = resId;

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
            double bestResidntialScore = int.MinValue;
            object lockObject = new object();
            // Parallel.ForEach(orderResidntial, new ParallelOptions() { MaxDegreeOfParallelism = 5 }, item =>
            foreach (var item in orderResidntial)
            {
                double currScore = GetScore(item, filledCells, inputCoordinate);
                //lock (lockObject)
                 {
                    if (bestResidntialScore < currScore)
                    {
                        bestResdintial = item;
                        bestResidntialScore = currScore;
                    }
                }
             }
           //});

            return bestResdintial;
        }

        private double GetScore(BuildingProject item, CellType[,] filledCells, MatrixCoordinate inputCoordinate)
        {
            if (inputCoordinate.Row + item.Plan.GetLength(0) > filledCells.GetLength(0) ||
                inputCoordinate.Column + item.Plan.GetLength(1) > filledCells.GetLength(1))
                return int.MinValue;

            if (!InMatrix(inputCoordinate.Row + item.Plan.GetLength(0) - 1, 
                        inputCoordinate.Column + item.Plan.GetLength(1) - 1))
                return int.MinValue;

            for (int row = 0; row < item.Plan.GetLength(0); row++)
            {
                for (int col = 0; col < item.Plan.GetLength(1); col++)
                {
                    int rowToCheck = inputCoordinate.Row + row;
                    int colToCheck = inputCoordinate.Column + col;

                    if (filledCells[rowToCheck, colToCheck].IsOccupied)
                        return int.MinValue;
                }
            }

            if (item.BuildingType == BuildingType.Residential)
                return 1.0 * GetResidntialScore(item, filledCells, inputCoordinate);
            return 1.0 * GetUtilityScore(item, filledCells, inputCoordinate);
        }

        private int GetUtilityScore(BuildingProject item, CellType[,] filledCells, MatrixCoordinate inputCoordinate)
        {
            HashSet<int> nearResidntials = new HashSet<int>();
            List<int> nearResidntials2 = new List<int>();
            foreach (var currCoordinate in m_AffectedCoordinates[item])
            {
                if (!InMatrix(currCoordinate.Row + inputCoordinate.Row, currCoordinate.Column + inputCoordinate.Column))
                    continue;

                var cellToCheck = filledCells[currCoordinate.Row + inputCoordinate.Row, currCoordinate.Column + inputCoordinate.Column];
                if (!cellToCheck.IsOccupied)
                    continue;

                if (cellToCheck.BuildingType == BuildingType.Utility)
                    continue;

                if (cellToCheck.NearUtilities.Contains(item.UtilityType))
                    continue;
                if (nearResidntials.Add(cellToCheck.BuildingUniqueIndex))
                    nearResidntials2.Add(cellToCheck.BuildingIndex);
            }

            if (nearResidntials2.Any())
                return nearResidntials2.Sum(_ => m_Input.BuildingProjects[_].Capacity);
            return 1;
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
            // return (int)(100.0 * item.Capacity / item.Plan.GetLength(0) / item.Plan.GetLength(1));

            HashSet<int> nearUtilities = new HashSet<int>();
            foreach (var currCoordinate in m_AffectedCoordinates[item])
            {
                if (!InMatrix(currCoordinate.Row + inputCoordinate.Row, currCoordinate.Column + inputCoordinate.Column))
                    continue;

                var cellToCheck = filledCells[currCoordinate.Row + inputCoordinate.Row, currCoordinate.Column + inputCoordinate.Column];
                if (!cellToCheck.IsOccupied)
                    continue;

                if (cellToCheck.BuildingType == BuildingType.Residential)
                    continue;

                nearUtilities.Add(cellToCheck.UtilityIndex);
            }

            if (nearUtilities.Any())
                return nearUtilities.Count * item.Capacity;
            return 1;
        }
    }
}