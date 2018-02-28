using System;
using System.IO;
using HashCodeCommon;

namespace _2018_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string[] firstLineSplited = reader.ReadLine().Split(' ');

            int num = int.Parse(firstLineSplited[0]);

            // input.PropList = new PropList()
            for (int i = 0; i < num; i++)
            {
                // input.PropList.Add(new Item(i))
            }

            return input;
        }
    }
}