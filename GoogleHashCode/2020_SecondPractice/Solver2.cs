using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_SecondPractice
{
    public class Solver2 : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            List<Server>[] serverRows = new List<Server>[input.NumOfRows];
            ProblemOutput output = new ProblemOutput();
            output.Servers = new List<Server>();
            List<Row> rows = new List<Row>();
            for (int i = 0; i < input.NumOfRows; i++)
            {
                rows.Add(new Row(i));
            }

            foreach (Server server in input.Servers.OrderByDescending(_ => _.Capacity / _.Size).ThenBy(_ => _.Size).ToList())
            {
                AddServer(input, output, rows, server);
            }

            foreach (var server in output.Servers)
            {
                var worstPool = input.Pools.OrderBy(_ => _.Capacity).First();
                worstPool.AddServerToPool(server);
                server.PoolAssigned = worstPool.Index;
            }

            // SolverHelper.ImproveWorstPoolWorstRow(input, output);

            return output;
        }

        private static void AddServer(ProblemInput input, ProblemOutput output, List<Row> rows, Server server)
        {
            foreach (var row in rows.OrderBy(_ => _.Capacity))
            {
                int countEmpty = 0;
                for (int i = 0; i < input.RowSize; i++)
                {
                    if (input.Slots[row.Index, i])
                    {
                        countEmpty = 0;
                    }
                    else
                    {
                        countEmpty++;
                        if (countEmpty == server.Size)
                        {
                            row.Capacity += server.Capacity;
                            server.Row = row.Index;
                            server.SlotInRow = i - countEmpty;
                            output.Servers.Add(server);
                            return;
                        }
                    }
                }
            }
        }
    }

    public class Row : IndexedObject
    {
        public int Capacity { get; set; }
        public Row(int index) : base(index)
        {
        }
    }
}
