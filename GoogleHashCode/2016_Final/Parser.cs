using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HashCodeCommon.HelperClasses;

namespace _2016_Final
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            var satCount = reader.GetInt();
            Satallite[] satallites = new Satallite[satCount];
            for (int i = 0; i < satCount; i++)
            {
                var longList = reader.GetLongList();
                satallites[i] = new Satallite(i, longList[0], longList[1], longList[2], longList[3], longList[4]);
            }


        }
    }
}
