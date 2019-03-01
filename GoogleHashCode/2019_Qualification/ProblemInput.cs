using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodeCommon;

namespace _2019_Qualification
{
    public class ProblemInput
    {
        public Photo[] Photos { get; set; }

        public int TagCount { get; set; }
    }

    public class Photo : IndexedObject
    {
        public Photo(int index, bool isVertical, string[] tags) : base(index)
        {
            IsVertical = isVertical;
            Tags = tags;
        }

        public bool IsVertical { get; }

        public string[] Tags { get; }

        public int[] TagIndexes { get; set; }
    }
}
