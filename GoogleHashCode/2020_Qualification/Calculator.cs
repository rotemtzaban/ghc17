using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Qualification
{
    public class Calculator : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            long score = 0;
            HashSet<int> booksScanned = new HashSet<int>();
            foreach (var library in output.libaries)
            {
                score += library.Books.Take(input.NumberOfDays - (library.LibrarySignupTime + library.LibaryStartSignUpTime))
                    .Sum(_ => _.Score);
            }

            return score;
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
