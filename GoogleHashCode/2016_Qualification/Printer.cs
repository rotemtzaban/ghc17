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
            return;
            Console.WriteLine(result.Count);
            foreach (var command in result)
            {
                    Console.WriteLine(command);
            }
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(result.Count);
                foreach (var command in result)
                {
                    writer.WriteLine(command);
                }
            }

        }
    }
}