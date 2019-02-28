using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2019_Qualification
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            //ProblemOutput res = new ProblemOutput();
            //res.Slides = new List<Slide>();
            //res.Slides.Add(new Slide(new List<Photo>() { new Photo(0, false, null) }));
            //res.Slides.Add(new Slide(new List<Photo>() { new Photo(3, false, null) }));
            //res.Slides.Add(new Slide(new List<Photo>() { new Photo(1, true, null), new Photo(2, true, null) }));

            //return res;

            return Solve2(input);
        }

        protected ProblemOutput Solve2(ProblemInput input)
        {
         //   HashSet<string> tags = new HashSet<string>();
            Dictionary<string, List<Photo>> tagToImages = new Dictionary<string, List<Photo>>();
            foreach (var image in input.Photos)
            {
                foreach (var tag in image.Tags)
                {
                    if (tagToImages.ContainsKey(tag))
                    {
                        tagToImages[tag].Add(image);
                    }
                    else
                    {
                        tagToImages.Add(tag, new List<Photo>(){image});
                    }
                }
            }


            ProblemOutput res = new ProblemOutput();
            res.Slides = new List<Slide>();
            HashSet<int> takenPhotos = new HashSet<int>();

            int count = 1;
            int index = 1;
            int lastTaken = 0;

            var first = GetFirstSlide(takenPhotos, input);
            if (first == null)
            {
                return res;
            }
            res.Slides.Add(new Slide(new List<Photo>() { first }));
            takenPhotos.Add(first.Index);

            while (count < input.Photos.Length && index < input.Photos.Length)
            {
                count++;
                var photo = res.Slides[res.Slides.Count - 1].Images[0];
                Photo nextSlide = GetNextSlide(tagToImages, takenPhotos, input, photo);
                if (nextSlide == null)
                {
                    Photo random = GetFirstSlide(takenPhotos, input);
                    if (random == null)
                    {
                        break;
                    }

                    lastTaken = random.Index;
                    takenPhotos.Add(random.Index);
                    res.Slides.Add(new Slide(new List<Photo>() { random }));
                }
                else
                {
                    lastTaken = nextSlide.Index;
                    takenPhotos.Add(nextSlide.Index);
                    res.Slides.Add(new Slide(new List<Photo>() { nextSlide }));
                }

                if (count % 500 == 0)
                {
                    Console.WriteLine($"we are in: {count}");
                    Console.WriteLine($"lastTaken: {lastTaken}");
                }

                if (count == 2000)
                {
                    return res;
                }
            }

            return res;
        }

        private Photo GetFirstSlide(HashSet<int> takenPhotos, ProblemInput input)
        {
            for (int i = 0; i < input.Photos.Length; i++)
            {
                if (input.Photos[i].IsVertical)
                {
                    continue;
                }

                if (!takenPhotos.Contains(input.Photos[i].Index))
                {
                    return input.Photos[i];
                }
            }

            return null;
        }

        private Photo GetNextSlide(Dictionary<string, List<Photo>> tagToImages ,HashSet<int> takenPhotos, ProblemInput input, Photo firstPhoto)
        {
            foreach (var tag in firstPhoto.Tags)
            {
                if(tagToImages[tag].Count == 1) continue;

                foreach (var optionalImage in tagToImages[tag])
                {
                    if (optionalImage.IsVertical)
                    {
                        continue;
                    }
                    if (!takenPhotos.Contains(optionalImage.Index))
                    {
                        if (MoreThanX(firstPhoto, optionalImage))
                        {
                            takenPhotos.Add(optionalImage.Index);
                            return optionalImage;
                        }
                    }
                }
            }
            return null;
        }

        private bool MoreThanX(Photo firstPhoto, Photo secondPhoto, int threshold = 2)
        {
            var intersectTags = firstPhoto.Tags.Intersect(secondPhoto.Tags);

            var score = Calcutaor.CalculatePhotosScore(firstPhoto, secondPhoto);
            return score >= threshold;
        }
    }
}
