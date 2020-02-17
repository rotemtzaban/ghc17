using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_SecondPractice
{
    public class Calculator : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            Dictionary<int, HashSet<int>> rowToServers = new Dictionary<int, HashSet<int>>();
            Dictionary<int, HashSet<int>> poolToServer = new Dictionary<int, HashSet<int>>();
            foreach (var server in output.Servers)
            {
                if (server.Row == null) continue;
                if (!rowToServers.ContainsKey(server.Row.Value))
                {
                    rowToServers[server.Row.Value] = new HashSet<int>();
                }

                if (!poolToServer.ContainsKey(server.PoolAssigned.Value))
                {
                    poolToServer[server.PoolAssigned.Value] = new HashSet<int>();
                }

                poolToServer[server.PoolAssigned.Value].Add(server.Index);
                rowToServers[server.Row.Value].Add(server.Index);
            }

            long minGc = long.MaxValue;

            for (int i = 0; i < input.NumOfPools; i++)
            {
            }

            return minGc;
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
