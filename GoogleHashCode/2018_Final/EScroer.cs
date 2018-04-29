using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_Final
{
    public class EScroer : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            ProblemOutput output = new ProblemOutput();
            output.Buildings = new List<OutputBuilding>();
            BuildingProject utiliy = GetBestUtility(input);
            // List<OutputBuilding> residntialis = GetBestResidentials(input);
            List<OutputBuilding> residntialis = new List<OutputBuilding>();

            bool[,] filles = new bool[input.Rows, input.Columns];
            for (int row = 0; row < input.Rows; row += input.MaxDistance  + utiliy.Plan.GetLength(0))
            {
                for (int col = 0; col < input.Columns; col += input.MaxDistance + utiliy.Plan.GetLength(1) + 1)
                {
                    output.Buildings.Add(new OutputBuilding()
                    { Coordinate = new MatrixCoordinate(row, col), ProjectNumber = utiliy.Index });
                    for (int i = 0; i < utiliy.Plan.GetLength(0); i++)
                    {
                        for (int j = 0; j < utiliy.Plan.GetLength(1); j++)
                        {
                            filles[row + i, col + j] = utiliy.Plan[i, j];
                        }
                    }

                    foreach (var item in residntialis)
                    {
                        output.Buildings.Add(new OutputBuilding()
                        {
                            Coordinate = new MatrixCoordinate(item.Coordinate.Row, item.Coordinate.Column),
                            ProjectNumber = item.ProjectNumber
                        });
                    }
                }
            }

            var Size1Building = input.BuildingProjects.FirstOrDefault(_ => _.Plan.Length == 1 && _.BuildingType == BuildingType.Residential);
            if (Size1Building != null)
            {
                for (int i = 0; i < filles.GetLength(0); i++)
                {
                    for (int j = 0; j < filles.GetLength(1); j++)
                    {
                        if (filles[i, j])
                            continue;
                        output.Buildings.Add(new OutputBuilding()
                        {
                            Coordinate = new MatrixCoordinate(i, j),
                            ProjectNumber = Size1Building.Index
                        });
                    }
                }
            }
            return output;
        }

        private List<OutputBuilding> GetBestResidentials(ProblemInput input)
        {
            var allResidintials = input.BuildingProjects.Where(_ => _.BuildingType == BuildingType.Residential).
                OrderByDescending(OrderByResidintialMethod).ToList();

            foreach (var item in allResidintials)
            {
                // if (Can)
            }

            return null;
        }

        private object OrderByResidintialMethod(BuildingProject arg)
        {
            return 1.0 * arg.Capacity / arg.Plan.GetLength(0) / arg.Plan.GetLength(1);
        }

        private BuildingProject GetBestUtility(ProblemInput input)
        {
            double bestRatio = -1;
            BuildingProject bestProject = null;
            foreach (var item in input.BuildingProjects)
            {
                if (item.BuildingType == BuildingType.Residential)
                {
                    continue;
                }

                double currRatio = 1.0 * item.Plan.GetLength(0) * item.Plan.GetLength(1) / item.Plan.Cast<bool>().Count(_ => _);
                if (currRatio > bestRatio)
                {
                    bestProject = item;
                    bestRatio = currRatio;
                }
            }

            return bestProject;
        }
    }
}
