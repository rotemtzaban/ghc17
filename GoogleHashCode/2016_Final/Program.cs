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
            Runner<ProblemInput, ProblemOutput> runner1 = new Runner<ProblemInput, ProblemOutput>("2016", new Parser(), new Solver(), new Printer(),new Scorer());
            runner1.Run("constellation", Resources.weekend);
            runner1.Run("constellation", Resources.forever_alone);
            runner1.Run("constellation", Resources.constellation);
            runner1.Run("constellation", Resources.overlap);
            ZipCreator.CreateCodeZip("2016");
            // TODO: input file
            //runner1.Run(Properties.Resources.MeAtTheZoo, "MeAtTheZoo", 1, true);            

            ZipCreator.CreateCodeZip("2016");
            Console.ReadKey();
        }
    }
}
