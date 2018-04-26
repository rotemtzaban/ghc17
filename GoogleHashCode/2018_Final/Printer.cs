using System;
using HashCodeCommon;
using System.IO;

namespace _2018_Final
{
    public class Printer : PrinterBase<ProblemOutput>
    {
        public override void PrintToConsole(ProblemOutput result)
        {
            return;
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                // writer.WriteLine
            }
        }
    }
}