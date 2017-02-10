using System;
using HashCodeCommon;
using System.IO;
using System.Linq;

namespace _2015_Qualification
{
	public class Printer : PrinterBase<ProblemOutput>
	{
		public override void PrintToConsole (ProblemOutput result)
		{
			throw new NotImplementedException ();
		}

		public override void PrintToFile (ProblemOutput result, string outputPath)
		{
			// Fomrat: Server_row Server_slot(first idx) Pool_ID
			int key_index = 0;

			using (var writer = new StreamWriter(outputPath))
			{
				foreach (var server_allocated in result._allocations.OrderBy (kvp => kvp.Key.Index)) {
					if (key_index != server_allocated.Key.Index) { // not found in dictionary == it was not allocated
						writer.WriteLine ("x");
					}						
					else {
						writer.WriteLine (server_allocated.Value.Row + " " + server_allocated.Value.InitialColumn + " " + server_allocated.Value.Pool.Index);
					}

					key_index++; // inc for next index test.
				}					
			}

		}
	}
}

