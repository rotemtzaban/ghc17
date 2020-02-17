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
            // PrintDc(input);
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

            // Console.WriteLine();

            TryFix(input, output);
            SolverHelper.ImproveWorstPoolWorstRow(input, output);

            // PrintDc(input);


            return output;
        }

        private void TryFix(ProblemInput input, ProblemOutput output)
        {
            var unusedServersBySize = input.Servers.Except(output.Servers)
                .GroupBy(server => server.Size)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(s => s.Capacity).ToList());

            //var unusedServers = input.Servers.Except(output.Servers).ToList();

            var copy = output.Servers.ToList();
            foreach (var server in copy)
            {
                var serverSize = server.Size;
                var serverCapacity= server.Capacity;

                var unusedServers = unusedServersBySize.SelectMany(pair => pair.Value.Take(server.Size / pair.Key)).ToList();
                var servers = TryReplaceServer(unusedServers, serverSize, serverCapacity);
                if (servers != null)
                {
                    output.Servers.Remove(server);
                    var pool = input.Pools[server.PoolAssigned.Value];

                    pool.RemoveServerFromPool(server);

                    int index = 0;

                    for (int j = 0; j < server.Size; j++)
                    {
                        input.Slots[server.Row.Value, server.SlotInRow + j] = false;
                    }

                    foreach (var replacedServer in servers)
                    {
                        replacedServer.Row = server.Row;
                        pool.AddServerToPool(replacedServer);
                        replacedServer.SlotInRow = index + server.SlotInRow;
                        output.Servers.Add(replacedServer);
                        for (int j = 0; j < replacedServer.Size; j++)
                        {
                            input.Slots[server.Row.Value, server.SlotInRow + index + j] = true;
                        }
                        index += replacedServer.Size;

                        unusedServersBySize[replacedServer.Size].Remove(replacedServer);
                    }
                    
                    pool.RemoveServerFromPool(server);
                    server.Row = null;
                    server.PoolAssigned = null;
                    server.SlotInRow = -1;


                    unusedServersBySize[server.Size].Add(server);
                }
            }
        }

        private List<Server> TryReplaceServer(List<Server> unusedServers, int currentServerSize, int currentCapacity)
        {
            if (currentServerSize == 0 && currentCapacity <= 0)
            {
                return new List<Server>();
            }

            if (currentServerSize <= 0)
            {
                return null;
            }

            var serversCopy = unusedServers.ToList();

            foreach (var unusedServer in unusedServers)
            {
                serversCopy.Remove(unusedServer);
                var tryReplaceServer = TryReplaceServer(serversCopy, currentServerSize - unusedServer.Size,
                    currentCapacity - unusedServer.Capacity);
                if (tryReplaceServer != null)
                {
                    tryReplaceServer.Add(unusedServer);
                    return tryReplaceServer;
                }
            }

            return null;
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
            var ordered = servers.OrderByDescending(server => Math.Pow(server.Capacity, 1.5) / server.Size)
                .ThenByDescending(server => server.Size)
                .ToList();

            
            return ordered;
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
