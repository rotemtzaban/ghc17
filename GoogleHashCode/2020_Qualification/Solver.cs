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

            int currentTime = 0;
            var output = new ProblemOutput(){libaries = new List<Library>()};

            int count = 0;
            while (currentTime < input.NumberOfDays)
            {
                List <BestLibrariesData> bestLibrariesDatas =
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

            return output;
        }

      
    }
}
