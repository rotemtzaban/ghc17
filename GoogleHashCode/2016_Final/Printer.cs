using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Final
{
    public class Printer : PrinterBase<ProblemOutput>
    {
        public override void PrintToConsole(ProblemOutput result)
        {
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(result.ImagesTaken.Count);
                foreach (var imageTaken in result.ImagesTaken)
                {
                    writer.WriteLine($"{imageTaken.Latitude} {imageTaken.Longitude} {imageTaken.TurnTaken} {imageTaken.SatelliteId}");
                }
            }
        }
    }
}
