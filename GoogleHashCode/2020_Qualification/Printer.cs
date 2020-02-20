using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Qualification
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
                var ordersBooks = result.libaries.OrderBy(_ => _.LibrarySignupTime);
                writer.WriteLine(result.libaries.Count);
                foreach (var libary in ordersBooks)
                {

                    //writer.WriteLine(libary.IndexObject {libary.books.Count()});
                    var books = libary.books.Select(_ => _.Index.ToString());
                    var output = string.Join(" ", books);
                    writer.WriteLine(output);
                }

                //foreach (var server in result.Servers)
                //{
                //    if (server.Row == null)
                //    {
                //        writer.WriteLine("x");
                //    }
                //    else
                //    {
                //        writer.WriteLine($"{server.Row.Value} {server.SlotInRow} {server.PoolAssigned.Value}");
                //    }
                //}
            }
        }
    }
}
