using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2016_Final.Properties;

namespace _2016_Final
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            parser.ParseFromData(Resources.constellation);
            parser.ParseFromData(Resources.forever_alone);
            parser.ParseFromData(Resources.overlap);
            parser.ParseFromData(Resources.weekend);

            Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>("2016", new Parser(), new Solver(), new Printer());
            ZipCreator.CreateCodeZip("2016");
            // TODO: input file
            //runner1.Run(Properties.Resources.MeAtTheZoo, "MeAtTheZoo", 1, true);            

            ZipCreator.CreateCodeZip("2016");
            Console.ReadKey();
        }
    }
}
