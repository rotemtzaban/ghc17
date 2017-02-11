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
	public class Solver : SolverBase<ProblemInput, ProblemOutput>
	{
		private Dictionary<Pool, int> _poolGuaranteedCapacities;
		private ProblemOutput _result;
		private ProblemInput _input;
		private RowAllocator _rowAllocator;
		private ServerSelector _serverSelector;

		protected override ProblemOutput Solve(ProblemInput input)
		{
			_input = input;
			_poolGuaranteedCapacities = new Dictionary<Pool, int>();
			_result = new ProblemOutput{ _allocations = new Dictionary<Server, ServerAllocation>(), original_input = input};

			_rowAllocator = new RowAllocator(_input, _result, this.NumbersGenerator);
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
				_poolGuaranteedCapacities[pool] = pool.GurranteedCapacity(_result);
			}

			//var lowestPool = GetLowestCapacityPool();
			//var highestPool = GetHighestCapacityPool();
			//Console.WriteLine("Lowest ({0})", lowestPool.GurranteedCapacity(_result));
			//PrintPoolRows(lowestPool);
			//Console.WriteLine("Highest ({0})", highestPool.GurranteedCapacity(_result));
			//PrintPoolRows(highestPool);

			TryToMoveServersFromBestToWorst();

			//Console.WriteLine("Used: "+ used);
			//Console.WriteLine("NotUsed: "+ notUsed);

			return _result;
		}

		private void TryToMoveServersFromBestToWorst()
		{
			while (true)
			{
				var lowestPool = GetLowestCapacityPool();
				var highestPool = GetHighestCapacityPool();

				var lowestAllocations = GetAllPoolAllocations(lowestPool);
				var highestRow = GetHighestRow(lowestAllocations);
				var highestAllocations = GetAllPoolAllocations(highestPool);

				var freeAllocation = highestAllocations.Where(a => a.Row != highestRow).ArgMin(a => a.Server.Capacity);

				var lowestScore = _poolGuaranteedCapacities[lowestPool];

				freeAllocation.Pool = lowestPool;

				var newHighestScore = highestPool.GurranteedCapacity(_result);
				var newLowestScore = lowestPool.GurranteedCapacity(_result);

				if (newHighestScore <= lowestScore)
				{
					// Wasn't worth it
					freeAllocation.Pool = highestPool;
					break;
				}
				else
				{
					_poolGuaranteedCapacities[highestPool] = newHighestScore;
					_poolGuaranteedCapacities[lowestPool] = newLowestScore;
				}
			}
		}

		private int GetHighestRow(IEnumerable<ServerAllocation> allocations)
		{
			return allocations.GroupBy(a => a.Row).ArgMax(g => g.Sum(x => x.Server.Capacity)).Key;
		}

		private void PrintPoolRows(Pool ppool)
		{
			foreach (
				var row in GetAllPoolAllocations(ppool).GroupBy(a => a.Row).OrderBy(g => g.Key))
			{
				Console.WriteLine("Row {0}: total {1} ({2})", row.Key, row.Sum(x => x.Server.Capacity), string.Join(", ", row.Select(r => r.Server.Capacity).ToList()));
			}
		}

		private IEnumerable<ServerAllocation> GetAllPoolAllocations(Pool ppool)
		{
			return _result._allocations.Values.Where(a => a.Pool.Equals(ppool));
		}

		private Pool GetLowestCapacityPool()
		{
			return _poolGuaranteedCapacities.ArgMin(kvp => kvp.Value).Key;
		}

		private Pool GetHighestCapacityPool()
		{
			return _poolGuaranteedCapacities.ArgMax(kvp => kvp.Value).Key;
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
