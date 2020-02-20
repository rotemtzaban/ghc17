using HashCodeCommon;
using HashCodeCommon.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            var firstRow = reader.GetIntList();
            input.NumberOfBooks = firstRow[0];
            input.NumberOfLibraries = firstRow[1];
            input.NumberOfDays = firstRow[2];

            var secondRow = reader.GetIntList();
            foreach (var item in secondRow)
            {

            }

            return input;
        }
    }
}
