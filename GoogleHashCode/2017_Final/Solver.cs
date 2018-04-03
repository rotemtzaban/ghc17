using System;
using System.Collections.Generic;
using System.Linq;
using HashCodeCommon;

namespace _2017_Final
{
    internal class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            Dictionary<MatrixCoordinate, ScoredCoordinate> scoreDictionary = new Dictionary<MatrixCoordinate, ScoredCoordinate>();
            SortedSet<ScoredCoordinate> set = new SortedSet<ScoredCoordinate>(new Shit());
            Cell[,] cells = input.Cells;
            int[,] cellScores = new int[cells.GetLength(0), cells.GetLength(1)];
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    if (cells[i, j] == Cell.Traget)
                    {
                        InitScoreMatrix(input, cells, cellScores, i, j);
                    }
                }
            }

            for (int i = 0; i < cellScores.GetLength(0); i++)
            {
                for (int j = 0; j < cellScores.GetLength(1); j++)
                {
                    var coordinate = new MatrixCoordinate(i, j);
                    var cellScore = cellScores[i, j];
                    if (cellScore != 0)
                    {
                        var item = new ScoredCoordinate { Coordinate = coordinate, Score = cellScore };
                        scoreDictionary.Add(coordinate, item);
                        set.Add(item);
                    }
                }
            }

            int budget = input.StartingBudger;
            List<MatrixCoordinate> coordinates = CalculateRouterCoordinates(input, scoreDictionary, set, cells);

            int cost = 0;
            List<MatrixCoordinate> routerCoordinates = new List<MatrixCoordinate>();
            HashSet<MatrixCoordinate> backBoneCoordinates = new HashSet<MatrixCoordinate>();
            foreach (var coordinate in coordinates)
            {
                MatrixCoordinate startingBackbonePosition = input.StartingBackbonePosition;
                cost += input.RouterPrice;

                MatrixCoordinate backboneCoordinate = coordinate;
                List<Coordinate> coords = new List<Coordinate>();
                while (!backboneCoordinate.Equals(startingBackbonePosition) && budget > cost)
                {
                    if (backBoneCoordinates.Add(backboneCoordinate))
                    {
                        cost += input.BackBonePrice;
                    }
                    backboneCoordinate = new MatrixCoordinate(backboneCoordinate.Row - Math.Sign(backboneCoordinate.Row - startingBackbonePosition.Row),
                    backboneCoordinate.Column - Math.Sign(backboneCoordinate.Column - startingBackbonePosition.Column));
                }

                if (cost > budget)
                {
                    break;
                }
                routerCoordinates.Add(coordinate);
            }

            backBoneCoordinates.Add(input.StartingBackbonePosition);
            return new ProblemOutput
            {
                BackBoneCoordinates = backBoneCoordinates.ToArray(),
                RouterCoordinates = routerCoordinates.ToArray()
            };
        }

        private static List<MatrixCoordinate> CalculateRouterCoordinates(ProblemInput input, Dictionary<MatrixCoordinate, ScoredCoordinate> scoreDictionary, SortedSet<ScoredCoordinate> set, Cell[,] cells)
        {
            int maxRouters = (int)Math.Ceiling((double)input.StartingBudger / input.RouterPrice);
            List<MatrixCoordinate> coordinates = new List<MatrixCoordinate>();
            for (int i = 0; i < maxRouters; i++)
            {
                var max = set.Max;
                if (max.Score != 0)
                {
                    MatrixCoordinate coordinate = max.Coordinate;
                    coordinates.Add(coordinate);
                    var routerRadius = input.RouterRadius;
                    var notPossible = GetNotPossibleCells(input, cells, coordinate.Row, coordinate.Column, routerRadius);
                    for (int k = 0; k < notPossible.GetLength(0); k++)
                    {
                        for (int l = 0; l < notPossible.GetLength(1); l++)
                        {
                            if (!notPossible[k, l])
                            {
                                var x = coordinate.Row + k - routerRadius;
                                var y = coordinate.Column + l - routerRadius;
                                if (x >= 0 && x < cells.GetLength(0) && y >= 0 && y < cells.GetLength(1))
                                {
                                    var key = new MatrixCoordinate(x, y);
                                    if (scoreDictionary.TryGetValue(key, out var value))
                                    {
                                        set.Remove(value);
                                        value.Score--;
                                        if (value.Score > 0)
                                        {
                                            set.Add(value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            return coordinates;
        }

        public static void InitScoreMatrix(ProblemInput input, Cell[,] cells, int[,] cellScores, int i, int j)
        {
            int routerRadius = input.RouterRadius;
            bool[,] notPosible = GetNotPossibleCells(input, cells, i, j, routerRadius);

            for (int k = 0; k < notPosible.GetLength(0); k++)
            {
                for (int l = 0; l < notPosible.GetLength(1); l++)
                {
                    if (!notPosible[k, l])
                    {
                        var x = i + k - routerRadius;
                        var y = j + l - routerRadius;
                        if (x >= 0 && x < cells.GetLength(0) && y >= 0 && y < cells.GetLength(1))
                        {
                            cellScores[x, y]++;
                        }
                    }
                }
            }

        }

        private static bool[,] GetNotPossibleCells(ProblemInput input, Cell[,] cells, int i, int j, int routerRadius)
        {
            bool[,] notPosible = new bool[input.RouterRadius * 2 + 1, input.RouterRadius * 2 + 1];

            for (int k = Math.Max(0, i - routerRadius); k <= Math.Min(cells.GetLength(0) - 1, i + routerRadius); k++)
            {
                for (int l = Math.Max(0, j - routerRadius);
                    l <= Math.Min(cells.GetLength(1) - 1, j + routerRadius);
                    l++)
                {
                    int matX = k - i + routerRadius, matY = l - j + routerRadius;
                    if (notPosible[matX, matY])
                    {
                        continue;
                    }

                    if (cells[k, l] == Cell.Wall)
                    {
                        if (matX >= routerRadius)
                        {
                            for (int m = matX; m < routerRadius * 2 + 1; m++)
                            {
                                if (matY >= routerRadius)
                                {
                                    for (int n = matY; n < routerRadius * 2 + 1; n++)
                                    {
                                        notPosible[m, n] = true;
                                    }
                                }
                                else if (matY < routerRadius)
                                {
                                    for (int n = matY; n >= 0; n--)
                                    {
                                        notPosible[m, n] = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int m = matX; m >= 0; m--)
                            {
                                if (matY >= routerRadius)
                                {
                                    for (int n = matY; n < routerRadius * 2 + 1; n++)
                                    {
                                        notPosible[m, n] = true;
                                    }
                                }
                                else
                                {
                                    for (int n = matY; n >= 0; n--)
                                    {
                                        notPosible[m, n] = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return notPosible;
        }
    }

    internal class Shit : IComparer<ScoredCoordinate>
    {
        public int Compare(ScoredCoordinate scoredCoordinate, ScoredCoordinate y)
        {
            var scoreCompare = scoredCoordinate.Score.CompareTo(y.Score);
            if (scoreCompare != 0)
            {
                return scoreCompare;
            }

            var xCompare = scoredCoordinate.Coordinate.Row.CompareTo(y.Coordinate.Row);
            if (xCompare != 0)
            {
                return xCompare;
            }

            return scoredCoordinate.Coordinate.Column.CompareTo(y.Coordinate.Column);
        }
    }

    internal class ScoredCoordinate
    {
        public MatrixCoordinate Coordinate { get; set; }

        public int Score { get; set; }
    }
}