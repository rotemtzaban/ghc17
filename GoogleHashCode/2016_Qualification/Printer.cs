using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Qualification
{
    public class Printer : PrinterBase<ProblemOutput>
    {
        public override void PrintToConsole(ProblemOutput result)
        {
            var numberOfCommands = result.Sum(_ => _.Commands.Count());
            Console.WriteLine(numberOfCommands);

            foreach (var drown in result)
            {
                foreach (var command in drown.Commands)
                {
                    Console.WriteLine(command);
                }
            }
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                var numberOfCommands = result.Sum(_ => _.Commands.Count());
                writer.WriteLine(numberOfCommands);

                foreach (var drown in result)
                {
                    foreach (var command in drown.Commands)
                    {
                        writer.WriteLine(command);
                    }
                }
            }

        }
    }
}