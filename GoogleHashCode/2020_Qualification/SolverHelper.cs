using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2020_Qualification
{
    public class BestLibrariesData
    {
        public double Score { get; set; }
        public Library Library { get; set; }
        public List<Book> Books { get; set; }
    }

    public class BestLibrariesDataComparer : IComparer<BestLibrariesData>
    {
        public int Compare(BestLibrariesData x, BestLibrariesData y)
        {
            if (x.Score == y.Score)
            {
                return x.Library.Index.CompareTo(y.Library.Index);
            }
            return y.Score.CompareTo(x.Score);
        }
    }
    class SolverHelper
    {
        static volatile object s_lock = new object();
        public static List<BestLibrariesData> GetBestLibray(ProblemInput input, HashSet<Library> notSelectedLibraries, int currentTime, double runParam)
        {
            SortedSet<BestLibrariesData> librariesDatas = new SortedSet<BestLibrariesData>(new BestLibrariesDataComparer());
            Library selectedLibrary = null;
            List<Book> bestTakenBooks = null;
            Parallel.ForEach(notSelectedLibraries, new ParallelOptions() { MaxDegreeOfParallelism = 20 }, (notSelectedLibrary) =>
            {
                var (libraryScore, takenBooks) = GetLibraryScore(notSelectedLibrary, input, currentTime, runParam);
                libraryScore /= Math.Pow(notSelectedLibrary.LibrarySignupTime, runParam);
                lock (s_lock)
                {
                    librariesDatas.Add(new BestLibrariesData() { Score = libraryScore, Library = notSelectedLibrary, Books = takenBooks });
                }
            });

            var bestLibraries = librariesDatas.Take(30).ToList();
            double bestScore = 0;
            List<BestLibrariesData> bestLibrariesCouple = new List<BestLibrariesData>();
            foreach (var currLibrary1 in bestLibraries)
            {
                HashSet<Book> booksSet = new HashSet<Book>(currLibrary1.Books);
                foreach (var currLibrary2 in bestLibraries)
                {
                    if (currLibrary1.Library.Index == currLibrary2.Library.Index)
                        continue;
                    var (libraryScore, takenBooks) = GetLibrariesCombinedScore(booksSet, currLibrary2, input, currentTime + currLibrary1.Library.LibrarySignupTime, runParam);
                    libraryScore /= Math.Pow(currLibrary2.Library.LibrarySignupTime, runParam);
                    libraryScore += currLibrary1.Score;
                    if (libraryScore > bestScore)
                    {
                        bestScore = libraryScore;
                        bestLibrariesCouple = new List<BestLibrariesData>() { currLibrary1, new BestLibrariesData() { Library = currLibrary2.Library, Books = takenBooks } };
                    }
                }
            }

            return bestLibrariesCouple;
        }

        private static (double sum, List<Book> takenBooks) GetLibrariesCombinedScore(HashSet<Book> usedBooks, BestLibrariesData currLibrary2, ProblemInput input, int currentTime, double runParam)
        {
            long counter = 0;
            double sum = 0;
            List<Book> takenBook = new List<Book>();
            var availableTime = Math.BigMul(input.NumberOfDays - currentTime - currLibrary2.Library.LibrarySignupTime, currLibrary2.Library.BooksPerDay);

            foreach (var libraryBook in currLibrary2.Library.Books)
            {
                if (usedBooks.Contains(libraryBook))
                    continue;

                if (counter++ >= availableTime)
                {
                    break;
                }

                sum += libraryBook.Score - libraryBook.Libraries.Count;
                // sum += libraryBook.Score;
                takenBook.Add(libraryBook);
            }
            return (sum, takenBook);
        }

        public static (double sum, List<Book> takenBooks) GetLibraryScore(Library library, ProblemInput input, int currentTime, double runParam)
        {
            long counter = 0;
            double sum = 0;
            List<Book> takenBook = new List<Book>();
            var availableTime = Math.BigMul(input.NumberOfDays - currentTime - library.LibrarySignupTime, library.BooksPerDay);

            foreach (var libraryBook in library.Books)
            {
                if (counter++ >= availableTime)
                {
                    break;
                }

                sum += libraryBook.Score - libraryBook.Libraries.Count;
                // sum += libraryBook.Score;
                takenBook.Add(libraryBook);
            }

            return (sum, takenBook);
        }
    }
}
