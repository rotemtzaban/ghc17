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
                var (selectedLibrary, bestTakenBooks) =
                    SolverHelper.GetBestLibray(input, notSelectedLibraries, selectedBooks, currentTime);
                if (selectedLibrary == null)
                {
                    break;
                }

                selectedLibrary.SendBooksToScan(bestTakenBooks);
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

      
    }
}
