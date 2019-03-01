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


            IDictionary<string, int> allTags = new Dictionary<string, int>();

            for (int i = 0; i < count; i++)
            {
                var stringList = reader.GetStringList();

                var isVertical = stringList[0] == "V";

                var tagCount = int.Parse(stringList[1]);

                var tags = stringList.Skip(2).ToArray();

                photos[i] = new Photo(i, isVertical, tags);
                photos[i].TagIndexes = new int[tags.Length];
                for (int k = 0; k < tags.Length; k++)
                {
                    photos[i].TagIndexes[k] = GetTagIndex(tags[k], allTags);
                }
            }

            return new ProblemInput { Photos = photos, TagCount = allTags.Count };
        }

        private int GetTagIndex(string tag, IDictionary<string, int> allTags)
        {
            int index;
            if (allTags.TryGetValue(tag, out index))
            {
                return index;
            }
            allTags[tag] = allTags.Count;
            return allTags.Count - 1;
        }
    }
}
