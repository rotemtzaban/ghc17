using System;
using HashCodeCommon;
using System.IO;
using System.Linq;

namespace _2015_Qualification
{
	public class Printer : PrinterBase<ProblemOutput>
	{		
		private ConsoleColor[] printedColors = new ConsoleColor[]
		{
			ConsoleColor.Red,
			ConsoleColor.Yellow,
			ConsoleColor.Magenta,
			ConsoleColor.White,
			ConsoleColor.Cyan
		};

		public override void PrintToConsole (ProblemOutput result)
		{
            int freeSlots = 0;

            for (int y = 0; y < result.original_input.Rows; y++)
			{				
				for (int x = 0; x < result.original_input.Columns; x++)
				{
					if (result.original_input.UnavilableSlots.Contains(new Coordinate(x,y))) {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write('x');	
						continue;
					}

					var serversStartInCell = result._allocations.Values.Where (sa => sa.Row == y && sa.InitialColumn == x);
					int numOfCandidateServers = serversStartInCell.Count ();
					if (numOfCandidateServers == 0) {
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write ('.');
                        freeSlots++;

                        continue;
					}

					if (numOfCandidateServers > 1) {
						throw new InvalidProgramException("Found more than 1 server starting in the same cell!");					
					}

					var server = serversStartInCell.First ();
					Console.ForegroundColor = GetColor (server.Pool.Index);
					for (int i = 0; i < server.Server.Slots; i++) {
                        if (i !=0)
                            x++;
						Console.Write(server.Server.Index);	
					}
                    if (x >= result.original_input.Columns)
                    {
                        throw new Exception();
                    }						
				}

				Console.WriteLine ();
			}

            Console.WriteLine("free slots: " + freeSlots);
		}

		private ConsoleColor GetColor(int pool_index)
		{
			int index = pool_index % printedColors.Length;
			return printedColors[index];
		}

		public override void PrintToFile (ProblemOutput result, string outputPath)
		{
			// Fomrat: Server_row Server_slot(first idx) Pool_ID
			using (var writer = new StreamWriter(outputPath))
			{
				for (int i = 0; i < result.original_input.Servers.Count; i++)
				{
					var server = result.original_input.Servers[i];
					ServerAllocation allocation;
					if (result._allocations.TryGetValue(server, out allocation))
						writer.WriteLine(allocation.Row + " " + allocation.InitialColumn + " " + allocation.Pool.Index);
					else
						writer.WriteLine("x");
				}
			}
		}
	}
}

