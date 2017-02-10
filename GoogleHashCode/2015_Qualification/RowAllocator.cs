using System;
using System.Collections.Generic;
using System.Linq;
using HashCodeCommon;

namespace _2015_Qualification
{
	public class RowAllocator
	{
		private int _nextRowToUse = 0;
		private Dictionary<int, Row> _allRows;
		private readonly ProblemOutput _result;
		private readonly ProblemInput _input;
		private Random _random;
		private Stack<Server> _unusedServers;

		public RowAllocator(ProblemInput input, ProblemOutput result, Random random)
		{
			_random = random;
			_result = result;
			_input = input;
			_unusedServers = new Stack<Server>();
			CreateRows();
		}

		public bool HasUnusedServers
		{
			get { return _unusedServers.Any(); }
		}

		public bool AllocateNextServerToPool(ProblemInput input, ServerSelector serverSelector, Pool pool)
		{
			var nextServer = serverSelector.UseNextServer();
			ServerAllocation allocation = AlllocateServerToRow(input, nextServer, pool);
			if (allocation == null)
			{
				_unusedServers.Push(nextServer);
				return false;
			}

			allocation.Pool = pool;

			_result._allocations.Add(allocation.Server, allocation);
			return true;
		}

		private void CreateRows()
		{
			_allRows = new Dictionary<int, Row>();
			for (int i = 0; i < _input.Rows; i++)
				_allRows[i] = new Row(_input, i);
		}

		private ServerAllocation AlllocateServerToRow(ProblemInput input, Server server, Pool pool)
		{
			Row row;
			int column;
			int tries = 0;
			do
			{
				row = GetNextRowForPool(pool);
				column = row.GetAndAcquireSlot(server.Slots);
				tries++;
				if (tries > input.Rows * 2)
					return null;

			} while (column == -1);

			return new ServerAllocation { InitialColumn = column, Row = row._rowIndex, Server = server };
		}

		private Row GetNextRowForPool(Pool pool)
		{
			var allRows = Enumerable.Range(0, _input.Rows).ToList();
			var usedRows = new HashSet<int>(_result._allocations.Values.Where(v => Equals(v.Pool, pool)).Select(v => v.Row).Distinct());
			var availbleRows = allRows.Where(r => !usedRows.Contains(r)).ToList();
			var newRow = availbleRows.Any() ? availbleRows.RandomElement(_random) : allRows.RandomElement(_random);
			return _allRows[newRow];
		}

		private Row GetNextRow()
		{
			var res = _nextRowToUse;

			_nextRowToUse++;
			if (_nextRowToUse >= _input.Rows)
				_nextRowToUse = 0;

			return _allRows[res];
		}

		public void AllocateUnsedServerToPool(Pool pool)
		{
			var nextServer = _unusedServers.Pop();

			int col = -1;
			int row = 0;
			for (; row < _input.Rows; row++)
			{
				col = _allRows[row].GetAndAcquireSlot(nextServer.Slots);
				if (col != -1)
				{
					break;
				}
			}

			if (col == -1)
				return;

			ServerAllocation allocation = new ServerAllocation {InitialColumn = col, Row = row, Server = nextServer};

			allocation.Pool = pool;

			_result._allocations.Add(allocation.Server, allocation);
		}
	}
}