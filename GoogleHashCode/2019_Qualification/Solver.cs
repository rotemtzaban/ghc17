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
            var vertical = dictionary[true];
            var horizontal = dictionary[false];

            var slides = horizontal.Select(photo => new Slide(new List<Photo> {photo}))
                .Concat(VerticalUnifier.GetUnified(vertical.ToList(), NumbersGenerator));
            ProblemOutput res = new ProblemOutput();
            res.Slides = new List<Slide>();

            HashSet<int> notPaired = new HashSet<int>(input.Photos.Select(x => x.Index).ToList());
            List<int> notPairedList = new List<int>(input.Photos.Select(x => x.Index).ToList());

            // TODO: Go over all slides instead of photots and find optimal pair for each slide 'greedily'
            for (int i = 0; i < input.Photos.Length; i++)
            {
                if (!notPaired.Contains(i))
                {
                    continue;
                }

                long bestScore = -1;
                int pairId = -1;

                for (int j = 0; j < 100;)
                {
                    int nextId = this.NumbersGenerator.Next(0, notPairedList.Count);

                    if (!notPaired.Contains(notPairedList[nextId]) || i == nextId)
                    {
                        continue;
                    }

                    long myScore = Calcutaor.CalculatePhotosScore(input.Photos[i], input.Photos[notPairedList[nextId]]);

                    if (bestScore < myScore)
                    {
                        bestScore = myScore;
                        pairId = notPairedList[nextId];
                    }

                    j++;
                }

                notPaired.Remove(i);
                notPaired.Remove(pairId);
                notPairedList.Remove(i);
                notPairedList.Remove(pairId);

                res.Slides.Add(new Slide(new List<Photo>() { new Photo(i, false, null) }));
                res.Slides.Add(new Slide(new List<Photo>() { new Photo(pairId, false, null) }));
            }

            return res;
            // TODO: add consideration for (1) vertical slides, (2) order between pairs.
        }

        protected ProblemOutput Solve2(ProblemInput input)
        {
            ProblemOutput res = new ProblemOutput();
            res.Slides = new List<Slide>();
            HashSet<int> takenPhotos = new HashSet<int>();

            int count = 1;
            int index = 1;
            int lastTaken = 0;
            res.Slides.Add(new Slide(new List<Photo>() { input.Photos[0]}));
            takenPhotos.Add(input.Photos[0].Index);
            while (count < input.Photos.Length && index < input.Photos.Length)
            {
                count++;
                var photo = res.Slides[res.Slides.Count - 1].Images[0];
                Photo nextSlide = GetNextSlide(takenPhotos, input, photo);
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
            }

            return res;
        }

        private Photo GetFirstSlide(HashSet<int> takenPhotos, ProblemInput input)
        {
            for (int i = 0; i < input.Photos.Length; i++)
            {
                if (!takenPhotos.Contains(input.Photos[i].Index))
                {
                    return input.Photos[i];
                }
            }

            return null;
        }

        private Photo GetNextSlide(HashSet<int> takenPhotos, ProblemInput input, Photo firstPhoto)
        {
            foreach (var photo in input.Photos)
            {
                if (photo.IsVertical)
                {
                    continue;
                }
                if (!takenPhotos.Contains(photo.Index))
                {
                    if (MoreThanX(firstPhoto, photo))
                    {
                        takenPhotos.Add(photo.Index);
                        return photo;
                    }
                }
            }

            return null;
        }

        private bool MoreThanX(Photo firstPhoto, Photo secondPhoto, int threshold = 1)
        {
            var intersectTags = firstPhoto.Tags.Intersect(secondPhoto.Tags);

            var score = Calcutaor.CalculatePhotosScore(firstPhoto, secondPhoto);
            return score >= threshold;
        }
    }
}
