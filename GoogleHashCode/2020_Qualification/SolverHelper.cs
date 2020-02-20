using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Qualification
{
    class SolverHelper
    {
        public static (Library, List<Book>) GetBestLibray(ProblemInput input, HashSet<Library> notSelectedLibraries, HashSet<Book> selectedBooks, int currentTime)
        {
            object s_lock = new object();
            Library selectedLibrary = null;
            double bestScore = -1;
            List<Book> bestTakenBooks = null;
            Parallel.ForEach(notSelectedLibraries, (notSelectedLibrary) =>
            {
                var (libraryScore, takenBooks) = GetLibraryScore(selectedBooks, notSelectedLibrary, input, currentTime);
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

        public static (int sum, List<Book> takenBooks) GetLibraryScore(HashSet<Book> selectedBooks, Library library, ProblemInput input, int currentTime)
        {
            int counter = 0;
            int sum = 0;
            List<Book> takenBooks = new List<Book>();
            var availableTime = (input.NumberOfDays - currentTime - library.LibrarySignupTime - 1) * library.BooksPerDay;

            foreach (var libraryBook in library.Books)
            {
                if (selectedBooks.Contains(libraryBook))
                    continue;

                if (counter++ >= availableTime)
                {
                    break;
                }

                sum += libraryBook.Score;
                takenBooks.Add(libraryBook);
            }

            return (sum, takenBooks);
        }
    }
}
