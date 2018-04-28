using System;
using System.Linq;
using HashCodeCommon;

namespace _2018_Final
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            ProblemOutput output = new ProblemOutput();

            var residtianls = input.BuildingProjects.Where(_ => _.BuildingType == BuildingType.Residential).ToList();
            var utility = input.BuildingProjects.Where(_ => _.BuildingType == BuildingType.Utility).ToList();

            // Order residintial
            // Order utility

            throw new NotImplementedException();
        }
    }
}