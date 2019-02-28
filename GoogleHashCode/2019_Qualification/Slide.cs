using System.Collections.Generic;
using System.Linq;

namespace _2019_Qualification
{
    public class Slide
    {
        public List<Photo> Images { get; set; }

        public IEnumerable<string> Tags
        {
            get
            {
                return this.Images.SelectMany(_ => _.Tags).Distinct();
            }
        }
    }
}