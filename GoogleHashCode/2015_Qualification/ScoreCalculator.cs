using System;
using HashCodeCommon;
using System.IO;
using System.Collections.Generic;

namespace _2015_Qualification
{
	public class ScoreCalculator : ScoreCalculatorBase<ProblemInput, ProblemOutput>
	{
		public ScoreCalculator()
		{
		}

		public override int Calculate (ProblemInput input, ProblemOutput output)
		{
            return 0;
		}

		public override ProblemOutput GetResultFromReader (ProblemInput input, TextReader reader)
		{
            ProblemOutput output = new ProblemOutput();
            output._allocations = new Dictionary<Server, ServerAllocation>();

            for (int i = 0; i < input.Servers.Count; i++)
            {
                string[] line = reader.ReadLine().Split(' ');
                int row = int.Parse(line[0]);
                int slot = int.Parse(line[1]);
                int pool = int.Parse(line[2]);

                Server current = input.Servers[i];
                ServerAllocation alooc = new ServerAllocation();
                alooc.Row = row;
                alooc.Pool = input.Pools[pool];
                alooc.Server = current;
                alooc.InitialColumn = slot;

                output._allocations.Add(current, alooc);
            }

            return output;
		}
	}
}

