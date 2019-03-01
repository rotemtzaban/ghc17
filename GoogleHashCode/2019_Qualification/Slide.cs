using System.Collections.Generic;
using System.Linq;

namespace _2019_Qualification
{
    public class Slide
    {
        public Slide(List<Photo> photos)
        {
            this.Images = photos;
        }

        int[] m_TI = null;
        string[] m_Tags = null;

        public List<Photo> Images { get; set; }

        public string[] Tags
        {
            get
            {
                if (m_Tags == null)
                {
                    m_Tags = Images.SelectMany(photo => photo.Tags).Distinct().ToArray();
                }
                return m_Tags;
            }
        }

        public int[] TagsIndexes
        {
            get
            {
                if (m_TI == null)
                {
                    m_TI= Images.SelectMany(photo => photo.TagIndexes).Distinct().ToArray();
                }
                return m_TI;
            }
        }
    }
}