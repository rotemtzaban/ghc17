using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Qualification
{
	class Row
	{
		public Row(ProblemInput input)
		{
			_isAvailable = new List<bool>(input.Rows);
			for (int i = 0; i < input.Rows; i++)
				_isAvailable[i] = true;
		}

		public IList<bool> _isAvailable;
	}

	class Solver : ISolver<ProblemInput, ProblemOutput>
	{
		public ProblemOutput Solve(ProblemInput input)
		{
			var result = new ProblemOutput();
			var availableServersByCapacity = new Stack<Server>(input.Servers.OrderBy(x => x.Capacity));

			foreach (var pool in input.Pools)
			{
				var nextServer = availableServersByCapacity.Pop();
				var nextServer2 = availableServersByCapacity.Pop();

				AlllocateServerToRow(input, nextServer);
				//pool.Servers.Add()
			}

			return result;
		}

		private void AlllocateServerToRow(ProblemInput input, Server server)
		{
			//input.Rows.
			throw new NotImplementedException();
		}
	}
}
