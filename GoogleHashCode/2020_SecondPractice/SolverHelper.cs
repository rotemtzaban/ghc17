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
            foreach (var server in input.Servers)
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

            List<PoolDetails> poolDetails = new List<PoolDetails>(input.NumOfPools);
            for (int i = 0; i < input.NumOfPools; i++)
            {
                poolDetails[i] = new PoolDetails(i);
                if (!poolToCapacity.ContainsKey(i)){
                    poolDetails[i].GuaranteedCapacity = 0;
                    continue;
                }

                long minGcPool = poolToCapacity[i];
                for (int j = 0; j < input.NumOfRows; j++)
                {
                    long currentRowDownGc = rowToServers.ContainsKey(j)
                        ? poolToCapacity[i] - rowToServers[j].Sum(_ => input.Servers[_].Capacity)
                        : poolToCapacity[i];
                    minGcPool = Math.Min(minGcPool, currentRowDownGc);
                }

                poolDetails[i].GuaranteedCapacity = minGcPool;
            }

            poolDetails.Sort();
            return poolDetails;
        }
    }

    public class PoolDetails : IndexedObject
    {
        public PoolDetails(int index) : base(index)
        {
        }

        public long GuaranteedCapacity { get; set; }
    }
}
