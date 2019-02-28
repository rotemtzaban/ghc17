using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodeCommon.HelperClasses;

namespace _2019_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            var count = reader.GetInt();
            var photos = new Photo[count];
            for (int i = 0; i < count; i++)
            {
                var stringList = reader.GetStringList();

                var isVertical = stringList[0] == "V";

                var tagCount = int.Parse(stringList[1]);

                var tags = stringList.Skip(2).ToArray();
                photos[i] = new Photo(i, isVertical, tags);
            }

            return new ProblemInput { Photos = photos };
        }
    }
}
