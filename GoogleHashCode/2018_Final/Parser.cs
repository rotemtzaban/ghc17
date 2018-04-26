using System;
using System.IO;
using HashCodeCommon;

namespace _2018_Final
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            int[] firstLine = ReadLineAsIntArray(reader);

            return input;
        }
    }
}