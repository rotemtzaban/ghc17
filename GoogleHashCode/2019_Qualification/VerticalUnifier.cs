using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_Qualification
{
    public class VerticalUnifier
    {
        public static List<Slide> GetUnified(List<Photo> vertical, Random random)
        {
            HashSet<Photo> notPaired = new HashSet<Photo>(vertical);
            List<Slide> slides = new List<Slide>();
            for (int i = 0; i < vertical.Count / 2; i++)
            {
                var first = notPaired.First();
                var maxScore = 0;
                Photo maxIndex = null;
                for (int j = 0; j < 100; j++)
                {
                    var randomTry = random.Next(0, vertical.Count);

                    var second = vertical[randomTry];

                    while (!notPaired.Contains(second) || second.Equals(first))
                    {
                        randomTry = random.Next(0, vertical.Count);
                        second = vertical[randomTry];
                    }

                    var count = vertical[0].Tags.Union(vertical[randomTry].Tags).Count();

                    if (count > maxScore || maxIndex == null)
                    {
                        maxScore = count;
                        maxIndex = second;
                    }
                }

                slides.Add(new Slide(new List<Photo> { first, maxIndex }));

                notPaired.Remove(first);
                notPaired.Remove(maxIndex);
            }

            return slides;
        }
    }
}
