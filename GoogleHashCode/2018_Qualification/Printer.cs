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
            return;
            //foreach (var item in result.Cars)
            //{
            //    string s = item.RidesTaken.Count + " ";
            //    var join = string.Join(" ", item.RidesTaken.Select(_ => _.Index).ToArray());
            //    Console.WriteLine(s + join);
            //}
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                foreach (var item in result.Cars)
                {
                    string s = item.RidesTaken.Count + " ";
                    var join = string.Join(" ", item.RidesTaken.Select(_ => _.Index).ToArray());
                    writer.WriteLine(s + join);
                }
            }
        }
    }
}