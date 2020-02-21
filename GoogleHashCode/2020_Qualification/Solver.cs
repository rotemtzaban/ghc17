using _2020_Qualification.Properties;
using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            int currentTime = 0;
            var output = new ProblemOutput() { libaries = new List<Library>() };

            int count = 0;
            while (currentTime < input.NumberOfDays)
            {
                List<BestLibrariesData> bestLibrariesDatas =
                    SolverHelper.GetBestLibray(input, notSelectedLibraries, currentTime, RunParam);
                if (bestLibrariesDatas == null || !bestLibrariesDatas.Any())
                {
                    break;
                }

                foreach (var selectedLibrary in bestLibrariesDatas)
                {
                    selectedLibrary.Library.SendBooksToScan(selectedLibrary.Books);
                    selectedLibrary.Library.LibaryStartSignUpTime = currentTime;
                    notSelectedLibraries.Remove(selectedLibrary.Library);

                    currentTime += selectedLibrary.Library.LibrarySignupTime;
                    output.libaries.Add(selectedLibrary.Library);
                }

                if (++count % 100 == 0)
                {
                    Console.WriteLine(count);
                }
            }

            if (input.NumberOfDays == 200)
            {
                var newOutput = ReorderLibraries(output);
                return newOutput;
            }

            return output;
        }

        private ProblemOutput ReorderLibraries(ProblemOutput output)
        {
            Parser parser = new Parser();
            var originalInput = parser.ParseFromData(Resources.e_so_many_books);
            // var librariesToImprove = output.libaries.Where(_ => originalInput.Libraries[_.Index].Books.Count > _.SelectedBooks.Count).ToList();
            int notImproved = 0;
            Calculator calculator = new Calculator();
            List<Library> orderedLibraries = new List<Library>(output.libaries.Select(_ => originalInput.Libraries.FirstOrDefault(__ => __.Index == _.Index)));
            var bestOrderedLibraries = orderedLibraries.ToList();
            var bestScore = GetScore(orderedLibraries, originalInput);
            Stopwatch time = Stopwatch.StartNew();
            while (notImproved != 10000 && time.ElapsedMilliseconds < 20 * 1000)
            {
                var libraryToTop = orderedLibraries[NumbersGenerator.Next(orderedLibraries.Count)];
                orderedLibraries.Remove(libraryToTop);
                orderedLibraries.Insert(NumbersGenerator.Next(orderedLibraries.Count), libraryToTop);
                int score = GetScore(orderedLibraries, originalInput);
                if (bestScore < score)
                {
                    bestScore = score;
                    bestOrderedLibraries = orderedLibraries.ToList();
                    notImproved = 0;
                }
                else
                {
                    orderedLibraries = bestOrderedLibraries.ToList();
                    notImproved++;
                }
            }

            return GenerateOutput(originalInput, bestOrderedLibraries);
        }

        private ProblemOutput GenerateOutput(ProblemInput originalInput, List<Library> bestOrderedLibraries)
        {
            ProblemOutput output = new ProblemOutput();
            output.libaries = new List<Library>();
            int day = 0;
            foreach (var library in bestOrderedLibraries)
            {
                if (originalInput.NumberOfDays < day + library.LibrarySignupTime)
                    break;

                var (libraryScore, takenBooks) = SolverHelper.GetLibraryScore(library, originalInput, day, 1);
                library.SendBooksToScan(takenBooks);
                library.LibaryStartSignUpTime = day;

                day += library.LibrarySignupTime;

                output.libaries.Add(library);
            }

            return output;
        }

        public int GetScore(List<Library> libraries, ProblemInput input)
        {
            int score = 0;
            int day = 0;
            HashSet<Book> books = new HashSet<Book>();
            foreach (var library in libraries)
            {
                day += library.LibrarySignupTime;
                if (input.NumberOfDays < day)
                    break;

                var booksCount = Math.BigMul(input.NumberOfDays - day, library.BooksPerDay);

                int takenBooks = 0;
                foreach (var book in library.Books)
                {
                    if (takenBooks == booksCount)
                        break;
                    if (books.Add(book))
                    {
                        score += book.Score;
                        takenBooks++;
                    }
                }
            }

            return score;
        }
    }
}
