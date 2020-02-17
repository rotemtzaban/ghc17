using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_SecondPractice
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string[] firstLine = reader.ReadLine().Split(' ');
            input.NumOfRows = int.Parse(firstLine[0]);
            input.RowSize = int.Parse(firstLine[1]);
            input.NumOfUnavliableSlots = int.Parse(firstLine[2]);
            input.NumOfPools = int.Parse(firstLine[3]);
            input.NumOfServers = int.Parse(firstLine[4]);

            input.Slots = new bool[input.NumOfRows, input.RowSize];

            for (int i = 0; i < input.NumOfUnavliableSlots; i++)
            {
                string[] line = reader.ReadLine().Split(' ');
                input.Slots[int.Parse(line[0]), int.Parse(line[1])] = true;
            }

            input.Servers = new List<Server>();
            for (int i = 0; i < input.NumOfServers; i++)
            {
                string[] line = reader.ReadLine().Split(' ');
                Server server = new Server(i);
                server.Size = int.Parse(line[0]);
                server.Capacity = int.Parse(line[1]);
                input.Servers.Add(server);
            }

            input.Pools = new List<PoolDetails>();
            for (int i = 0; i < input.NumOfPools; i++)
            {
                input.Pools.Add(new PoolDetails(i, input.NumOfRows));
            }
            //var v = input.Servers.GroupBy(_ => _.Size).ToList();
            //foreach (var item in v.OrderBy(_ => _.Key))
            //{
            //    foreach (var item2 in item.GroupBy(_ => _.Capacity).OrderBy( _ => _.Key))
            //    {
            //        Console.WriteLine(item.Key + "," + item2.Key + "," + item2.Count());
            //    }
            //}

            return input;
        }
    }
}
