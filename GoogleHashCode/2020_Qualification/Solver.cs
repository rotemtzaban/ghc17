using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Qualification
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            HashSet<Library> notSelectedLibraries = new HashSet<Library>(input.Libraries);
            HashSet<Book> selectedBooks = new HashSet<Book>();

            int currentTime = 0;
            var output = new ProblemOutput(){libaries = new List<Library>()};

            int count = 0;
            while (currentTime < input.NumberOfDays)
            {
                Library selectedLibrary = null;
                double bestScore = -1;
                List<Book> bestTakenBooks = null;
                foreach (var notSelectedLibrary in notSelectedLibraries)
                {
                    var (libraryScore, takenBooks) = GetLibraryScore(selectedBooks, notSelectedLibrary, input, currentTime);
                    if (libraryScore > bestScore)
                    {
                        bestScore = libraryScore;
                        selectedLibrary = notSelectedLibrary;
                        bestTakenBooks = takenBooks;
                    }
                }

                if (selectedLibrary == null)
                {
                    break;
                }

                selectedLibrary.SelectedBooks = bestTakenBooks;
                selectedLibrary.LibrarySignupTime = currentTime;
                notSelectedLibraries.Remove(selectedLibrary);
                foreach (var book in bestTakenBooks)
                {
                    selectedBooks.Add(book);
                }

                currentTime += selectedLibrary.LibrarySignupTime;
                output.libaries.Add(selectedLibrary);

                if (++count % 100 == 0)
                {
                    Console.WriteLine(count);
                }
            }

            return output;
        }

        private (int sum, List<Book> takenBooks) GetLibraryScore(HashSet<Book> selectedBooks, Library library, ProblemInput input, int currentTime)
        {
            int counter = 0;
            int sum =0 ;
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
