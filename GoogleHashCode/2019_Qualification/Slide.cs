using System.Collections.Generic;
using System.Linq;

namespace _2019_Qualification
{
    public class Slide
    {
        public Slide(List<Photo> photos)
        {
            this.Images = photos;
            Tags = Images.SelectMany(photo => photo.Tags).Distinct().ToArray();
        }

        public List<Photo> Images { get; set; }

        public string[] Tags { get; }
    }
}