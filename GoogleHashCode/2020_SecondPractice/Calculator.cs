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
            foreach (var server in output.Servers)
            {
                input.Pools[server.PoolAssigned.Value].AddServerToPool(server);
            }

            foreach (var item in input.Pools.OrderBy(_ => _.GuaranteedCapacity))
            {
                Console.WriteLine($"{item.Index} : {item.Capacity}, {item.GuaranteedCapacity}, { item.WorstRow},{ item.RowsCapacity[item.WorstRow]}, {output.Servers.Count(_ => _.PoolAssigned == item.Index && _.Row == item.WorstRow)}, {item.BestRow}, {item.RowsCapacity[ item.BestRow]}, {output.Servers.Count(_ => _.PoolAssigned == item.Index && _.Row == item.BestRow)} ");
            }

            return input.Pools.Min(_ => _.GuaranteedCapacity);
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
