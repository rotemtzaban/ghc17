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
		private ProblemInput _input;

		private Row GetNextRow()
		{
			var res = _nextRowToUse;

			_nextRowToUse++;
			if (_nextRowToUse >= _input.Rows)
				_nextRowToUse = 0;
			
			return _allRows[res];
		}

		public ProblemOutput Solve(ProblemInput input)
		{
			_input = input;
			_poolGuaranteedCapacities = new Dictionary<Pool, int>();
			CreateRows(input);
			_result = new ProblemOutput{ _allocations = new Dictionary<Server, ServerAllocation>(), original_input = input};

			// TODO: Make sure order is correct
			var availableServersByCapacity = new Stack<Server>(GetServerListByPreference(input));

			InitializeServers(input, availableServersByCapacity);

			while (availableServersByCapacity.Count > 0)
			{
				var pool = GetLowestCapacityPool();
				if (!AllocateNextServerToPool(input, availableServersByCapacity, pool))
					continue;

				_poolGuaranteedCapacities[pool] = pool.GurranteedCapacity(_result);
			}

			return _result;
		}

		private Pool GetLowestCapacityPool()
		{
			return _poolGuaranteedCapacities.ArgMin(kvp => kvp.Value).Key;
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

			foreach (var pool in input.Pools)
				_poolGuaranteedCapacities[pool] = pool.GurranteedCapacity(_result);
		}

		private bool AllocateNextServerToPool(ProblemInput input, Stack<Server> availableServersByCapacity, Pool pool)
		{
			var nextServer = availableServersByCapacity.Pop();
			ServerAllocation allocation = AlllocateServerToRow(input, nextServer);
			if (allocation == null)
				return false;

			allocation.Pool = pool;

			_result._allocations.Add(allocation.Server, allocation);
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

		private IOrderedEnumerable<Server> GetServerListByPreference(ProblemInput input)
		{
			return input.Servers.OrderBy(x => ((double)x.Capacity) / x.Slots);
		}
	}
}
