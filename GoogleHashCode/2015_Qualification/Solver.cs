using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Qualification
{
    class Solver : ISolver<ProblemInput, ProblemOutput>
    {
        public ProblemOutput Solve(ProblemInput input)
        {
			var result = new ProblemOutput();
			var availableServersByCapacity = input.Servers.OrderBy(x => x.Capacity).ToList();

			// foreach(var pool in input.)

			return result;
        }
    }
}
