using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public abstract class SolverBase<TInput, TOutput> : ISolver<TInput, TOutput>
    {
        protected Random NumbersGenerator { get; private set; }
        protected string ProblemName { get; private set; }

        protected abstract TOutput Solve(TInput input);

        public TOutput Solve(TInput input, Random random, string problemName)
        {
            this.NumbersGenerator = random;
            this.ProblemName = problemName;
            return Solve(input);
        }
    }
}
