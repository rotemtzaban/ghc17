using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodeCommon;

namespace _2020_SecondPractice
{
    public static class SolverHelper
    {
        public static List<PoolDetails> GetPoolsGC(ProblemInput input, ProblemOutput output2)
        {
            Dictionary<int, List<int>> rowToServers = new Dictionary<int, List<int>>();
            Dictionary<int, HashSet<int>> poolToServer = new Dictionary<int, HashSet<int>>();
            Dictionary<int, long> poolToCapacity = new Dictionary<int, long>();
            foreach (var server in output2.Servers)
            {
                if (server.Row == null) continue;
                if (!rowToServers.ContainsKey(server.Row.Value))
                {
                    rowToServers[server.Row.Value] = new List<int>();
                }

                if (!poolToServer.ContainsKey(server.PoolAssigned.Value))
                {
                    poolToServer[server.PoolAssigned.Value] = new HashSet<int>();
                    poolToCapacity[server.PoolAssigned.Value] = 0;
                }

                poolToServer[server.PoolAssigned.Value].Add(server.Index);
                rowToServers[server.Row.Value].Add(server.Index);
                poolToCapacity[server.PoolAssigned.Value] += server.Capacity;
            }

            //List<PoolDetails> poolDetails = new List<PoolDetails>(input.NumOfPools);
            //for (int i = 0; i < input.NumOfPools; i++)
            //{
            //    poolDetails[i] = new PoolDetails(i);
            //    if (!poolToCapacity.ContainsKey(i))
            //    {
            //        poolDetails[i].WorstRow = -1;
            //        poolDetails[i].GuaranteedCapacity = 0;
            //        continue;
            //    }

            //    long minGcPool = poolToCapacity[i];
            //    long bestRowForPool = long.MinValue;
            //    for (int j = 0; j < input.NumOfRows; j++)
            //    {
            //        long rowForPool = rowToServers.ContainsKey(j)
            //            ? rowToServers[j].Sum(_ => input.Servers[_].Capacity)
            //            : 0;
            //        long currentRowDownGc = poolToCapacity[i] - rowForPool;

            //        if (bestRowForPool < rowForPool)
            //        {
            //            bestRowForPool = rowForPool;
            //            poolDetails[i].BestRow = j;
            //        }

            //        if (currentRowDownGc >= minGcPool) continue;
            //        minGcPool = currentRowDownGc;
            //        poolDetails[i].WorstRow = j;
            //    }

            //    poolDetails[i].GuaranteedCapacity = minGcPool;
            //}

            //poolDetails.Sort();
            //return poolDetails;
            return null;
        }

        public static void ImproveWorstPoolWorstRow(ProblemInput input, ProblemOutput output2)
        {
            var details = GetPoolsGC(input, output2);

            var worstPool = details[0];
            var worstGc = worstPool.GuaranteedCapacity;

            var poolToChange = details.FirstOrDefault(_ => _.BestRow == worstPool.WorstRow);
            if (poolToChange == null)
            {

            }
            else
            {
                ReplaceServer(poolToChange, worstPool, worstPool.WorstRow, input);
            }
        }

        private static void ReplaceServer(PoolDetails poolToChange, PoolDetails worstPool, int worstPoolWorstRow, ProblemInput input)
        {
        }
    }

    public class PoolDetails : IndexedObject
    {
        public PoolDetails(int index, int numberOfRows) : base(index)
        {
            RowsCapacity = new int[numberOfRows];
        }

        private bool isAllUpdated = false;
        private long _GuaranteedCapacity = long.MinValue;
        private int _WorstRow = -1;
        private int _BestRow = -1;

        public int WorstRow
        {
            get
            {
                if (isAllUpdated)
                {
                    return _WorstRow;
                }
                else
                {
                    UpdateAll();
                    return _WorstRow;
                }
            }
        }
        public int BestRow
        {
            get
            {
                if (isAllUpdated)
                {
                    return _BestRow;
                }
                else
                {
                    UpdateAll();
                    return _BestRow;
                }
            }
        }

        public long GuaranteedCapacity
        {
            get
            {
                if (isAllUpdated)
                {
                    return _GuaranteedCapacity;
                }
                else
                {
                    UpdateAll();
                    return _GuaranteedCapacity;
                }
            }
        }

        private void UpdateAll()
        {
            _GuaranteedCapacity = Capacity;
            long bestRowForPool = long.MinValue;
            for (int i = 0; i < RowsCapacity.Count(); i++)
            {
                long currentRowDownGc = Capacity - RowsCapacity[i];

                if (bestRowForPool < RowsCapacity[i])
                {
                    bestRowForPool = RowsCapacity[i];
                    _BestRow = i;
                }

                if (currentRowDownGc >= _GuaranteedCapacity) continue;
                _GuaranteedCapacity = currentRowDownGc;
                _WorstRow = i;
            }

            isAllUpdated = true;
        }

        public long Capacity { get; set; }
        public int[] RowsCapacity { get; set; }

        public void AddServerToPool(Server server)
        {
            server.PoolAssigned = this.Index;
            RowsCapacity[server.Row.Value] += server.Capacity;
            Capacity += server.Capacity;
            isAllUpdated = false;
        }

        public void RemoveServerFromPool(Server server)
        {
            server.PoolAssigned = null;
            RowsCapacity[server.Row.Value] -= server.Capacity;
            Capacity -= server.Capacity;
            isAllUpdated = false;
        }
    }
}
