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
    public class Calcutaor : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public static int CalculatePhotosScore(Photo firstPhotos, Photo secPhoto)
        {
            var firstTags = firstPhotos.Tags.ToArray();
            var secondTags = secPhoto.Tags.ToArray();

            var score = Math.Min(Math.Min(firstTags.Intersect(secondTags).Count(), secondTags.Except(firstTags).Count()),
                firstTags.Except(secondTags).Count());

            return score;
        }
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            long sum = 0;
            for (int i = 0; i < output.Slides.Count - 1; i++)
            {
                var first = output.Slides[i];
                var second = output.Slides[i + 1];

                var firstTags = first.Tags.ToArray();
                var secondTags = second.Tags.ToArray();

                var score = Math.Min(Math.Min(firstTags.Intersect(secondTags).Count(), secondTags.Except(firstTags).Count()),
                    firstTags.Except(secondTags).Count());

                sum += score;
            }

            return sum;
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            var slideCount = reader.GetInt();
            var slides = new Slide[slideCount];
            for (int i = 0; i < slideCount; i++)
            {
                List<Photo> slide = new List<Photo>();
                var intList = reader.GetIntList();
                if (intList.Count == 1)
                {
                    var x = input.Photos[intList[0]];
                    if (x.IsVertical)
                    {
                        throw new Exception("vertical can't be alone");
                    }

                    slide.Add(x);
                }
                else if (intList.Count == 2)
                {
                    var x = input.Photos[intList[0]];
                    var y = input.Photos[intList[1]];

                    if (!(x.IsVertical && y.IsVertical))
                    {
                        throw new Exception("horizontal must be alone");
                    }

                    slide.Add(x);
                    slide.Add(y);
                }

                slides[i] = new Slide(slide);
            }

            return new ProblemOutput { Slides = slides.ToList() };
        }
    }
}
