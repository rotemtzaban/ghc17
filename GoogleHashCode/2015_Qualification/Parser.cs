using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2015_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string firstLine = reader.ReadLine();
            string[] lineNumbers = firstLine.Split(' ');
            input.Rows = int.Parse(lineNumbers[0]);
            input.Columns = int.Parse(lineNumbers[1]);
            int numOfUnavliableSlots = int.Parse(lineNumbers[2]);

            input.UnavilableSlots = new List<Coordinate>();
            for (int i = 0; i < numOfUnavliableSlots; i++)
            {
                Coordinate coord = ReadLineAsCoordinate(reader);
                input.UnavilableSlots.Add(coord);
            }

            int numOfPools = int.Parse(lineNumbers[3]);
            input.Pools = new List<Pool>();
            for (int i = 0; i < numOfPools; i++)
            {
                input.Pools.Add(new Pool(i));
            }

                int numOServers = int.Parse(lineNumbers[4]);
            input.Servers = new List<Server>();
            for (int i = 0; i < numOServers; i++)
            {
                Coordinate coord = ReadLineAsCoordinate(reader);
                Server server = new Server(i, coord.X, coord.Y);
                input.Servers.Add(server);
            }

            return input;
        }
    }
}
