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
            PrintDc(input);
            var rows = new List<List<Server>>();
            for (var i=0; i < input.NumOfRows; i++)
                rows.Add(new List<Server>());
            
            var pools = new List<List<Server>>();
            for (int i = 0; i < input.NumOfPools; i++)
            {
                pools.Add(new List<Server>());
            }

            var orderedServers = orderServers(input.Servers);

            ProblemOutput output = new ProblemOutput{Servers = new List<Server>()};
            
            foreach (var server in orderedServers)
            {
                var minPool = input.Pools.OrderBy(details => details.GuaranteedCapacity).First();

                foreach (var row in minPool.RowsCapacity.Select((i, i1) => new {Capacity = i, Index = i1}).OrderBy(arg => arg.Capacity))
                {
                    if (TryPlaceServer(row.Index, server, minPool, input, output))
                    {
                        break;
                    }     
                }
            }

            Console.WriteLine();

            PrintDc(input);

            return output;
        }

        private bool TryPlaceServer(int row, Server server, PoolDetails minPool, ProblemInput input , ProblemOutput output)
        {
            for (int i = 0; i < input.RowSize - server.Size + 1; i++)
            {
                bool canPlace = true;
                for (int j = 0; j < server.Size; j++)
                {
                    canPlace &= !input.Slots[row, i + j];
                }

                if (canPlace)
                {
                    server.Row = row;
                    minPool.AddServerToPool(server);
                    server.SlotInRow = i;
                    output.Servers.Add(server);
                    for (int j = 0; j < server.Size; j++)
                    {
                        input.Slots[row, i + j] = true;
                    }

                    return true;
                }
            }

            return false;
        }

        
        private List<Server> orderServers(List<Server> servers)
        {
            return servers.OrderByDescending(server => server.Capacity / server.Size)
                .ThenByDescending(server => server.Size)
                .ToList();
        }

        private void PrintDc(ProblemInput input)
        {
            for (int i = 0; i < input.Slots.GetLength(0); i++)
            {
                for (int j = 0; j < input.Slots.GetLength(1); j++)
                {
                    Console.Write(input.Slots[i, j] ? "x" : "+");
                }

                Console.WriteLine();
            }
        }
    }
}
