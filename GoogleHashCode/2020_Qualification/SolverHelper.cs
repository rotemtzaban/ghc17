using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2020_Qualification
{
    class SolverHelper
    {
        static volatile object s_lock = new object();
        public static (Library, List<Book>) GetBestLibray(ProblemInput input, HashSet<Library> notSelectedLibraries, int currentTime, double runParam)
        {
            Library selectedLibrary = null;
            double bestScore = 0;
            List<Book> bestTakenBooks = null;
            Parallel.ForEach(notSelectedLibraries, new ParallelOptions() { MaxDegreeOfParallelism = 20 }, (notSelectedLibrary) =>
            {
                var (libraryScore, takenBooks) = GetLibraryScore(notSelectedLibrary, input, currentTime, runParam);
                libraryScore /= Math.Pow(notSelectedLibrary.LibrarySignupTime, runParam);

                if (libraryScore > bestScore)
                    lock (s_lock)
                    {
                        if (libraryScore > bestScore)
                        {
                            bestScore = libraryScore;
                            selectedLibrary = notSelectedLibrary;
                            bestTakenBooks = takenBooks;
                        }
                    }
            });

            return (selectedLibrary, bestTakenBooks);
        }

        public static (double sum, List<Book> takenBooks) GetLibraryScore(Library library, ProblemInput input, int currentTime, double runParam)
        {
            long counter = 0;
            double sum = 0;
            List<Book> takenBooks = new List<Book>();
            var availableTime = Math.BigMul(input.NumberOfDays - currentTime - library.LibrarySignupTime, library.BooksPerDay);

            foreach (var libraryBook in library.Books)
            {
                if (counter++ >= availableTime)
                {
                    break;
                }

                //  sum += libraryBook.Score - libraryBook.Libraries.Count;
                 sum += libraryBook.Score;
                takenBooks.Add(libraryBook);
            }

            return (sum, takenBooks);
        }
    }
}
