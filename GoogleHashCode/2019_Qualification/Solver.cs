using HashCodeCommon;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2019_Qualification
{

    public class StupidDolver : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            var allPhotos = input.Photos.ToArray();
            var totalTags = input.TagCount;

            var usedInASlideC = new ConcurrentDictionary<long, byte>();

            var usedInASlide = new HashSet<long>();
            Slide lastSlide;

            ////for (int i = 0; i < allPhotos.Length; i++)
            ////{
            ////    var bestScore = -1;

            ////    for (int j = 0; j < allPhotos.Length; j++)
            ////    {
            ////        int currScore = -1;
            ////        if (visited.Contains(j))
            ////        {
            ////            continue;
            ////        }
            ////        currScore = CalclateScoreForCurrentSlide(lastSlide, allPhotos[i], allPhotos[j]);
            ////        if (bestScore < currScore)
            ////        {

            ////        }
            ////    }
            ////}

            // Build First Slide
            List<Slide> slides = new List<Slide>();

            if (!allPhotos.First().IsVertical)
            {
                lastSlide = new Slide(new List<Photo>() { allPhotos.First() });
            }
            else
            {
                lastSlide = new Slide(new List<Photo>() { allPhotos.First(), allPhotos.Skip(1).First(_ => _.IsVertical) });
            }
            
            slides.Add(lastSlide);
            foreach (var photo in lastSlide.Images)
            {
                usedInASlide.Add(photo.Index);
                usedInASlideC.TryAdd(photo.Index, 0);
            }

            var lastSlideTagsBool = new bool[input.TagCount];
            var pairTagsBool = new bool[input.TagCount];

            while (usedInASlide.Count != allPhotos.Length)
            {
                Array.Clear(lastSlideTagsBool, 0, lastSlideTagsBool.Length);
                int lastSlideTagCount = 0;
                foreach (var tagIndex in lastSlide.TagsIndexes)
                {
                    lastSlideTagsBool[tagIndex] = true;
                    lastSlideTagCount++;
                }

                // build a slide
                long bestScore = -1;
                Slide slideToUse = null;

                Parallel.ForEach(Partitioner.Create(1, allPhotos.Length), range =>
                {
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        if (usedInASlideC.ContainsKey(i))
                        {
                            continue;
                        }

                        // Build a slide with this photo

                        var potentialSlide = new Slide(new List<Photo>() { allPhotos[i] });

                        long ScoreIfUsed = CalculateScoreForNewSlide(allPhotos[i], lastSlideTagsBool, lastSlideTagCount);

                        if (InterlockedExchangeIfGreaterThan(ref bestScore, ScoreIfUsed))
                        {
                            // use this
                            //bestScore = ScoreIfUsed;
                            lock (slides)
                            {
                                slideToUse = potentialSlide;

                            }
                        }
                    }
                });

                //for (int i = 1; i < allPhotos.Length; i++)
                //{
                //    // Skip if already used
                //    if (usedInASlide.Contains(i))
                //    {
                //        continue;
                //    }

                //    // Build a slide with this photo

                //    var potentialSlide = new Slide(new List<Photo>() { allPhotos[i] });

                //    long ScoreIfUsed = CalculateScoreForNewSlide(allPhotos[i], lastSlideTagsBool, lastSlideTagCount);

                //    if (bestScore < ScoreIfUsed)
                //    {
                //        // use this
                //        slideToUse = potentialSlide;
                //        bestScore = ScoreIfUsed;
                //    }
                //}

                // if slideToUse is lonely V, find it a pair:
                if (slideToUse.Images.First().IsVertical)
                {
                    // Build Pair Bool
                    Array.Clear(pairTagsBool, 0, pairTagsBool.Length);
                    foreach (var tagindex in slideToUse.Images.First().TagIndexes)
                    {
                        pairTagsBool[tagindex] = true;
                    }

                    long bestScoreForV = -1;
                    Photo vPair = null;
                    object lockpair = new object();

                    Parallel.ForEach(Partitioner.Create(1, allPhotos.Length), range =>
                    {
                        for (int i = range.Item1; i < range.Item2; i++)
                        {
                            if (usedInASlideC.ContainsKey(i) || !allPhotos[i].IsVertical || i == slideToUse.Images.First().Index)
                            {
                                continue;
                            }

                            long scoreIfUsed = CalculateScoreForVPair(lastSlideTagsBool, lastSlideTagCount, slideToUse.Images.First(), allPhotos[i], pairTagsBool);

                            if (InterlockedExchangeIfGreaterThan(ref bestScoreForV, scoreIfUsed))
                            {
                                lock (lockpair)
                                {
                                    vPair = allPhotos[i];
                                }
                            }
                        }
                    });

                    //for (int i = 0; i < allPhotos.Length; i++)
                    //{
                    //    if (usedInASlide.Contains(i) || !allPhotos[i].IsVertical || i == slideToUse.Images.First().Index)
                    //    {
                    //        continue;
                    //    }

                    //    ////foreach (var tagIndex in allPhotos[i].TagIndexes)
                    //    ////{
                    //    ////    pairTagsBool[tagIndex] = true;
                    //    ////}
                    //    long scoreIfUsed = CalculateScoreForVPair(lastSlideTagsBool, lastSlideTagCount, slideToUse.Images.First(), allPhotos[i]);

                    //    if (bestScoreForV < scoreIfUsed)
                    //    {
                    //        vPair = allPhotos[i];
                    //        bestScoreForV = scoreIfUsed;
                    //    }
                    //}

                    if (vPair == null)
                    {
                        var vertsNotUsed = allPhotos.Where(p => p.IsVertical && !usedInASlide.Contains(p.Index)).ToArray();
                    }

                    slideToUse.Images.Add(vPair);
                }

                slides.Add(slideToUse);
                foreach (var photo in slideToUse.Images)
                {
                    usedInASlide.Add(photo.Index);
                    usedInASlideC.TryAdd(photo.Index,0);
                }
                lastSlide = slideToUse;
            }
            return new ProblemOutput() { Slides = slides };
        }

        public static bool InterlockedExchangeIfGreaterThan(ref long variable, long newValue)
        {
            long initialValue;
            do
            {
                initialValue = Interlocked.Read(ref variable);
                if (initialValue >= newValue) return false;
            }
            while (Interlocked.CompareExchange(ref variable, newValue, initialValue) != initialValue);
            return true;
        }

        private long CalculateScoreForVPair(bool[] lastTagsInBool, int lastSlideTagCount, Photo firstV, Photo secondV, bool[] firstVTagsInBool)
        {
            int score = 0;

            int interSize = 0;

            int commontagscount = 0;
            foreach (var tagindex in firstV.TagIndexes)
            {
                if (lastTagsInBool[tagindex])
                {
                    interSize++;
                }
            }
            foreach (var tagindex in secondV.TagIndexes)
            {
                if (firstVTagsInBool[tagindex])
                {
                    commontagscount++;
                    continue;
                    // not counting it again
                }
                if (lastTagsInBool[tagindex])
                {
                    interSize++;
                }
            }

            int totalTagsCount = firstV.TagIndexes.Length + secondV.TagIndexes.Length - commontagscount;

            score = Math.Min(Math.Min(interSize, totalTagsCount - interSize),
                lastSlideTagCount - interSize);

            return score;
        }

        private long CalculateScoreForNewSlide(Photo potentialPhoto, bool[] lastTagsInBool, int lastSlideTagCount)
        {
            int secondPhotoTagsCount = potentialPhoto.Tags.Length;

            int interSize = 0;

            foreach (var secondTag in potentialPhoto.TagIndexes)
            {
                if (lastTagsInBool[secondTag])
                {
                    interSize++;
                }
            }

            var score = Math.Min(Math.Min(interSize, secondPhotoTagsCount - interSize),
                lastSlideTagCount - interSize);

            return score;
        }
    }
}
