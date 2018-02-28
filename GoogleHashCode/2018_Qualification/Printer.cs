using System;
using HashCodeCommon;
using System.IO;

namespace _2018_Qualification
{
    public class Printer : PrinterBase<ProblemOutput>
    {
        public override void PrintToConsole(ProblemOutput result)
        {
            throw new NotImplementedException();
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                // writer.WriteLine()
                throw new NotImplementedException();
            }
        }
    }
}