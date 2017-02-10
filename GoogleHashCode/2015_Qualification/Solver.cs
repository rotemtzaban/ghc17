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
		private Dictionary<Pool, int> _poolGuaranteedCapacities;
		private ProblemOutput _result;
		private ProblemInput _input;
		private RowAllocator _rowAllocator;
		private ServerSelector _serverSelector;

		public ProblemOutput Solve(ProblemInput input)
		{
			_input = input;
			_poolGuaranteedCapacities = new Dictionary<Pool, int>();
			_result = new ProblemOutput{ _allocations = new Dictionary<Server, ServerAllocation>(), original_input = input};

			_rowAllocator = new RowAllocator(_input, _result, new Random());
			_serverSelector = new ServerSelector(_input, _result);

			// TODO: Make sure order is correct

			InitializeServers(input, _serverSelector);

            int used = 0, notUsed = 0;

			while (_serverSelector.HasAvailableServer)
			{
				var pool = GetLowestCapacityPool();
				if (!_rowAllocator.AllocateNextServerToPool(input, _serverSelector, pool))
                {
                    notUsed++;
                }
                else
                {
                    used++;
				    _poolGuaranteedCapacities[pool] = pool.GurranteedCapacity(_result);
                }
            }

			while (_rowAllocator.HasUnusedServers)
			{
				var pool = GetLowestCapacityPool();
				_rowAllocator.AllocateUnsedServerToPool(pool);
			}

			//Console.WriteLine("Used: "+ used);
			//Console.WriteLine("NotUsed: "+ notUsed);

			return _result;
		}

		private Pool GetLowestCapacityPool()
		{
			return _poolGuaranteedCapacities.ArgMin(kvp => kvp.Value).Key;
		}

		private void InitializeServers(ProblemInput input, ServerSelector serverSelector)
		{
			IEnumerable<Pool> reversedPools = input.Pools;
			reversedPools.Reverse();

			foreach (var pool in input.Pools)
			{
				if (!_rowAllocator.AllocateNextServerToPool(input, serverSelector, pool))
					throw new Exception("Couldn't allocate in initialization!");
			}

			foreach (var pool in reversedPools)
			{
				if (!_rowAllocator.AllocateNextServerToPool(input, serverSelector, pool))
					throw new Exception("Couldn't allocate in initialization!");
			}

			foreach (var pool in input.Pools)
				_poolGuaranteedCapacities[pool] = pool.GurranteedCapacity(_result);
		}
	}
}
