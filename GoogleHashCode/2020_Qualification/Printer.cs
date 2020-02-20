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
                var ordersBooks = result.libaries.OrderBy(_ => _.LibaryStartSignUpTime);
                writer.WriteLine(result.libaries.Count);
                foreach (var library in ordersBooks)
                {
                    if(!library.SelectedBooks.Any()) continue;
                    writer.WriteLine($"{library.Index} { library.SelectedBooks.Count()}");
                    var books = library.SelectedBooks.Select(_ => _.Index.ToString());
                    var output = string.Join(" ", books);
                    writer.WriteLine(output);
                }
            }
        }
    }
}
