using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_Qualification
{
    public class Printer : PrinterBase<ProblemOutput>
    {
        public override void PrintToConsole(ProblemOutput result)
        {
            foreach (var slide in result.Slides)
            {
                if (slide.Images.Count() == 1)
                {
                    Console.WriteLine(slide.Images[0]);
                }
                else
                {
                    Console.WriteLine($"{slide.Images[0]} {slide.Images[1]}");
                }
            }
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(result.Slides.Count);
                foreach (var slide in result.Slides)
                {
                    if (slide.Images.Count() == 1)
                    {
                        writer.WriteLine(slide.Images[0]);
                    }
                    else
                    {
                        writer.WriteLine($"{slide.Images[0]} {slide.Images[1]}");
                    }
                }
            }
        }
    }
}
