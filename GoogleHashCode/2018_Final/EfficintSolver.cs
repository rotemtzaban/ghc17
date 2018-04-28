using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_Final
{
    public class EfficintSolver : Solver
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            if (input.Rows != 1000)
                return base.Solve(input);
            int Factor = 10;

            input.Rows /= Factor;
            input.Columns /= Factor;

            ProblemOutput basePit = base.Solve(input);

            ProblemOutput newOutput = new ProblemOutput();
            newOutput.Buildings = new List<OutputBuilding>();
            foreach (var item in basePit.Buildings.ToList())
            {
                for (int i = 0; i < Factor; i++)
                {
                    for (int j = 0; j < Factor; j++)
                    {
                        newOutput.Buildings.Add(new OutputBuilding()
                        {
                            Coordinate = new MatrixCoordinate(item.Coordinate.Row + i * input.Rows, 
                            item.Coordinate.Column + j * input.Columns),
                            ProjectNumber = item.ProjectNumber
                        });
                    }
                }
            }

            return newOutput;
        }
    }
}
