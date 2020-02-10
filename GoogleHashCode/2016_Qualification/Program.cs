using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Qualification
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new Runner<ProblemInput, ProblemOutput>("2016_Qualification", new Parser(), new Solver(), new Printer(), new Calculator());
            runner.Run("", "");

        }
    }
}
