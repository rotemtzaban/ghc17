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

            input.Rows /= 5;
            input.Columns /= 5;

            ProblemOutput basePit = base.Solve(input);

            ProblemOutput newOutput = new ProblemOutput();
            newOutput.Buildings = new List<OutputBuilding>();
            foreach (var item in basePit.Buildings.ToList())
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        newOutput.Buildings.Add(new OutputBuilding()
                        {
                            Coordinate = new MatrixCoordinate(item.Coordinate.Row + i * 100, 
                            item.Coordinate.Column + j * 100),
                            ProjectNumber = item.ProjectNumber
                        });
                    }
                }
            }

            return newOutput;
        }
    }
}
