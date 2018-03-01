using System;
using System.Linq;
using HashCodeCommon;
using System.IO;

namespace _2018_Qualification
{
    public class Printer : PrinterBase<ProblemOutput>
    {
        public override void PrintToConsole(ProblemOutput result)
        {
            foreach (var item in result.Cars)
            {
                string s = item.RidesTaken.Count + " ";

                string.Join(" ", item.RidesTaken.Select(_ => _.Index).ToArray());
                Console.WriteLine(s);
            }
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                foreach (var item in result.Cars)
                {
                    string s = item.RidesTaken.Count + " ";

                    string.Join(" ", item.RidesTaken.Select(_ => _.Index).ToArray());
                    writer.Write(item);
                }
            }
        }
    }
}