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
            ProblemOutput res = new ProblemOutput();
            res.Slides = new List<Slide>();
            res.Slides.Add(new Slide(new List<Photo>() { new Photo(0, false, null) }));
            res.Slides.Add(new Slide(new List<Photo>() { new Photo(3, false, null) }));
            res.Slides.Add(new Slide(new List<Photo>() { new Photo(1, true, null), new Photo(2, true, null) }));

            return res;
        }

        protected ProblemOutput Solve2(ProblemInput input)
        {
            HashSet<int> takenPhotos = new HashSet<int>();
            List<Slide> slideShow = new List<Slide>();

            int count = 0;
            int index = 0;
            while (count < input.Photos.Length && index < input.Photos.Length)
            {
                var photo = input.Photos[index];
                takenPhotos.Add(photo.Index);
                Photo getNextSlide = GetNextSlide(takenPhotos, input, photo);
                if (getNextSlide != null)
                {
                    takenPhotos.Remove(photo.Index);
                    index++;
                }
                else
                {
                    count++;
                }
            }

            return null;
        }

        private Photo GetNextSlide(HashSet<int> takenPhotos, ProblemInput input, Photo firstPhoto)
        {
            foreach (var photo in input.Photos)
            {
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
