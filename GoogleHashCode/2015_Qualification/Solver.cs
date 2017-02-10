using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Qualification
{
	public class Solver : ISolver<ProblemInput, ProblemOutput>
	{
		private int _nextRowToUse = 0;
		private Dictionary<int, Row> _allRows; 


		private Row GetNextRow()
		{
			var res = _nextRowToUse;
			_nextRowToUse++;
			return _allRows[res];
		}

		public ProblemOutput Solve(ProblemInput input)
		{
			CreateRows(input);
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

		private void CreateRows(ProblemInput input)
		{
			_allRows = new Dictionary<int, Row>();
			for (int i = 0; i < input.Rows; i++)
				_allRows[i] = new Row(input, i);
		}

		private ServerAllocation AlllocateServerToRow(ProblemInput input, Server server)
		{
			Row row;
			int column;
			int tries = 0;
			do
			{
				row = GetNextRow();
				column = row.GetSpace(server.Slots);
				tries++;
				if (tries > input.Rows*2)
				{
					// TODO: Does this happen? What should we do?
					throw new Exception("Unable to allocate server because no more space!");
				}
			} while (column == -1);

			return new ServerAllocation{ InitialColumn = column, Row = row.RowIndex, Server = server};
		}
	}
}
