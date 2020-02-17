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
        public static void ImproveWorstPoolWorstRow(ProblemInput input, ProblemOutput output2)
        {
            bool swap = true;
            int m = 0;
            while (swap)
            {
                // Console.WriteLine(m++);
                swap = Swap(input);
            }
        }

        private static bool Swap(ProblemInput input)
        {
            var worst = input.Pools.Min(_ => _.GuaranteedCapacity);
            var allBadPools = input.Pools.Where(_ => _.GuaranteedCapacity == worst);

            foreach (var poolToImprove in allBadPools.ToList())
            {
                foreach (PoolDetails goodPool in input.Pools.OrderByDescending(_ => _.GuaranteedCapacity).ToList())
                {
                    for (int j = 0; j < input.NumOfRows; j++)
                    {
                        foreach (var server1 in goodPool.Servers.Where(_ => _.Row == j).ToList())
                        {
                            foreach (var server2 in poolToImprove.Servers.Where(_ => _.Capacity < server1.Capacity && _.Row != server1.Row).ToList())
                            {
                                poolToImprove.RemoveServerFromPool(server2);
                                goodPool.RemoveServerFromPool(server1);

                                goodPool.AddServerToPool(server2);
                                poolToImprove.AddServerToPool(server1);

                                if (goodPool.GuaranteedCapacity > worst && poolToImprove.GuaranteedCapacity > worst)
                                    return true;
                                else
                                {
                                    poolToImprove.RemoveServerFromPool(server1);
                                    goodPool.RemoveServerFromPool(server2);

                                    goodPool.AddServerToPool(server1);
                                    poolToImprove.AddServerToPool(server2);
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private static List<Row> GetRows(ProblemInput input, PoolDetails poolToImprove)
        {
            List<Row> rows = new List<Row>();
            for (int i = 0; i < input.NumOfRows; i++)
            {
                rows.Add(new Row(i) { Capacity = poolToImprove.RowsCapacity[i] });
            }

            return rows;
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

        public HashSet<Server> Servers { get; set; } = new HashSet<Server>();

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
            long worstRowForPool = long.MaxValue;
            for (int i = 0; i < RowsCapacity.Count(); i++)
            {
                long currentRowDownGc = Capacity - RowsCapacity[i];

                if (bestRowForPool < RowsCapacity[i])
                {
                    bestRowForPool = RowsCapacity[i];
                    _BestRow = i;
                }

                if (worstRowForPool > RowsCapacity[i])
                {
                    worstRowForPool = RowsCapacity[i];
                   _WorstRow = i;
                }

                if (currentRowDownGc >= _GuaranteedCapacity) continue;
                _GuaranteedCapacity = currentRowDownGc;
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
            Servers.Add(server);
            isAllUpdated = false;
        }

        public void RemoveServerFromPool(Server server)
        {
            server.PoolAssigned = null;
            RowsCapacity[server.Row.Value] -= server.Capacity;
            Capacity -= server.Capacity;
            Servers.Remove(server);
            isAllUpdated = false;
        }
    }
}
