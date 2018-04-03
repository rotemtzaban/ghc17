using System;
using System.IO;
using HashCodeCommon;

namespace _2017_Final
{
    internal class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string[] firstLineSplited = reader.ReadLine().Split(' ');

            string[] secondLineSplited = reader.ReadLine().Split(' ');

            string[] thirdLineSplited = reader.ReadLine().Split(' ');

            return input;
        }
    }
}