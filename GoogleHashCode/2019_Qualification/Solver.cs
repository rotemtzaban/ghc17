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
            ProblemOutput res = new ProblemOutput();
            res.Slides = new List<Slide>();

            HashSet<long> visited = new HashSet<long>();

            // TODO: Go over all slides instead of photots and find optimal pair for each slide 'greedily'
            for (int i = 0; i < input.Photos.Length; i++)
            {
                if (visited.Contains(i))
                {
                    continue;
                }

                long bestScore = 0;
                int pairId = -1;

                for (int j = 0; j < 100; j++)
                {
                    int nextId = this.NumbersGenerator.Next(i+1, input.Photos.Length-1);

                    if (visited.Contains(nextId) || i == nextId)
                    {
                        continue;
                    }

                    long myScore = Calcutaor.CalculatePhotosScore(input.Photos[i], input.Photos[nextId]);

                    if (bestScore < myScore)
                    {
                        bestScore = myScore;
                        pairId = nextId;
                    }
                }

                visited.Add(i);
                visited.Add(pairId);

                res.Slides.Add(new Slide(new List<Photo>() { new Photo(i, false, null), new Photo(pairId, false, null) }));
            }

            return res;
            // TODO: add consideration for (1) vertical slides, (2) order between pairs.
        }

        protected ProblemOutput Solve2(ProblemInput input)
        {
            ProblemOutput res = new ProblemOutput();
            res.Slides = new List<Slide>();
            HashSet<int> takenPhotos = new HashSet<int>();

            int count = 0;
            int index = 0;
            while (count < input.Photos.Length && index < input.Photos.Length)
            {
                Photo photo;
                if (res.Slides.Count != 0)
                {
                    photo = res.Slides[res.Slides.Count -1].Images[0];
                }
                else
                {
                    photo = input.Photos[index];
                }
                takenPhotos.Add(photo.Index);
                Photo nextSlide = GetNextSlide(takenPhotos, input, photo);
                if (nextSlide == null)
                {
                    takenPhotos.Remove(photo.Index);
                    index++;
                }
                else
                {
                    res.Slides.Add(new Slide(new List<Photo>() { photo }));
                    res.Slides.Add(new Slide(new List<Photo>(){ nextSlide}));
                    count++;
                }
            }

            return res;
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
