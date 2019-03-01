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
        protected ProblemOutput SolveConst(ProblemInput input)
        {
            //ProblemOutput res = new ProblemOutput();
            //res.Slides = new List<Slide>();
            //res.Slides.Add(new Slide(new List<Photo>() { new Photo(0, false, null) }));
            //res.Slides.Add(new Slide(new List<Photo>() { new Photo(3, false, null) }));
            //res.Slides.Add(new Slide(new List<Photo>() { new Photo(1, true, null), new Photo(2, true, null) }));

            //return res;

            return Solve2(input);
        }

        protected override ProblemOutput Solve(ProblemInput input)
        {
            var dictionary = input.Photos.GroupBy(photo => photo.IsVertical).ToDictionary(photos => photos.Key);
            IEnumerable<Photo> vertical = dictionary.ContainsKey(true) ? dictionary[true] : (IEnumerable<Photo>)Array.Empty<Photo>();
            IEnumerable<Photo> horizontal = dictionary.ContainsKey(false) ? dictionary[false] : (IEnumerable<Photo>)Array.Empty<Photo>();

            var slides = horizontal.Select(photo => new Slide(new List<Photo> { photo }))
                .Concat(VerticalUnifier.GetUnified(vertical.ToList(), NumbersGenerator)).ToList();
            ProblemOutput res = new ProblemOutput();
            res.Slides = new List<Slide>();

            HashSet<int> notPaired = new HashSet<int>(Enumerable.Range(0, slides.Count));
            //List<int> notPairedList = new List<int>(input.Photos.Select(x => x.Index).ToList());

            var last = NumbersGenerator.Next(slides.Count);
            notPaired.Remove(last);
            res.Slides.Add(slides[last]);

            for (int i = 0; i < slides.Count - 1; i++)
            {
                long bestScore = 0;
                int pairId = 0;

                for (int j = 0; j < 10; j++)
                {
                    int nextId = this.NumbersGenerator.Next(0, slides.Count);

                    while (!notPaired.Contains(nextId) || nextId == last)
                    {
                        nextId = this.NumbersGenerator.Next(0, slides.Count);
                    }

                    long myScore = Calcutaor.CalculatePhotosScore(slides[last], slides[nextId]);

                    if (bestScore < myScore || pairId == 0)
                    {
                        bestScore = myScore;
                        pairId = nextId;
                    }
                }

                last = pairId;
                notPaired.Remove(pairId);
                res.Slides.Add(slides[pairId]);
            }

            return res;
            // TODO: add consideration for (1) vertical slides, (2) order between pairs.
        }

        protected ProblemOutput Solve2(ProblemInput input)
        {
            var dictionary = input.Photos.GroupBy(photo => photo.IsVertical).ToDictionary(photos => photos.Key);
            IEnumerable<Photo> vertical = dictionary.ContainsKey(true) ? dictionary[true] : (IEnumerable<Photo>)Array.Empty<Photo>();
            IEnumerable<Photo> horizontal = dictionary.ContainsKey(false) ? dictionary[false] : (IEnumerable<Photo>)Array.Empty<Photo>();

            var slides = horizontal.Select(photo => new Slide(new List<Photo> { photo }))
                .Concat(VerticalUnifier.GetUnified(vertical.ToList(), NumbersGenerator)).ToList();
            Dictionary<string, List<Slide>> tagToImages = new Dictionary<string, List<Slide>>();
            // HashSet<string> tags = new HashSet<string>();
            foreach (var image in slides)
            {
                foreach (var tag in image.Tags)
                {
                    //  tags.Add(tag);
                    if (tagToImages.ContainsKey(tag))
                    {
                        tagToImages[tag].Add(image);
                    }
                    else
                    {
                        tagToImages.Add(tag, new List<Slide>() { image });
                    }
                }
            }

            ProblemOutput res = new ProblemOutput();
            res.Slides = new List<Slide>();
            HashSet<int> takenPhotos = new HashSet<int>();

            int count = 1;
            int index = 1;
            int lastTaken = 0;

            var first = GetFirstSlide(takenPhotos, slides);
            res.Slides.Add(first);
            takenPhotos.Add(first.Images[0].Index);
            if (first.Images.Count == 2)
            {
                takenPhotos.Add(first.Images[1].Index);
            }

            while (count < input.Photos.Length && index < slides.Count)
            {
                count++;
                var photo = res.Slides[res.Slides.Count - 1];
                Slide nextSlide = GetNextSlide(tagToImages, takenPhotos, slides, photo);
                if (nextSlide == null)
                {
                    var randomSlide = GetFirstSlide(takenPhotos, slides);
                    if (randomSlide == null)
                    {
                        break;
                    }
                    takenPhotos.Add(randomSlide.Images[0].Index);
                    if (randomSlide.Images.Count == 2)
                    {
                        takenPhotos.Add(randomSlide.Images[1].Index);
                    }
                    res.Slides.Add(randomSlide);
                }
                else
                {
                    takenPhotos.Add(nextSlide.Images[0].Index);
                    if (nextSlide.Images.Count == 2)
                    {
                        takenPhotos.Add(nextSlide.Images[1].Index);
                    }
                    res.Slides.Add(nextSlide);
                }
            }

            return res;
        }

        private Slide GetFirstSlide(HashSet<int> takenPhotos, List<Slide> input)
        {
            for (int i = input.Count - 1; i >= 0; i--)
            {
                var slide = input[i];
                bool isTaken = false;
                if (slide.Images.Count == 1)
                {
                    isTaken = takenPhotos.Contains(slide.Images[0].Index);
                }
                else
                {
                    isTaken = takenPhotos.Contains(slide.Images[0].Index) || takenPhotos.Contains(slide.Images[1].Index);
                }

                if (!isTaken)
                {
                    return slide;
                }
            }

            return null;
        }

        private Slide GetNextSlide(Dictionary<string, List<Slide>> tagToImages, HashSet<int> takenPhotos, List<Slide> input, Slide firstPhoto)
        {
            int maxScore = 0;
            int threshold = 4;
            Slide nex = null;
            foreach (var tag in firstPhoto.Tags)
            {
                if (tagToImages[tag].Count == 1) continue;

                foreach (var optionalImage in tagToImages[tag])
                {
                    bool isTaken = false;
                    if (optionalImage.Images.Count == 1)
                    {
                        isTaken = takenPhotos.Contains(optionalImage.Images[0].Index);
                    }
                    else
                    {
                        isTaken = takenPhotos.Contains(optionalImage.Images[0].Index) || takenPhotos.Contains(optionalImage.Images[1].Index);
                    }
                    if (!isTaken)
                    {
                        var score = Calcutaor.CalculatePhotosScore(firstPhoto, optionalImage);
                        if (score >= maxScore)
                        {
                            maxScore = score;
                            nex = optionalImage;
                            if (maxScore > threshold)
                            {
                                return nex;
                            }
                        }
                    }
                }
            }

            return nex;
        }
    }
}
}
