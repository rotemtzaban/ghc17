using System.Collections.Generic;

namespace _2015_Qualification
{
	public class RowAllocator
	{
		private int _nextRowToUse = 0;
		private Dictionary<int, Row> _allRows;
		private readonly ProblemOutput _result;
		private readonly ProblemInput _input;

		public RowAllocator(ProblemInput input, ProblemOutput result)
		{
			_result = result;
			_input = input;
			CreateRows();
		}

		public bool AllocateNextServerToPool(ProblemInput input, ServerSelector serverSelector, Pool pool)
		{
			var nextServer = serverSelector.UseNextServer();
			ServerAllocation allocation = AlllocateServerToRow(input, nextServer);
			if (allocation == null)
				return false;

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
				if (tries > input.Rows * 2)
					return null;

			} while (column == -1);

			return new ServerAllocation { InitialColumn = column, Row = row._rowIndex, Server = server };
		}
		private Row GetNextRow()
		{
			var res = _nextRowToUse;

			_nextRowToUse++;
			if (_nextRowToUse >= _input.Rows)
				_nextRowToUse = 0;

			return _allRows[res];
		}
	}
}