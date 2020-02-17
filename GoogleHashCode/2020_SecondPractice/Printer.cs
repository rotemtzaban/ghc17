using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_SecondPractice
{
    public class Printer : PrinterBase<ProblemOutput>
    {
        public override void PrintToConsole(ProblemOutput result)
        {
            //foreach (var server in result.Servers)
            //{
            //    if (server.Row == null)
            //    {
            //        Console.WriteLine("x");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{server.Index} {server.Row} {server.SlotInRow}");
            //    }
            //}
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                foreach (var server in result.Servers)
                {
                    if (server.Row == null)
                    {
                        writer.WriteLine("x");
                    }
                    else
                    {
                        writer.WriteLine($"{server.Index} {server.Row} {server.SlotInRow}");
                    }
                }
            }
        }
    }
}
