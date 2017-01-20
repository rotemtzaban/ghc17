using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class DronesPrinter : PrinterBase<DronesOutput>
    {
        public override void PrintToConsole(DronesOutput result)
        {
			Console.WriteLine(result.Commands.Count);

			foreach (var command in result.Commands)
				Console.WriteLine(command.GetOutputLine());
		}

        public override void PrintToFile(DronesOutput result, string outputPath)
        {
			using (var writer = new StreamWriter(outputPath))
			{
				writer.WriteLine(result.Commands.Count);

				foreach (var command in result.Commands)
					writer.WriteLine(command.GetOutputLine());
			}
		}
    }
}
