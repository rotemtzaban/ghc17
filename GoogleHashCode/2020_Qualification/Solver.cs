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
            while (currentTime < input.NumberOfDays)
            {
                Library selectedLibrary = null;
                double bestScore = -1;
                foreach (var notSelectedLibrary in notSelectedLibraries)
                {
                    var libraryScore = GetLibraryScore(selectedBooks, notSelectedLibrary, input, currentTime);
                    if (libraryScore > bestScore)
                    {
                        bestScore = libraryScore;
                        selectedLibrary = notSelectedLibrary;
                    }
                }

                if (selectedLibrary == null)
                {
                    break;
                }

                var books = selectedLibrary.Books.Except(selectedBooks)
                    .Take((input.NumberOfDays - currentTime - selectedLibrary.LibrarySignupTime) * selectedLibrary.BooksPerDay).ToList();

                selectedLibrary.SelectedBooks = books;
                selectedLibrary.LibrarySignupTime = currentTime;
                notSelectedLibraries.Remove(selectedLibrary);
                foreach (var book in books)
                {
                    selectedBooks.Add(book);
                }

                currentTime += selectedLibrary.LibrarySignupTime;
                output.libaries.Add(selectedLibrary);
            }

            return output;
        }

        private double GetLibraryScore(HashSet<Book> selectedBooks, Library library, ProblemInput input, int currentTime)
        {
            return library.Books.Except(selectedBooks)
                .Take((input.NumberOfDays - currentTime - library.LibrarySignupTime) * library.BooksPerDay)
                .Sum(book => book.Score);
        }
    }
}
