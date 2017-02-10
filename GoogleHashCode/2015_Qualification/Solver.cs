using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Qualification
{
	public class Solver : ISolver<ProblemInput, ProblemOutput>
	{
		private int _nextRowToUse = 0;
		private Dictionary<int, Row> _allRows;
		private Dictionary<Pool, int> _poolGuaranteedCapacities;
		private ProblemOutput _result; 

		private Row GetNextRow()
		{
			var res = _nextRowToUse;
			_nextRowToUse++;
			return _allRows[res];
		}

		public ProblemOutput Solve(ProblemInput input)
		{
			CreateRows(input);
			_result = new ProblemOutput();
			var availableServersByCapacity = new Stack<Server>(input.Servers.OrderBy(x => x.Capacity));

			InitializeServers(input, availableServersByCapacity);

			while (availableServersByCapacity.Count > 0)
			{
				var pool = GetLowestCapacityPool();
				if (!AllocateNextServerToPool(input, availableServersByCapacity, pool))
					break;
			}

			return _result;
		}

		private Pool GetLowestCapacityPool()
		{
			throw new NotImplementedException();
		}

		private void InitializeServers(ProblemInput input, Stack<Server> availableServersByCapacity)
		{
			foreach (var pool in input.Pools)
			{
				if(!AllocateNextServerToPool(input, availableServersByCapacity, pool))
					throw new Exception("Couldn't allocate in initialization!");
				if(!AllocateNextServerToPool(input, availableServersByCapacity, pool))
					throw new Exception("Couldn't allocate in initialization!");
			}
		}

		private bool AllocateNextServerToPool(ProblemInput input, Stack<Server> availableServersByCapacity, Pool pool)
		{
			var nextServer = availableServersByCapacity.Pop();
			var allocation = AlllocateServerToRow(input, nextServer);
			if (allocation == null)
				return false;
			allocation.Pool = pool;

			_result._allocations.Add(allocation.Server, allocation);
			pool.Servers.Add(nextServer);
			return true;
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
				column = row.GetAndAqcuireSlot(server.Slots);
				tries++;
				if (tries > input.Rows*2)
				{
					// TODO: Does this happen? What should we do?
					return null;
				}
			} while (column == -1);

			return new ServerAllocation{ InitialColumn = column, Row = row._rowIndex, Server = server};
		}
	}
}
