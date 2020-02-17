using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_SecondPractice
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            var rows = new List<List<Server>>();
            for (var i=0; i < input.NumOfRows; i++)
                rows.Add(new List<Server>());
            
            var pools = new List<List<Server>>();
            for (int i = 0; i < input.NumOfPools; i++)
            {
                pools.Add(new List<Server>());
            }

            var orderedServers = orderServers(input.Servers);

            while (true)
            {
                int maxPool = 0;
                int maxRow = 0;
                int maxScore = 0;
                var row = getRowToInsert(rows);
            }

            return null;
            throw new NotImplementedException();
        }

        private List<Server> orderServers(List<Server> servers)
        {
            return servers.OrderByDescending(server => server.Capacity / server.Size)
                .ThenByDescending(server => server.Size)
                .ToList();
        }
    }
}
