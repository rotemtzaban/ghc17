using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2015_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string firstLine = reader.ReadLine();
            string[] lineNumbers = firstLine.Split(' ');
            input.Rows = int.Parse(lineNumbers[0]);
            input.Columns = int.Parse(lineNumbers[1]);


            return input;
        }
    }
}
