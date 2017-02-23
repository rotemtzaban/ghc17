using System;
using HashCodeCommon;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace _2015_Qualification
{
	public class ScoreCalculator : ScoreCalculatorBase<ProblemInput, ProblemOutput>
	{
		public ScoreCalculator()
		{
		}

		public override long Calculate (ProblemInput input, ProblemOutput output)
		{
			// calculate gc= gauaranteed capacity
			int[] gc_pool = new int[input.Pools.Count];

			int[,] capacity_sum_row = new int[input.Pools.Count, input.Rows];
				
			for (int x=0;x<input.Pools.Count;x++) {
				for (int y=0;y<input.Rows;y++) {
					capacity_sum_row[x,y] = 0;
				}
			}

			foreach (var kvp in output._allocations)
			{
				capacity_sum_row[kvp.Value.Pool.Index, kvp.Value.Row] += kvp.Value.Server.Capacity;
			}

			for (int i = 0; i < input.Pools.Count; i++) {
				int maxRowSum = 0;
				int totalSum = 0;
				for (int j = 0; j < input.Rows; j++) {
					totalSum += capacity_sum_row [i, j];
					if (maxRowSum < capacity_sum_row [i, j]) {
						maxRowSum = capacity_sum_row [i, j];
					}
				}

				gc_pool [i] = totalSum - maxRowSum;
			}

            foreach (var item in gc_pool.OrderBy(_ => _))
            {
                // Console.WriteLine(item);
            }

            return gc_pool.Min ();
		}

		public override ProblemOutput GetResultFromReader (ProblemInput input, TextReader reader)
		{
            ProblemOutput output = new ProblemOutput();
            output._allocations = new Dictionary<Server, ServerAllocation>();

            for (int i = 0; i < input.Servers.Count; i++)
            {
                string[] line = reader.ReadLine().Split(' ');
	            if (line[0] == "x")
		            continue;
                int row = int.Parse(line[0]);
                int slot = int.Parse(line[1]);
                int pool = int.Parse(line[2]);

                Server current = input.Servers[i];

				for (int j=0;j<current.Slots;j++) {
					if (input.UnavilableSlots.Contains(new Coordinate(slot+j,row)))
					{
						throw new InvalidProgramException("Validation failed, server placed in unavailable slot");
					}
				}
				// Dictionary will validate that server is not used twice
				// Validation for server not in same spot in the printer to console code 

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

