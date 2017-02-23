using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
	public class Printer : PrinterBase<ProblemOutput>
	{
		public override void PrintToConsole(ProblemOutput result)
		{
			Console.WriteLine(result.ServerAssignments.Count);

			foreach (var serverAssignment in result.ServerAssignments.OrderBy(kvp => kvp.Key.Index))
			{
				Console.Write(serverAssignment.Key.Index);
				foreach (var video in serverAssignment.Value.OrderBy(v => v.Index))
				{
					Console.Write(" " + video.Index);
				}

				Console.WriteLine();
			}
		}

		public override void PrintToFile(ProblemOutput result, string outputPath)
		{
			// Fomrat: N (number of cache server to follow)
			// c (cache_id) v0 v1 ... vN (videos stored)
			using (var writer = new StreamWriter(outputPath))
			{
				writer.WriteLine(result.ServerAssignments.Count);

				foreach (var serverAssignment in result.ServerAssignments.OrderBy(kvp => kvp.Key.Index))
				{
					writer.Write(serverAssignment.Key.Index);
					foreach (var video in serverAssignment.Value.OrderBy(v => v.Index))
					{
						writer.Write(" " + video.Index);
					}

					writer.WriteLine();
				}
			}
		}
	}
}
