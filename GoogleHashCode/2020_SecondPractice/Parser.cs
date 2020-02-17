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
            input.UnavliableSlots = int.Parse(firstLine[2]);
            input.NumOfPools = int.Parse(firstLine[3]);
            input.NumOfServers = int.Parse(firstLine[4]);

            return input;
        }
    }
}
