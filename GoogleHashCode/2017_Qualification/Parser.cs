using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2017_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string[] firstLineSplited = reader.ReadLine().Split(' ');
            input.Rows = int.Parse(firstLineSplited[0]);
            input.Columns = int.Parse(firstLineSplited[1]);

            int someCount = int.Parse(firstLineSplited[2]);
            for (int i = 0; i < someCount; i++)
            {
                string[] lineSplitted = reader.ReadLine().Split(' ');
                int x = int.Parse(lineSplitted[0]);
            }

            return input;
        }
    }
}
